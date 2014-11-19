using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
	public class HTMLTheme
	{
		public HTMLTheme(Theme theme)
		{
			this.ThemeName = theme.ThemeName;
		}
		public string ThemeName { get; set; }
		public List<HTMLAttraction> Attractions { get; set; }
	}
}
