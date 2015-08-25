using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;
using UIKit;

namespace BarcodeScanner.Touch.Table
{
    public class ScanItemTableViewSource : MvxStandardTableViewSource
    {
        public ScanItemTableViewSource(UITableView tableView)
            : base(tableView)
        {
            tableView.RegisterClassForCellReuse(typeof(ScanItemTableCell), ScanItemTableCell.Key);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return tableView.DequeueReusableCell(ScanItemTableCell.Key) as ScanItemTableCell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 96;
        }
    }
}