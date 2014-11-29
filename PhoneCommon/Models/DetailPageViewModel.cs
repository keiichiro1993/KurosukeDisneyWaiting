using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneCommon.Models
{
	public class DetailPageViewModel : INotifyPropertyChanged
	{
		private string _attractionTitle = "";

		public string AttractionTitle
		{
			get { return this._attractionTitle; }
			set
			{
				if (_attractionTitle != value)
				{
					_attractionTitle = value;
					RaisePropertyChanged("AttractionTitle");
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
