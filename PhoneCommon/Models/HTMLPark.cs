using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneCommon.Models
{
	public class HTMLPark
	{
		public string WaitingTimeUrl { get; set; }
		public string ParkName { get; set; }
		public ObservableCollection<HTMLTheme> Themes { get; set; }
	}
}
