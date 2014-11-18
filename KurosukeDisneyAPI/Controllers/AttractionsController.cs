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
	[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
    public class AttractionsController : ApiController
    {
		/*アトラクション（最新の待ち時間込）の一覧を返す。*/
		[]
		public List<HTMLAttraction> GetAttractions() 
		{
			var context = new LightSpeedContext<WaitingTimeModelUnitOfWork>("WaitingTimeModel");
			var htmlAttractions = new List<HTMLAttraction>();
			using (var uow = context.CreateUnitOfWork())
			{
				var attractions = uow.Attractions.ToArray();
				foreach(var attraction in attractions)
				{
					var status = uow.Statuses.Where(x => x.AttractionId == attraction.Id).OrderByDescending(x => x.UpdateDateTime).FirstOrDefault();
					var htmlAttraction = new HTMLAttraction(attraction);
					var htmlStatus = new HTMLStatus(status);
					htmlAttraction.status = htmlStatus;
					htmlAttractions.Add(htmlAttraction);
				}
			}
			return htmlAttractions;
		}
    }
}
