using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
	public class HTMLAttraction
	{
		public HTMLAttraction() { }
		public HTMLAttraction(Attraction attraction)
		{
			this.title = attraction.Title;
			this.parkName = attraction.Theme.Park.ParkName;
			this.themeName = attraction.Theme.ThemeName;
			this.description = attraction.Description;
		}
		public string parkName { get; set; }
		public string themeName { get; set; }
		public string title { get; set; }
		public string description { get; set; }
		public HTMLStatus status { get; set; }
	}
}
