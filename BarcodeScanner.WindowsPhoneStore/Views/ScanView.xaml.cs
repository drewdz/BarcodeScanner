using BarcodeScanner.Core.ViewModels;
using BarcodeScanner.WindowsPhoneStore.Common;
using Cirrious.MvvmCross.WindowsCommon.Views;
namespace BarcodeScanner.WindowsPhoneStore.Views
{
    public sealed partial class ScanView : MvxWindowsPage
    {
        #region Fields
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        #endregion Fields

        #region Constructors

        public ScanView()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.Loaded += OnLoaded;

            Windows.Graphics.Display.DisplayInformation.AutoRotationPreferences = Windows.Graphics.Display.DisplayOrientations.Landscape;
        }

        void OnLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.ReadyCommand.Execute(Preview);
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

        public new ScanViewModel ViewModel
        {
            get { return (ScanViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        #endregion Properties

        #region Lifecycle

        protected override void OnNavigatingFrom(Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs e)
        {
            ViewModel.DoneCommand.Execute(null);
            Windows.Graphics.Display.DisplayInformation.AutoRotationPreferences = Windows.Graphics.Display.DisplayOrientations.None;
            base.OnNavigatingFrom(e);
        }

        #endregion Lifecycle
    }
}
