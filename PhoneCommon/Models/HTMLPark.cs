using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneCommon.Models
{
	public class HTMLPark
	{
		public string WaitingTimeUrl { get; set; }
		public string ParkName { get; set; }
		public List<HTMLTheme> Themes { get; set; }
	}
}
