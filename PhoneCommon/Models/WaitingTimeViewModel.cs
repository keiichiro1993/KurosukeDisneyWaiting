using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneCommon.Models
{
	public class WaitingTimeViewModel : INotifyPropertyChanged
	{
		private HTMLPark _tokyoDisneySea = new HTMLPark();
		private HTMLPark _tokyoDisneyLand = new HTMLPark();
		public HTMLPark TokyoDisneySea
		{
			get { return _tokyoDisneySea; }
			set
			{
				if (_tokyoDisneySea != value)
				{
					_tokyoDisneySea = value;
					RaisePropertyChanged("TokyoDisneySea");
				}
			}
		}
		public HTMLPark TokyoDisneyLand
		{
			get { return _tokyoDisneyLand; }
			set
			{
				if (_tokyoDisneyLand != value)
				{
					_tokyoDisneyLand = value;
					RaisePropertyChanged("TokyoDisneyLand");
				}
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChanged(string propertyName)
		{
			var d = PropertyChanged;
			if (d != null)
				d(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
