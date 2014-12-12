using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneCommon.Models
{
	public class HTMLTheme
	{
		public string ThemeName { get; set; }
		public ObservableCollection<HTMLAttraction> Attractions{ get; set; }
	}
}
