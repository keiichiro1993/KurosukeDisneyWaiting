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
		private ObservableCollection<HTMLStatus> _statuses = new ObservableCollection<HTMLStatus>();
		private string _attractionTitle = "";
		public ObservableCollection<HTMLStatus> Statuses
		{
			get { return this._statuses; }
			set
			{
				if (_statuses != value)
				{
					_statuses = value;
					RaisePropertyChanged("Statuses");
				}
			}
		}
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
