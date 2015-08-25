using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;

using Foundation;

using System;

using UIKit;

namespace BarcodeScanner.Touch.Table
{
    public class ScannerTableSource : MvxStandardTableViewSource
    {
        string cellIdentifier = "scanItem"; // set in the Storyboard

        public ScannerTableSource(UITableView tableView)
            : base(tableView)
        {
            tableView.RegisterClassForCellReuse(typeof(ScannerTableCell), ScannerTableCell.KEY);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return tableView.DequeueReusableCell(ScannerTableCell.KEY) as ScannerTableCell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 44;
        }
    }

    [Register("ScannerTableCell")]
    public class ScannerTableCell : MvxTableViewCell
    {
        #region Constants

        public const string KEY = "ScanerTableCell";

        #endregion Constants

        #region Fields

        #endregion Fields

        #region Constructors

        public ScannerTableCell(IntPtr handle)
            : base(handle)
        {
            InitBindings();
        }

        public ScannerTableCell()
            : base()
        {
            InitBindings();
        }

        #endregion Constructors

        #region Init

        private void InitBindings()
        {
            var set = this.CreateBindingSet<ScannerTableCell, Scanning.Entities.ScanItem>();
            set.Bind(this.TextLabel).To(vm => vm.Text);
            set.Apply();
        }

        #endregion Init
    }
}