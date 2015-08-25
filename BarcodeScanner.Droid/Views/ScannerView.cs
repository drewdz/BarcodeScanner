using Android.App;
using Android.OS;
using Android.Views;
using BarcodeScanner.Core.ViewModels;
using Cirrious.MvvmCross.Droid.Views;

namespace BarcodeScanner.Droid.Views
{
    [Activity(Label = "Barcode Scanner")]
    public class ScannerView : MvxActivity, MWBarcodeScanner.IScanSuccessCallback
    {
        #region Properties

        public new ScannerViewModel ViewModel
        {
            get { return (ScannerViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        #endregion Properties

        #region Lifecycle

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ScannerLayout);
            ViewModel.ReadyCommand.Execute(this);
        }

        #endregion Lifecycle

        #region Menu

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.ScanMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId.Equals(Resource.Id.Scan))
            {
                ViewModel.ScanCommand.Execute(null);
                return true;
            }
            return false;
        }

        #endregion Menu

        public void barcodeDetected(MWBarcodeScanner.MWResult result)
        {
        }
    }
}