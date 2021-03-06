﻿using System;
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

		[HttpGet]
		[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
		public List<HTMLStatus> GetStatusesToday(int id)
		{
			var context = new LightSpeedContext<WaitingTimeModelUnitOfWork>("WaitingTimeModel");
			var htmlStatuses = new List<HTMLStatus>();
			using (var uow = context.CreateUnitOfWork())
			{
				TimeZoneInfo jst = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
				var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), jst);
				var statuses = uow.Statuses.Where(x => x.AttractionId == id).Where(x => (x.UpdateDateTime.Year == now.Year && x.UpdateDateTime.Date == now.Date)).OrderByDescending(x => x.UpdateDateTime).ToArray();
				foreach (var status in statuses)
				{
					var htmlStatus = new HTMLStatus(status);
					htmlStatuses.Add(htmlStatus);
				}
			}
			return htmlStatuses;
		}

		[HttpGet]
		[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
		public List<HTMLStatus> GetStatuses(int id, int days)
		{
			var context = new LightSpeedContext<WaitingTimeModelUnitOfWork>("WaitingTimeModel");
			var htmlStatuses = new List<HTMLStatus>();
			using (var uow = context.CreateUnitOfWork())
			{
				TimeZoneInfo jst = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
				var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), jst);
				var past = now - new TimeSpan(days, 0, 0, 0, 0);
				var statuses = uow.Statuses.Where(x => x.AttractionId == id).Where(x => (x.UpdateDateTime.Year == past.Year && x.UpdateDateTime.Date == past.Date)).OrderByDescending(x => x.UpdateDateTime).ToArray();
				foreach (var status in statuses)
				{
					var htmlStatus = new HTMLStatus(status);
					htmlStatus.update = htmlStatus.update + new TimeSpan(days, 0, 0, 0, 0);
					htmlStatuses.Add(htmlStatus);
				}
			}
			return htmlStatuses;
		}

	}
}
