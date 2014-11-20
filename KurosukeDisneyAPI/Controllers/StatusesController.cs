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
	public class StatusesController : ApiController
	{
		[HttpGet]
		[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
		public List<HTMLStatus> GetStatuses()
		{
			var context = new LightSpeedContext<WaitingTimeModelUnitOfWork>("WaitingTimeModel");
			var htmlStatuses = new List<HTMLStatus>();
			using (var uow = context.CreateUnitOfWork())
			{
				var attractions = uow.Attractions.ToArray();
				foreach (var attraction in attractions)
				{
					var status = uow.Statuses.Where(x => x.AttractionId == attraction.Id).OrderByDescending(x => x.UpdateDateTime).FirstOrDefault();
					var htmlStatus = new HTMLStatus(status);
					htmlStatuses.Add(htmlStatus);
				}
			}
			return htmlStatuses;
		}
		public List<HTMLStatus> GetStatuses(int attraction_id)//未実装
		{
			var context = new LightSpeedContext<WaitingTimeModelUnitOfWork>("WaitingTimeModel");
			using (var uow = context.CreateUnitOfWork())
			{
				var statuses = uow.Statuses.Where(x => x.AttractionId == attraction_id).OrderByDescending(x => x.UpdateDateTime).Take(10);

			}
			return new List<HTMLStatus>();
		}
	}
}
