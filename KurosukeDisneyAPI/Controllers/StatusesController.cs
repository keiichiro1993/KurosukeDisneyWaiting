using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.Models;
using Mindscape.LightSpeed;
using Mindscape.LightSpeed.Linq;

namespace KurosukeDisneyAPI.Controllers
{
	public class StatusesController : ApiController
	{
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
		public List<HTMLStatus> GetStatuses(int attraction_id)
		{
			return new List<HTMLStatus>();
		}
	}
}
