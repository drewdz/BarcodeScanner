using BarcodeScanner.Core.ViewModels;
using BarcodeScanner.Touch.Table;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Touch.Views;

using Foundation;

using System;

using UIKit;

namespace BarcodeScanner.Touch.Views
{
    public partial class ScannerView : MvxTableViewController
    {
        #region Constructors

        public ScannerView(IntPtr handle)
            : base(handle)
        {
        }

        #endregion Constructors

        #region Properties

        public new ScannerViewModel ViewModel
        {
            get { return (ScannerViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        #endregion Properties

        #region Lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            MvxTableViewSource source = new ScannerTableSource(TableView);

            var set = this.CreateBindingSet<ScannerView, ScannerViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Bind(btnScan).To(vm => vm.ScanCommand).CommandParameter(this.NavigationController);
            set.Apply();

            TableView.Source = source;
            TableView.ReloadData();
        }

        #endregion Lifecycle
    }
}