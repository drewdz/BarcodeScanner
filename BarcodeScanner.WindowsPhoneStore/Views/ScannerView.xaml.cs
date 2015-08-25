using BarcodeScanner.Core.ViewModels;
using BarcodeScanner.WindowsPhoneStore.Common;

using Cirrious.MvvmCross.WindowsCommon.Views;

using BarcodeScanner.WindowsPhoneStore.Extensions;

namespace BarcodeScanner.WindowsPhoneStore.Views
{
    public sealed partial class ScannerView : MvxWindowsPage
    {
        #region Fields

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        #endregion Fields

        #region Constructors

        public ScannerView()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
        }

        #endregion Constructors

        #region Properties

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public new ScannerViewModel ViewModel
        {
            get { return (ScannerViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        #endregion Properties

        #region Lifecycle

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            this.BackStackClear();
            base.OnNavigatedTo(e);
        }

        #endregion Lifecycle
    }
}
