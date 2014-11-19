using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
	public class HTMLPark
	{
		public HTMLPark(Park park)
		{
			this.WaitingTimeUrl = park.WaitingTimeUrl;
			this.ParkName = park.ParkName;
		}
		public string WaitingTimeUrl { get; set; }
		public string ParkName { get; set; }
		public List<HTMLTheme> Themes { get; set; }
	}
}
