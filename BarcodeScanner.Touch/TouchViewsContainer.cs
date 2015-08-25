using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.ViewModels;

using Foundation;

using System;

using UIKit;

namespace BarcodeScanner.Touch
{
    public class TouchViewsContainer : MvxTouchViewsContainer
    {
        protected override IMvxTouchView CreateViewOfType(Type viewType, MvxViewModelRequest request)
        {
            // Get storyboard using the View name that is passed. This will only work if the Storyboard ID is set on the .storyboard file
            // If we move to unified storyboards this will not be necessary
            var sb = UIStoryboard.FromName(string.Format("{0}_{1}", viewType.Name, UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone ? "iPhone" : "iPad"), NSBundle.MainBundle);
            var vc = sb.InstantiateViewController(viewType.Name);
            return vc as IMvxTouchView;
        }
    }
}