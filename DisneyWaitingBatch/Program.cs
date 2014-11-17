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
					if (attractionList == null)//パーサー内でエラーが起きているので終了する。
					{
						Console.WriteLine("終了します。");
						return;
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

						//statusに値が入った場合は、前回の取得から待ち時間が更新されていない。
						var status = uow.Statuses
							.Where(x => (x.Attraction.Id == attraction.Id && x.UpdateString == htmlAttraction.status.updateString))
							.OrderByDescending(x => x.UpdateDateTime)
							.FirstOrDefault();
						if (status == null)
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
						uow.SaveChanges();
					}

				}
			}
		}
	}
}
