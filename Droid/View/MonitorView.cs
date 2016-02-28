using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using GalaSoft.MvvmLight;
using LocationMonitor.ViewModel;
using GalaSoft.MvvmLight.Helpers;

namespace LocationMonitor.Droid.View
{
	[Activity (
		Label = "Location Monitor",
		ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,
		MainLauncher = true,
		Icon = "@drawable/icon")]
	public class MonitorView : Activity
	{
		private MonitorViewModel viewModel;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			viewModel = LocationMonitorApplication.ViewModelLocator.Monitor;

			SetContentView (Resource.Layout.LocationMonitor);

			var txtLat = FindViewById<TextView> (Resource.Id.txtLat);
			var txtLon = FindViewById<TextView> (Resource.Id.txtLon);
			var txtUpdatedAt = FindViewById<TextView> (Resource.Id.txtUpdatedAt);
			var btnStart = FindViewById<TextView> (Resource.Id.btnStart);
			var btnStop = FindViewById<TextView> (Resource.Id.btnStop);

			viewModel.SetBinding(() => viewModel.Lat, txtLat, () => txtLat.Text);
			viewModel.SetBinding(() => viewModel.Lon, txtLon, () => txtLon.Text);
			viewModel.SetBinding(() => viewModel.UpdatedAt, txtUpdatedAt, () => txtLon.Text);

			btnStart.SetCommand ("Click", viewModel.StartCommand);
			btnStop.SetCommand ("Click", viewModel.StopCommand);
		}
	}
}
