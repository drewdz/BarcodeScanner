using Cirrious.MvvmCross.ViewModels;

using Scanning;

using System.Windows.Input;

namespace BarcodeScanner.Core.ViewModels
{
    public class ScanViewModel : MvxViewModel
    {
        #region Fields

        private IScanningService _ScanningService;

        private object _Context;

        #endregion Fields

        #region Constructors

        public ScanViewModel(IScanningService scannerService)
        {
            _ScanningService = scannerService;
        }

        #endregion Constructors

        #region Commands

        private ICommand _ReadyCommand = null;
        public ICommand ReadyCommand
        {
            get
            {
                if (_ReadyCommand == null)
                {
                    _ReadyCommand = new MvxCommand<object>((context) =>
                    {
                        _Context = context;
                        //  begin preview
                        _ScanningService.BeginPreviewAsync(_Context);
                        //  scan
                        _ScanningService.DetectAsync((results) =>
                        {
                            _ScanningService.StopScanning();
                            ShowViewModel<ScannerViewModel>();
                        });
                    });
                }
                return _ReadyCommand;
            }
        }

        private ICommand _DoneCommand = null;
        public ICommand DoneCommand
        {
            get
            {
                if (_DoneCommand == null)
                {
                    _DoneCommand = new MvxCommand(() => _ScanningService.StopScanning());
                }
                return _DoneCommand;
            }
        }

        #endregion Commands
    }
}
