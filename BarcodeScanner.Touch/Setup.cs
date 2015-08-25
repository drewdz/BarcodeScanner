using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Touch.Platform;
using UIKit;

namespace BarcodeScanner.Touch
{
	public class Setup : MvxTouchSetup
	{
		public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			return new Core.App();
		}
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override Cirrious.CrossCore.Plugins.IMvxPluginConfiguration GetPluginConfiguration(System.Type plugin)
        {
            if (plugin == typeof(Scanning.Touch.Plugin))
            {
                return new Scanning.Entities.ScanningConfigCollection
                {
                    new Scanning.Entities.ScanningConfig 
                    {
                        Code = Scanning.Entities.ScanningConfig.MWB_CODE_MASK_QR,
                        UserName = "your_user_name",
                        Key = "your_key"
                    },
                    new Scanning.Entities.ScanningConfig
                    {
                        Code = Scanning.Entities.ScanningConfig.MWB_CODE_MASK_PDF,
                        UserName = "your_user_name",
                        Key = "your_key"
                    }
                };
            }
            return null;
        }

        protected override Cirrious.MvvmCross.Touch.Views.IMvxTouchViewsContainer CreateTouchViewsContainer()
        {
            return new TouchViewsContainer();
        }
	}
}