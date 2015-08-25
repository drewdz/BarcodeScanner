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
                        UserName = "mobiledev@outsurance.co.za",
                        Key = "E9C1EAD4DCE87631BFCA0FBB6DF54D23D22FC004CB46881EC0E65C0D2B513285"
                    },
                    new Scanning.Entities.ScanningConfig
                    {
                        Code = Scanning.Entities.ScanningConfig.MWB_CODE_MASK_PDF,
                        UserName = "OUTsurance.PDF.iOS.UDL",
                        Key = "55B500FBC6F7AB88E1785843076818DBC6AC2FD728AEF9EF3D46B5DFA956D2FC"
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