using System;
using LocationMonitor.Services;
using CoreLocation;
using UIKit;
using LocationMonitor.Models;

namespace LocationMonitor.iOS.Services
{
	public class LocationService : ILocationService
	{
		private bool isStarted;

		#region ILocationService implementation
		public void Run ()
		{
			if (isStarted)
				return;
			
			if (CLLocationManager.LocationServicesEnabled)
			{
				locationManager.LocationsUpdated += locationUpdated;
				locationManager.StartUpdatingLocation();
				isStarted = true;
			}
		}

		public void Stop ()
		{
			if (!isStarted)
				return;
			
			if (CLLocationManager.LocationServicesEnabled)
			{
				locationManager.StopUpdatingLocation();
				locationManager.LocationsUpdated -= locationUpdated;
				isStarted = false;
			}
		}

		public event Action<Location> LocationUpdated;
		#endregion

		private CLLocationManager locationManager;

		public LocationService()
		{
			locationManager = new CLLocationManager ();
			this.locationManager.PausesLocationUpdatesAutomatically = false; 

			// iOS 8 has additional permissions requirements
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				locationManager.RequestAlwaysAuthorization (); // works in background
				//locationManager.RequestWhenInUseAuthorization (); // only in foreground
			}

			if (UIDevice.CurrentDevice.CheckSystemVersion (9, 0)) {
				locationManager.AllowsBackgroundLocationUpdates = true;
			}

			locationManager.DesiredAccuracy = (double)Config.GpsRefreshDistance;
		}

		void locationUpdated (object sender, CLLocationsUpdatedEventArgs e)
		{
			var geoData = e.Locations [e.Locations.Length - 1];
			if (LocationUpdated != null)
				UIApplication.SharedApplication.InvokeOnMainThread (() =>
					LocationUpdated.Invoke (new Location {
						Lat = geoData.Coordinate.Latitude,
						Lon = geoData.Coordinate.Longitude,
						DateTime = DateTime.Now
					}));
		}
	}
}

