using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace DisneyWaitingBatch.Utils
{
	class ParseUtil
	{
		public static List<HTMLAttraction> ParseWaitingPage(Park park)
		{
			string url = park.WaitingTimeUrl;
			string parkName = park.ParkName;
			HtmlWeb htmlWeb = new HtmlWeb();
			HtmlDocument page;
			try
			{
				page = htmlWeb.Load(url);
			}
			catch(System.Net.WebException ex) //ネットワークに接続できなかったとき
			{
				Console.WriteLine(ex.Message);
				return null;
			}

			var wait = page.GetElementbyId("wait");
			var themes = wait.SelectNodes("section[@class='theme open']");
			var attractionList = new List<HTMLAttraction>();
			foreach (var theme in themes)
			{
				/*各テーマエリアごとの処理*/
				string themeName = theme.SelectSingleNode("h2").InnerText;
				var articles = theme.SelectNodes("article");
				foreach (var article in articles)
				{
					/*各アトラクションごとの処理*/
					var attraction = new HTMLAttraction();
					var status = new HTMLStatus();

					var item = article.SelectSingleNode("a");
					var about = item.SelectSingleNode("div[@class='about']");
					string title = about.SelectSingleNode("h3").InnerText;
					string run = about.SelectSingleNode("p[@class='run']").InnerText;
					var updateNode = item.SelectSingleNode("p[@class='update']");
					string update = "取得できません";
					if (updateNode != null)
					{
						update = updateNode.InnerText;
					}

					var time = item.SelectSingleNode("div[@class='time']");
					string waitTime = "";
					if (time != null)
					{
						waitTime = time.SelectSingleNode("p[@class='waitTime']").InnerText;
						waitTime = waitTime.Replace("分", "");
						status.waitTime = int.Parse(waitTime);
					}

					attraction.title = title;
					attraction.themeName = themeName;
					attraction.parkName = parkName;

					/*日付と時間をまとめる処理*/
					TimeZoneInfo jst = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
					var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), jst);
					status.update = now;
					status.updateString = update;

					if (run == "\r\n運営中\r\n")
					{
						status.run = true;
					}
					status.runString = run;

					attraction.status = status;
					attractionList.Add(attraction);
				}
			}
			return attractionList;
		}
	}
}
