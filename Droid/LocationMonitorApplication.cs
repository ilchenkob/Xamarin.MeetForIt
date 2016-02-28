using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using LocationMonitor.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using LocationMonitor.Services;
using LocationMonitor.Droid.Services;

namespace LocationMonitor.Droid
{
	[Application(Label = "LocationMonitor", Name = "com.meetforit.locationmonitor.LocationMonitorApplication")]
	public class LocationMonitorApplication : Application
	{
		public static ViewModelLocator ViewModelLocator { get; private set; }

		public LocationMonitorApplication(System.IntPtr ptr, Android.Runtime.JniHandleOwnership ownership) : base(ptr, ownership)
		{
		}

		public override void OnCreate()
		{
			base.OnCreate();

			SimpleIoc.Default.Register<ILocationService, LocationService>();

			ViewModelLocator = new ViewModelLocator();
		}
	}
}
