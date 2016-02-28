using System;
using UIKit;
using LocationMonitor.ViewModel;
using CoreGraphics;
using GalaSoft.MvvmLight.Helpers;

namespace LocationMonitor.iOS.View
{
	public class MonitorView : UIViewController
	{
		private MonitorViewModel viewModel;

		private UILabel txtLat;
		private UILabel txtLon;
		private UILabel txtUpdatedAt;

		private UIButton btnStart;
		private UIButton btnStop;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad();

			viewModel = AppDelegate.ViewModelLocator.Monitor;

			createView();

			viewModel.SetBinding(() => viewModel.Lat, txtLat, () => txtLat.Text);
			viewModel.SetBinding(() => viewModel.Lon, txtLon, () => txtLon.Text);
			viewModel.SetBinding(() => viewModel.UpdatedAt, txtUpdatedAt, () => txtUpdatedAt.Text);

			btnStart.SetCommand ("TouchUpInside", viewModel.StartCommand);
			btnStop.SetCommand ("TouchUpInside", viewModel.StopCommand);
		}

		private void createView()
		{
			int paddingTop = 30;
			int padding = 4;
			int controlWidth = (int)View.Frame.Width;
			int contentHeight = 20;

			View.BackgroundColor = UIColor.White;

			var labelLat = new UILabel (new CGRect (padding, paddingTop + padding, controlWidth, contentHeight)) {
				TextColor = UIColor.DarkTextColor,
				TextAlignment = UITextAlignment.Center,
				Font = UIFont.SystemFontOfSize(15.0f),
				Text = "Lat"
			};

			txtLat = new UILabel (new CGRect (padding, paddingTop + padding * 3 + contentHeight, controlWidth, contentHeight))
			{
				TextColor = UIColor.DarkTextColor,
				TextAlignment = UITextAlignment.Center,
				Font = UIFont.BoldSystemFontOfSize(15.0f),
			};

			var labelLon = new UILabel (new CGRect (padding, paddingTop + padding * 5 + contentHeight * 2, controlWidth, contentHeight)) {
				TextColor = UIColor.DarkTextColor,
				TextAlignment = UITextAlignment.Center,
				Font = UIFont.SystemFontOfSize(15.0f),
				Text = "Lon"
			};

			txtLon = new UILabel (new CGRect (padding, paddingTop + padding * 7 + contentHeight * 3, controlWidth, contentHeight))
			{
				TextColor = UIColor.DarkTextColor,
				TextAlignment = UITextAlignment.Center,
				Font = UIFont.BoldSystemFontOfSize(15.0f),
			};

			var labelUpdatedAt = new UILabel (new CGRect (padding, paddingTop + padding * 9 + contentHeight * 4, controlWidth, contentHeight)) {
				TextColor = UIColor.DarkTextColor,
				TextAlignment = UITextAlignment.Center,
				Font = UIFont.SystemFontOfSize(15.0f),
				Text = "Updated At"
			};

			txtUpdatedAt = new UILabel (new CGRect (padding, paddingTop + padding * 11 + contentHeight * 5, controlWidth, contentHeight))
			{
				TextColor = UIColor.DarkTextColor,
				TextAlignment = UITextAlignment.Center,
				Font = UIFont.BoldSystemFontOfSize(15.0f),
			};

			var btnRect = new CGRect (0, paddingTop + padding * 14 + contentHeight * 7, controlWidth, contentHeight * 2);
			btnStart = new UIButton (new CGRect(btnRect.X, btnRect.Y, (int)btnRect.Width / 2 - padding, btnRect.Height))
			{
				BackgroundColor = UIColor.FromRGB(245, 245, 245),
				Font = UIFont.SystemFontOfSize(15.0f),
			};
			btnStart.SetTitle ("Start", UIControlState.Normal);
			btnStart.SetTitleColor (UIColor.Blue, UIControlState.Normal);

			btnStop = new UIButton (new CGRect ((int)btnRect.Width / 2 + padding, btnRect.Y, (int)btnRect.Width/2, btnRect.Height))
			{
				BackgroundColor = UIColor.FromRGB(245, 245, 245),
				Font = UIFont.SystemFontOfSize(15.0f),
			};
			btnStop.SetTitle ("Stop", UIControlState.Normal);
			btnStop.SetTitleColor (UIColor.Blue, UIControlState.Normal);

			View.AddSubviews (new UIView[]
			{
				labelLat,
				txtLat,
				labelLon,
				txtLon,
				labelUpdatedAt,
				txtUpdatedAt,
				btnStart,
				btnStop
			});
		}
	}
}

