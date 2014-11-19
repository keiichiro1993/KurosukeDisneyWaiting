using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.Models;
using Mindscape.LightSpeed;
using Mindscape.LightSpeed.Linq;
using WebApi.OutputCache.V2;

namespace KurosukeDisneyAPI.Controllers
{
	public class AttractionsController : ApiController
	{
		/*アトラクション（最新の待ち時間込）の一覧を返す。*/
		[HttpGet]
		[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
		public List<HTMLPark> GetAttractions()
		{
			var context = new LightSpeedContext<WaitingTimeModelUnitOfWork>("WaitingTimeModel");
			var htmlParks = new List<HTMLPark>();//テーマのリストが入ったパークのリスト
			using (var uow = context.CreateUnitOfWork())
			{
				var parks = uow.Parks.ToArray();
				foreach (var park in parks)
				{
					var htmlPark = new HTMLPark(park);
					var themes = uow.Themes.Where(x => x.ParkId == park.Id).ToArray();
					var htmlThemes = new List<HTMLTheme>();//アトラクションのリストが入ったテーマのリスト
					foreach (var theme in themes)
					{
						var htmlTheme = new HTMLTheme(theme);
						var attractions = uow.Attractions.Where(x => x.Theme.Id == theme.Id).ToArray();
						var htmlAttractions = new List<HTMLAttraction>();//それぞれに最新の待ち時間が入ったアトラクションのリスト
						foreach (var attraction in attractions)
						{
							var status = uow.Statuses.Where(x => x.AttractionId == attraction.Id).OrderByDescending(x => x.UpdateDateTime).FirstOrDefault();
							var htmlAttraction = new HTMLAttraction(attraction);
							var htmlStatus = new HTMLStatus(status);
							htmlAttraction.status = htmlStatus;
							htmlAttractions.Add(htmlAttraction);
						}
						htmlTheme.Attractions = htmlAttractions;
						htmlThemes.Add(htmlTheme);
					}
					htmlPark.Themes = htmlThemes;
					htmlParks.Add(htmlPark);
				}
			}
			return htmlParks;
		}
	}
}
