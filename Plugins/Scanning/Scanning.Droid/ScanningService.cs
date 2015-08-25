using Android.App;
using MWBarcodeScanner;
using Scanning.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Scanning.Droid
{
    public class ScanningService : ScanningServiceBase, IScanningService, IScanSuccessCallback
    {
        #region Constants

        public static RectangleF RECT_FULL_1D = new RectangleF(6, 6, 88, 88);
        public static RectangleF RECT_FULL_2D = new RectangleF(20, 6, 60, 88);
        public static RectangleF RECT_DOTCODE = new RectangleF(30, 20, 40, 60);

        #endregion Constants

        #region Fields

        Scanner _Scanner;

        Action<List<ScanItem>> _CodeFound;
        Action<byte[]> _DataFound;

        Activity _Preview;

        #endregion Fields

        #region Constructors

        public ScanningService(ScanningConfigCollection config)
        {
            Initialize(config);
        }

        #endregion Constructors

        #region Initializations

        private void Initialize(ScanningConfigCollection config)
        {
            if ((config == null) || (config.Count == 0)) throw new Exception("No keys found for initializations.");
            int codes = 0;
            foreach (var c in config)
            {
                InitConfig(c);
                codes |= c.Code;
            }
            //  configure which codes to search for - less codes = faster
            BarcodeConfig.MWB_setActiveCodes(codes);
            // search in both directions
            BarcodeConfig.MWB_setDirection(BarcodeConfig.MWB_SCANDIRECTION_HORIZONTAL | BarcodeConfig.MWB_SCANDIRECTION_VERTICAL);

            // set decoder effort level (1 - 5)
            // for live scanning scenarios, a setting between 1 to 3 will suffice
            // levels 4 and 5 are typically reserved for batch scanning
            BarcodeConfig.MWB_setLevel(2);
        }

        private void InitConfig(ScanningConfig config)
        {
            //  register the plugin - must be done foreach not using the full bitmask
            if (config.Code == BarcodeConfig.MWB_CODE_MASK_39)
            {
                BarcodeConfig.MWB_registerCode(BarcodeConfig.MWB_CODE_MASK_39, config.UserName, config.Key);
                BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_39, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeConfig.MWB_CODE_MASK_93)
            {
                BarcodeConfig.MWB_registerCode(BarcodeConfig.MWB_CODE_MASK_93, config.UserName, config.Key);
                BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_93, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeConfig.MWB_CODE_MASK_25)
            {
                BarcodeConfig.MWB_registerCode(BarcodeConfig.MWB_CODE_MASK_25, config.UserName, config.Key);
                BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_25, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeConfig.MWB_CODE_MASK_128)
            {
                BarcodeConfig.MWB_registerCode(BarcodeConfig.MWB_CODE_MASK_128, config.UserName, config.Key);
                BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_128, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeConfig.MWB_CODE_MASK_AZTEC)
            {
                BarcodeConfig.MWB_registerCode(BarcodeConfig.MWB_CODE_MASK_AZTEC, config.UserName, config.Key);
                BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_AZTEC, RECT_FULL_2D);
            }
            else if (config.Code == BarcodeConfig.MWB_CODE_MASK_DM)
            {
                BarcodeConfig.MWB_registerCode(BarcodeConfig.MWB_CODE_MASK_DM, config.UserName, config.Key);
                BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_DM, RECT_FULL_2D);
            }
            else if (config.Code == BarcodeConfig.MWB_CODE_MASK_EANUPC)
            {
                BarcodeConfig.MWB_registerCode(BarcodeConfig.MWB_CODE_MASK_EANUPC, config.UserName, config.Key);
                BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_EANUPC, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeConfig.MWB_CODE_MASK_QR)
            {
                BarcodeConfig.MWB_registerCode(BarcodeConfig.MWB_CODE_MASK_QR, config.UserName, config.Key);
                BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_QR, RECT_FULL_2D);
            }
            else if (config.Code == BarcodeConfig.MWB_CODE_MASK_PDF)
            {
                BarcodeConfig.MWB_registerCode(BarcodeConfig.MWB_CODE_MASK_PDF, config.UserName, config.Key);
                BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_PDF, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeConfig.MWB_CODE_MASK_RSS)
            {
                BarcodeConfig.MWB_registerCode(BarcodeConfig.MWB_CODE_MASK_RSS, config.UserName, config.Key);
                BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_RSS, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeConfig.MWB_CODE_MASK_CODABAR)
            {
                BarcodeConfig.MWB_registerCode(BarcodeConfig.MWB_CODE_MASK_CODABAR, config.UserName, config.Key);
                BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_CODABAR, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeConfig.MWB_CODE_MASK_DOTCODE)
            {
                BarcodeConfig.MWB_registerCode(BarcodeConfig.MWB_CODE_MASK_DOTCODE, config.UserName, config.Key);
                BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_DOTCODE, RECT_DOTCODE);
            }
            else if (config.Code == BarcodeConfig.MWB_CODE_MASK_11)
            {
                BarcodeConfig.MWB_registerCode(BarcodeConfig.MWB_CODE_MASK_11, config.UserName, config.Key);
                BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_11, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeConfig.MWB_CODE_MASK_MSI)
            {
                BarcodeConfig.MWB_registerCode(BarcodeConfig.MWB_CODE_MASK_MSI, config.UserName, config.Key);
                BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_MSI, RECT_FULL_1D);
            }
        }

        #endregion Initialization

        #region Operations

        public override async Task BeginPreviewAsync(object preview)
        {
            if (_Scanner != null) return;
            if (!(preview is Activity)) return;
            _Scanner = new Scanner((Activity)preview);
        }

        public override async Task DetectAsync(Action<List<ScanItem>> codeFound)
        {
            if (_Scanner == null) throw new Exception("Scanner not initialized");
            if (codeFound == null) throw new Exception("There is no action to return results with");

            _CodeFound = codeFound;
            _DataFound = null;
            _Scanner.Scan(this);
        }

        public override async Task DetectDataAsync(Action<byte[]> dataFound)
        {
            if (_Scanner == null) throw new Exception("Scanner not initialized");
            if (dataFound == null) throw new Exception("There is no action to return results with");

            _CodeFound = null;
            _DataFound = dataFound;

            _Scanner.Scan(this);
        }

        #endregion Operations

        #region IScanSuccessCallback

        public void barcodeDetected(MWResult result)
        {
            if ((result == null) || ((_CodeFound == null) && (_DataFound == null))) return;

            if (_CodeFound != null)
            {
                List<ScanItem> list = DecodeScan(result.text.ToString());
                if ((list != null) && (list.Count > 0)) _CodeFound(list);
            }
            else
            {
                _DataFound(result.bytes);
            }
        }

        #endregion IScanSuccessCallback
    }
}