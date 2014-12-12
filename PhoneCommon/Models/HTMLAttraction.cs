using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneCommon.Models
{
	public class HTMLAttraction : INotifyPropertyChanged
	{
		private HTMLStatus _status;
		public HTMLAttraction() { }
		public string parkName { get; set; }
		public string themeName { get; set; }
		public string title { get; set; }
		public string description { get; set; }
		public HTMLStatus status
		{
			get { return _status; }
			set
			{
				_status = value;
				RaisePropertyChanged("status");
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
