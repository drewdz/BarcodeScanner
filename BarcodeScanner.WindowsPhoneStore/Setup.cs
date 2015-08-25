using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsCommon.Platform;

using Windows.UI.Xaml.Controls;

namespace BarcodeScanner.WindowsPhoneStore
{
    public class Setup : MvxWindowsSetup
    {
        public Setup(Frame rootFrame) : base(rootFrame)
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
            if (plugin == typeof(Scanning.WindowsPhoneStore.Plugin))
            {
                return new Scanning.Entities.ScanningConfigCollection
                {
                    new Scanning.Entities.ScanningConfig
                    {
                        Code = Scanning.Entities.ScanningConfig.MWB_CODE_MASK_PDF,
                        UserName = "your_user_name",
                        Key = "your_key"
                    },
                    new Scanning.Entities.ScanningConfig 
                    {
                        Code = Scanning.Entities.ScanningConfig.MWB_CODE_MASK_QR,
                        UserName = "your_user_name",
                        Key = "your_key"
                    }
                };
            }
            return null;
        }
    }
}