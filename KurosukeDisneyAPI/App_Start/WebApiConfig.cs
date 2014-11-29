using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace KurosukeDisneyAPI
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API の設定およびサービス
			// ベアラ トークン認証のみを使用するように、Web API を設定します。
			config.SuppressDefaultHostAuthentication();
			config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

			// Web API ルート
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "CurrentStatuses",
				routeTemplate: "api/statuses/now",
				defaults: new { controller = "Statuses", action = "GetStatuses" }
			);

			config.Routes.MapHttpRoute(
				name: "TodayStatuses",
				routeTemplate: "api/statuses/today/{id}",
				defaults: new { controller = "Statuses", action = "GetStatusesToday" }
			);

			config.Routes.MapHttpRoute(
				name: "PastStatuses",
				routeTemplate: "api/statuses/past/{id}/{days}",
				defaults: new { controller = "Statuses", action = "GetStatuses" }
			);

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
