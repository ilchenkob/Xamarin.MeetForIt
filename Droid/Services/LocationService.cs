
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LocationMonitor.Services;
using LocationMonitor.Models;
using Android.Locations;

namespace LocationMonitor.Droid.Services
{		
	public class LocationService : Java.Lang.Object, ILocationListener, ILocationService
	{
		private bool isStarted;

		#region ILocationService implementation
		public void Run ()
		{
			if (isStarted)
				return;
			
			string provider;
			if (locationManager.IsProviderEnabled (LocationManager.GpsProvider))
				provider = LocationManager.GpsProvider;
			else if (locationManager.IsProviderEnabled (LocationManager.NetworkProvider))
				provider = LocationManager.NetworkProvider;
			else
				return;
			
			locationManager.RequestLocationUpdates (
											provider,
											Config.GpsRefreshTimeMs,
											Config.GpsRefreshDistance,
											this);
			isStarted = true;
		}

		public void Stop ()
		{
			if (!isStarted)
				return;
			
			locationManager.RemoveUpdates (this);
			isStarted = false;
		}

		public event Action<LocationMonitor.Models.Location> LocationUpdated;
		#endregion

		#region ILocationListener implementation

		public void OnLocationChanged (Android.Locations.Location location)
		{
			var dateTime = DateTime.Now;
			Console.WriteLine("{0} - Location updated", dateTime);
			if (LocationUpdated != null)
			{
				LocationUpdated.Invoke (new LocationMonitor.Models.Location
				{
						Lat = location.Latitude,
						Lon = location.Longitude,
						DateTime = dateTime
				});
			}
		}

		public void OnProviderDisabled (string provider)
		{
		}

		public void OnProviderEnabled (string provider)
		{
		}

		public void OnStatusChanged (string provider, Availability status, Bundle extras)
		{
		}

		#endregion

		private LocationManager locationManager;

		public LocationService() : base()
		{
			locationManager = Application.Context.GetSystemService(Context.LocationService) as LocationManager;
		}
	}
}

