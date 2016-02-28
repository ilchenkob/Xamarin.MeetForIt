using GalaSoft.MvvmLight;
using LocationMonitor.Services;
using GalaSoft.MvvmLight.Command;

namespace LocationMonitor.ViewModel
{
    public class MonitorViewModel : ViewModelBase
    {
		private ILocationService locationService;

		public MonitorViewModel(ILocationService locationService)
        {
			StartCommand = new RelayCommand (startExecute);
			StopCommand = new RelayCommand (stopExecute);

			UpdatedAt = "";
			
			this.locationService = locationService;
			this.locationService.LocationUpdated += data =>
			{
				Lat = data.Lat;
				Lon = data.Lon;
				UpdatedAt = data.DateTime.ToString("hh:mm:ss");
			};
        }

		public RelayCommand StartCommand { get; private set; }

		public RelayCommand StopCommand { get; private set; }

		private double lat;
		public double Lat
		{
			get { return lat; }
			set
			{
				lat = value;
				RaisePropertyChanged (() => Lat);
			}
		}

		private double lon;
		public double Lon
		{
			get { return lon; }
			set
			{
				lon = value;
				RaisePropertyChanged (() => Lon);
			}
		}

		private string updatedAt;
		public string UpdatedAt
		{
			get { return updatedAt; }
			set
			{
				updatedAt = value;
				RaisePropertyChanged (() => UpdatedAt);
			}
		}

		private void startExecute()
		{
			locationService.Run();
		}

		private void stopExecute()
		{
			locationService.Stop();
		}
    }
}