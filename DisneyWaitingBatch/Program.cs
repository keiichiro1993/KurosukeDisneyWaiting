using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Collections.ObjectModel;
using Common.Models;
using Mindscape.LightSpeed;

namespace DisneyWaitingBatch
{
	class Program
	{
		static void Main(string[] args)
		{
			var context = new LightSpeedContext<WaitingTimeModelUnitOfWork>("WaitingTimeModel");
			using (var uow = context.CreateUnitOfWork())
			{
				var parks = uow.Parks.ToArray();
				foreach (var park in parks)
				{
					var themes = uow.Themes.Where(x => x.ParkId == park.Id).ToArray();
					var attractions = uow.Attractions.Where(x => x.Theme.ParkId == park.Id).ToArray();
					Console.WriteLine(park.ParkName);
					Console.WriteLine(park.WaitingTimeUrl);

					var attractionList = Utils.ParseUtil.ParseWaitingPage(park);
					if (attractionList == null)//パーサー内でエラーが起きているor閉園中と思われるので終了する。（今はとりあえず閉園中ということにする。）
					{
						/*すべてのアトラクションを閉園の扱いにする*/
						foreach (var attraction in attractions)
						{
							var existStatus = uow.Statuses.Where(x => x.AttractionId == attraction.Id).OrderByDescending(x => x.UpdateDateTime).FirstOrDefault();
							if (existStatus.UpdateString != "閉園しています")
							{
								var status = new Status();
								status.AttractionId = attraction.Id;
								status.Run = false;
								status.RunString = "閉園しています。";
								status.UpdateString = "閉園しています";
								TimeZoneInfo jst = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
								status.UpdateDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), jst);
								status.WaitTime = 0;
								uow.Add(status);
							}
						}
						uow.SaveChanges();


						Console.WriteLine("エラーもしくは閉園中です。中止します。");
						continue;
					}

					foreach (HTMLAttraction htmlAttraction in attractionList)
					{
						//テーマが存在しなければ作成
						var theme = themes
							.Where(x => (x.ThemeName == htmlAttraction.themeName && x.Park.Id == park.Id))
							.FirstOrDefault();
						if (theme == null)
						{
							theme = new Theme();
							theme.ParkId = park.Id;
							theme.ThemeName = htmlAttraction.themeName;
							uow.Add(theme);
						}

						//アトラクションが存在しなければ作成
						var attraction = attractions
							.Where(x => (x.Title == htmlAttraction.title && x.Theme.Id == theme.Id))
							.FirstOrDefault();
						if (attraction == null)
						{
							attraction = new Attraction();
							attraction.Title = htmlAttraction.title;
							attraction.ThemeId = theme.Id;
							uow.Add(attraction);
						}

						//statusにの最後の値から、更新時間もしくは運営状況（待ち時間が更新されないアトラクション（ショップ系）対策）が変わったときのみ更新
						var status = uow.Statuses
							.Where(x => x.Attraction.Id == attraction.Id)
							.OrderByDescending(x => x.UpdateDateTime)
							.FirstOrDefault();
						if (status.UpdateString != htmlAttraction.status.updateString || status.RunString != htmlAttraction.status.runString)
						{
							status = new Status();
							status.UpdateString = htmlAttraction.status.updateString;
							status.UpdateDateTime = htmlAttraction.status.update;
							status.Run = htmlAttraction.status.run;
							status.RunString = htmlAttraction.status.runString;
							status.WaitTime = htmlAttraction.status.waitTime;
							status.AttractionId = attraction.Id;
							uow.Add(status);
							Console.WriteLine(status.Attraction.Title + ":" + status.WaitTime.ToString());
						}
					}
				}
				uow.SaveChanges();
			}
		}
	}
}
