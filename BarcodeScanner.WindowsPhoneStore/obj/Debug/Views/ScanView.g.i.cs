﻿

#pragma checksum "C:\Code\BarcodeScanner\BarcodeScanner.WindowsPhoneStore\Views\ScanView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A5965DEE70D6558C4D52A1F122336ECE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BarcodeScanner.WindowsPhoneStore.Views
{
    partial class ScanView : global::Cirrious.MvvmCross.WindowsCommon.Views.MvxWindowsPage
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Cirrious.MvvmCross.WindowsCommon.Views.MvxWindowsPage TheView; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Image Preview; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Canvas Overlay; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            global::Windows.UI.Xaml.Application.LoadComponent(this, new global::System.Uri("ms-appx:///Views/ScanView.xaml"), global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            TheView = (global::Cirrious.MvvmCross.WindowsCommon.Views.MvxWindowsPage)this.FindName("TheView");
            Preview = (global::Windows.UI.Xaml.Controls.Image)this.FindName("Preview");
            Overlay = (global::Windows.UI.Xaml.Controls.Canvas)this.FindName("Overlay");
        }
    }
}



