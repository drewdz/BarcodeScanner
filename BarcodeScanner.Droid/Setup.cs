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
                        UserName = "mobiledev@outsurance.co.za",
                        Key = "8CE7B3D6121144EAEF0761106EFE414DF1EAF9BF041AE322A87ED6F05C039244"
                    },
                    new Scanning.Entities.ScanningConfig
                    {
                        Code = Scanning.Entities.ScanningConfig.MWB_CODE_MASK_PDF,
                        UserName = "mobiledev@outsurance.co.za",
                        Key = "CE7D9296AFF19F4AF931A7146B8D7BBE7DE3C574B4C0EC782179A95B210CE357"
                    }
                };
            }
            return null;
        }
    }
}