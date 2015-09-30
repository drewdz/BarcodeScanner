# BarcodeScanner
Cross platform app for barcode scanning using ManateeWorks

1. Make sure you have a working copy of Xamarin (Android and iOS) installed
2. Download and open the code using Visual Studio (I use VS 2013 Premium)
3. Restore missing Nu-Get packages and components
    - MVVM Cross - NuGet
    - ManateeWorks - Xamarin Component
4. Get an evaluation key from Manatee Works for the types of barcodes you want to scan (I needed to scan QR and PDF417 codes)
5. I would not recommend using an emulator for this - use a real device. You will need a Mac build host for this (for iOS).  Also your iOS devices will need proper provisioning profiles etc.

