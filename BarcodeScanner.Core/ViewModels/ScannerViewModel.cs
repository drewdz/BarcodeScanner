using Cirrious.MvvmCross.ViewModels;

using Scanning;
using Scanning.Entities;

using System.Collections.Generic;
using System.Windows.Input;

namespace BarcodeScanner.Core.ViewModels
{
    public class ScannerViewModel : MvxViewModel
    {
        #region Fields

        private readonly IScanningService _ScanningService;

        private object _Context;

        #endregion Fields

        #region Constructors

        public ScannerViewModel(IScanningService scanningService)
        {
            _ScanningService = scanningService;
        }

        #endregion Constructors

        #region Properties

        private List<ScanItem> _Items = null;
        public List<ScanItem> Items
        {
            get { return _Items; }
            set { _Items = value; RaisePropertyChanged(() => Items); }
        }

        private ScanItem _SelectedItem = null;
        public ScanItem SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                if (_SelectedItem != null) _SelectedItem.Selected = false;
                _SelectedItem = value;
                _SelectedItem.Selected = true;
                RaisePropertyChanged(() => SelectedItem);
            }
        }

        #endregion Properties

        #region Commands

        private ICommand _ScanCommand = null;
        public ICommand ScanCommand
        {
            get
            {
                if (_ScanCommand == null)
                {
                    _ScanCommand = new MvxCommand<object>(async (context) =>
                    {
                        if (_ScanningService.Encapsulated)
                        {
                            _Context = context;
                            await _ScanningService.BeginPreviewAsync(_Context);
                            _ScanningService.DetectAsync((results) => Items = results);
                        }
                        else
                        {
                            ShowViewModel<ScanViewModel>();
                        }
                    });
                }
                return _ScanCommand;
            }
        }

        #endregion Commands

        #region Private Methods

        public override void Start()
        {
            if (_ScanningService.ScanResults != null) Items = _ScanningService.ScanResults;

            base.Start();
        }

        #endregion Private Methods
    }
}
