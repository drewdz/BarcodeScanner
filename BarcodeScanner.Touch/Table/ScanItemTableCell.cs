using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using Foundation;
using UIKit;

namespace BarcodeScanner.Touch.Table
{
    [Register("ScanItemTableViewCell")]
    public class ScanItemTableCell : MvxStandardTableViewCell
    {
        private readonly MvxImageViewLoader _imageViewLoader;

        public static readonly NSString Key = new NSString("ScannerTableViewCell");

        public ScanItemTableCell(IntPtr handle)
            : base("TitleText", UITableViewCellStyle.Subtitle, Key, UITableViewCellAccessory.DisclosureIndicator)
        {
            _imageViewLoader = new MvxImageViewLoader(() => ImageView);

            this.DetailTextLabel.Lines = 4;
            this.DetailTextLabel.LineBreakMode = UILineBreakMode.TailTruncation;
            this.ClipsToBounds = true;

            SetBindings();
        }

        private void SetBindings()
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ScanItemTableCell, Scanning.Entities.ScanItem>();

                set.Bind().For(c => c.TitleText).To(vm => vm.Id);
                set.Bind().For(c => c.DetailText).To(vm => vm.Text);

                set.Apply();
            });
        }

        public override UILabel DetailTextLabel
        {
            get
            {
                UILabel lbl = base.DetailTextLabel;
                lbl.TextColor = UIColor.Gray;
                if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
                {
                    lbl.Font = UIFont.SystemFontOfSize(12);
                }
                else
                {
                    lbl.Font = UIFont.SystemFontOfSize(17);
                }
                return lbl;
            }
        }

        public override UILabel TextLabel
        {
            get
            {
                UILabel lbl = base.TextLabel;
                if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
                {
                    lbl.Font = UIFont.SystemFontOfSize(14);
                }
                else
                {
                    lbl.Font = UIFont.SystemFontOfSize(22);
                }
                return lbl;
            }
        }


    }
}