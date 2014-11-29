using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using PhoneCommon.Models;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using PhoneApp.Common;
using Windows.UI;
using Windows.UI.ViewManagement;


// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkID=390556 を参照してください

namespace PhoneApp
{
	/// <summary>
	/// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
	/// </summary>
	public sealed partial class DetailPage : Page
	{
		private DetailPageViewModel viewModel = new DetailPageViewModel();
		private Windows.ApplicationModel.Resources.ResourceLoader resourceLoader;
		private NavigationHelper navigationHelper;
		private List<ISeries> currentLines;
		private HTMLAttraction currentAttraction;
		public DetailPage()
		{
			this.InitializeComponent();

			this.DataContext = viewModel;
			this.navigationHelper = new NavigationHelper(this);
			this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
			this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
			resourceLoader = new Windows.ApplicationModel.Resources.ResourceLoader();
		}

		public NavigationHelper NavigationHelper
		{
			get { return this.navigationHelper; }
		}


		/// <summary>
		/// このページがフレームに表示されるときに呼び出されます。
		/// </summary>
		/// <param name="e">このページにどのように到達したかを説明するイベント データ。
		/// このプロパティは、通常、ページを構成するために使用します。</param>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			this.navigationHelper.OnNavigatedTo(e);
			var attraction = (HTMLAttraction)e.Parameter;
			viewModel.AttractionTitle = attraction.title;
			GetStatuses(attraction);
			currentAttraction = attraction;
		}

		private async void GetStatuses(HTMLAttraction attraction)
		{
			//ProgressIndicatorを表示したいがため
			StatusBar statusBar = StatusBar.GetForCurrentView();
			statusBar.ProgressIndicator.Text = "Loading...";
			await statusBar.ProgressIndicator.ShowAsync();

			if (currentLines != null)
			{
				foreach (var line in currentLines)
				{
					ChartSpace.Series.Remove(line);
				}
				currentLines = null;
			}
			if (currentLines == null)
			{
				currentLines = new List<ISeries>();
			}
			int attractionId = attraction.status.attractionId;
			using (var client = new HttpClient())
			{
				try
				{
					var json = await client.GetStringAsync("http://kurosukeapi.azurewebsites.net/api/statuses/today/" + attractionId.ToString());
					var obj = JsonConvert.DeserializeObject<ObservableCollection<HTMLStatus>>(json);
					var line = new LineSeries();
					line.ItemsSource = obj.OrderBy(x => x.update);
					line.DependentValuePath = "waitTime";
					line.IndependentValuePath = "update";
					line.BorderThickness = new Thickness(2);
					line.Title = "Today";
					line.BorderBrush = new SolidColorBrush(Colors.LightBlue);
					ChartSpace.Series.Add(line);
					currentLines.Add(line);

					var json2 = await client.GetStringAsync("http://kurosukeapi.azurewebsites.net/api/statuses/past/" + attractionId.ToString() + "/1/");
					var obj2 = JsonConvert.DeserializeObject<ObservableCollection<HTMLStatus>>(json2);
					var line2 = new LineSeries();
					line2.ItemsSource = obj2.OrderBy(x => x.update);
					line2.DependentValuePath = "waitTime";
					line2.IndependentValuePath = "update";
					line2.BorderThickness = new Thickness(2);
					line2.BorderBrush = new SolidColorBrush(Colors.Orange);
					line2.Title = "Yesterday";
					ChartSpace.Series.Add(line2);
					currentLines.Add(line2);
				}
				catch (HttpRequestException ex)
				{
					var msg = new MessageDialog(resourceLoader.GetString("NetWorkErr") + ": " + ex.Message, resourceLoader.GetString("ErrHeader"));
					statusBar.ProgressIndicator.HideAsync();
					msg.ShowAsync();
				}
			}
			await statusBar.ProgressIndicator.HideAsync();
		}



		/// <summary>
		/// このページには、移動中に渡されるコンテンツを設定します。前のセッションからページを
		/// 再作成する場合は、保存状態も指定されます。
		/// </summary>
		/// <param name="sender">
		/// イベントのソース (通常、<see cref="NavigationHelper"/>)
		/// </param>
		/// <param name="e">このページが最初に要求されたときに
		/// <see cref="Frame.Navigate(Type, Object)"/> に渡されたナビゲーション パラメーターと、
		/// 前のセッションでこのページによって保存された状態の辞書を提供する
		/// イベント データ。ページに初めてアクセスするとき、状態は null になります。</param>
		private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
		{
		}

		/// <summary>
		/// アプリケーションが中断される場合、またはページがナビゲーション キャッシュから破棄される場合、
		/// このページに関連付けられた状態を保存します。値は、
		/// <see cref="SuspensionManager.SessionState"/> のシリアル化の要件に準拠する必要があります。
		/// </summary>
		/// <param name="sender">イベントのソース (通常、<see cref="NavigationHelper"/>)</param>
		/// <param name="e">シリアル化可能な状態で作成される空のディクショナリを提供するイベント データ
		///。</param>
		private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
		{
		}

		#region NavigationHelper の登録

		/// <summary>
		/// このセクションに示したメソッドは、NavigationHelper がページの
		/// ナビゲーション メソッドに応答できるようにするためにのみ使用します。
		/// <para>
		/// ページ固有のロジックは、
		/// <see cref="NavigationHelper.LoadState"/>
		/// および <see cref="NavigationHelper.SaveState"/>。
		/// LoadState メソッドでは、前のセッションで保存されたページの状態に加え、
		/// ナビゲーション パラメーターを使用できます。
		/// </para>
		/// </summary>
		/// <param name="e">ナビゲーション要求を取り消すことのできないナビゲーション メソッドおよびイベント
		/// ハンドラーにデータを提供します。</param>

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			this.navigationHelper.OnNavigatedFrom(e);
		}

		#endregion

		private void ReloadButtonTapped(object sender, TappedRoutedEventArgs e)
		{
			GetStatuses(currentAttraction);
		}
	}
}
