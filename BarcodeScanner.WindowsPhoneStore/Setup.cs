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
                        UserName = "mobiledev@outsurance.co.za",
                        Key = "57F2CA783E8C134F6D73A005442D5296B3DA8B2FFEE228A4305D6E51E684418A"
                    },
                    new Scanning.Entities.ScanningConfig 
                    {
                        Code = Scanning.Entities.ScanningConfig.MWB_CODE_MASK_QR,
                        UserName = "mobiledev@outsurance.co.za",
                        Key = "0A354C8D0FCAA8B2AF566E991DF73BF8CEBED981A0A4234096BCAB895A9BDF3E"
                    }
                };
            }
            return null;
        }
    }
}