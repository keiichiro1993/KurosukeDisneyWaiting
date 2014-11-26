using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Popups;
using PhoneCommon.Models;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=391641 を参照してください

namespace PhoneApp
{
	/// <summary>
	/// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
	/// </summary>
	public sealed partial class MainPage : Page
	{
		private WaitingTimeViewModel viewModel = new WaitingTimeViewModel();
		private Windows.ApplicationModel.Resources.ResourceLoader resourceLoader;
		public MainPage()
		{
			this.InitializeComponent();

			this.NavigationCacheMode = NavigationCacheMode.Required;

			this.DataContext = viewModel;

			resourceLoader = new Windows.ApplicationModel.Resources.ResourceLoader();
		}

		/// <summary>
		/// このページがフレームに表示されるときに呼び出されます。
		/// </summary>
		/// <param name="e">このページにどのように到達したかを説明するイベント データ。
		/// このプロパティは、通常、ページを構成するために使用します。</param>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			// TODO: ここに表示するページを準備します。

			GetAttractions();

			// TODO: アプリケーションに複数のページが含まれている場合は、次のイベントの
			// 登録によりハードウェアの戻るボタンを処理していることを確認してください:
			// Windows.Phone.UI.Input.HardwareButtons.BackPressed イベント。
			// 一部のテンプレートで指定された NavigationHelper を使用している場合は、
			// このイベントが自動的に処理されます。
		}

		private async void GetAttractions()
		{
			using (var client = new HttpClient())
			{
				try
				{
					var json = await client.GetStringAsync("http://kurosukeapi.azurewebsites.net/api/attractions");
					var obj = JsonConvert.DeserializeObject<ObservableCollection<HTMLPark>>(json);
					viewModel.TokyoDisneyLand = (HTMLPark)obj.Where(x => x.ParkName == "東京ディズニーランド").FirstOrDefault();
					viewModel.TokyoDisneySea = (HTMLPark)obj.Where(x => x.ParkName == "東京ディズニーシー").FirstOrDefault();
				}
				catch (HttpRequestException ex)
				{
					var msg = new MessageDialog(resourceLoader.GetString("NetWorkErr") + ": " + ex.Message, resourceLoader.GetString("ErrHeader"));
					msg.ShowAsync();
				}
			}

		}

		private void AttractionTapped(object sender, TappedRoutedEventArgs e)
		{
			var htmlAttraction = (HTMLAttraction)((Border)sender).DataContext;
			this.Frame.Navigate(typeof(DetailPage), htmlAttraction);
		}

		private async void ReloadButtonTapped(object sender, TappedRoutedEventArgs e)
		{
			using (var client = new HttpClient())
			{
				try
				{
					var json = await client.GetStringAsync("http://kurosukeapi.azurewebsites.net/api/statuses/now");
					var obj = JsonConvert.DeserializeObject<ObservableCollection<HTMLStatus>>(json);
					FindNewStatus(obj, viewModel.TokyoDisneyLand);
					FindNewStatus(obj, viewModel.TokyoDisneySea);
				}
				catch (HttpRequestException ex)
				{
					var msg = new MessageDialog(resourceLoader.GetString("NetWorkErr") + ": " + ex.Message, resourceLoader.GetString("ErrHeader"));
					msg.ShowAsync();
				}
			}
		}

		private void FindNewStatus(ObservableCollection<HTMLStatus> obj, HTMLPark park)
		{
			foreach (var theme in park.Themes)
			{
				foreach (var attraction in theme.Attractions)
				{
					var newStatus = obj.Where(x => x.attractionId == attraction.status.attractionId).FirstOrDefault();
					if (newStatus != null)
					{
						attraction.status = newStatus;
					}
				}
			}
		}
	}
}
