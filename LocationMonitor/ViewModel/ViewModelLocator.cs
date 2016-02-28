/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:LocationMonitor"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace LocationMonitor.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			SimpleIoc.Default.Register<MonitorViewModel>();
        }

		public MonitorViewModel Monitor
        {
            get
            {
				return ServiceLocator.Current.GetInstance<MonitorViewModel>();
            }
        }
    }
}