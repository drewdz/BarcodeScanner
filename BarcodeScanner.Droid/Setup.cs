using Android.Content;

using Cirrious.CrossCore.Platform;
using Cirrious.CrossCore.Plugins;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;

namespace BarcodeScanner.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
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

        protected override IMvxPluginConfiguration GetPluginConfiguration(System.Type plugin)
        {
            if (plugin == typeof(Scanning.Droid.Plugin))
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
    }
}