using Cirrious.CrossCore;
using Lumia.Imaging;
using Lumia.Imaging.Adjustments;
using Lumia.Imaging.Transforms;
using MW;
using Scanning.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Media.Devices;
using Windows.Media.MediaProperties;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Scanning.WindowsPhoneStore
{
    public class ScanningService : ScanningServiceBase, IScanningService
    {
        #region Constants

        const long MAX_RESOLUTION = 640 * 480;

        #endregion Constants

        #region Fields

        public static Rect RECT_FULL_1D = new Rect(2, 2, 96, 96);
        public static Rect RECT_FULL_2D = new Rect(20, 5, 60, 90);
        public static Rect RECT_DOTCODE = new Rect(30, 20, 40, 60);

        WriteableBitmap _Image;
        WriteableBitmapRenderer _Render;
        VideoDeviceController _Controller;
        CameraPreviewImageSource _Source;
        Image _ImagePreview;
        FilterEffect _Effects;

        bool _Ready = false;
        bool _Done = false;
        bool _Rendering = false;
        bool _Saved = false;

        int _Threads = 0;
        int _MaxThreads = 5;

        Action<List<ScanItem>> _CodesFound;
        Action<byte[]> _DataFound;

        Timer _Timer;
        List<byte[]> _ImageData;
        int _Width = 0, _Height = 0;

        #endregion Fields

        #region Constructors

        public ScanningService(ScanningConfigCollection config)
        {
            Initialize(config);
        }

        ~ScanningService()
        {
            Cleanup();
        }

        #endregion Constructors

        #region Properties

        public bool Encapsulated { get { return false; } }

        #endregion Properties

        #region Initializations

        private async void Initialize(ScanningConfigCollection config)
        {
            if (config == null) throw new Exception("No keys found for initializations.");

            int codes = 0;
            foreach (var c in config)
            {
                InitConfig(c);
                codes |= c.Code;
            }
            //  configure which codes to search for - less codes = faster
            BarcodeLib.Scanner.MWBsetActiveCodes(codes);

            // set decoder effort level (1 - 5)
            // for live scanning scenarios, a setting between 1 to 3 will suffice
            // levels 4 and 5 are typically reserved for batch scanning
            BarcodeLib.Scanner.MWBsetLevel(2);

            _Timer = new Timer(ProcessStart, null, 10, 10);
        }

        private async Task InitCamera()
        {
            var devices = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(Windows.Devices.Enumeration.DeviceClass.VideoCapture);
            string deviceId = devices.FirstOrDefault(d => d.EnclosureLocation.Panel != Windows.Devices.Enumeration.Panel.Front).Id;

            _Source = new CameraPreviewImageSource();
            await _Source.InitializeAsync(deviceId);
            _Source.PreviewFrameAvailable += OnPreviewFrameAvailable;

            //  set auto focus
            _Controller = (VideoDeviceController)_Source.VideoDeviceController;
            if (_Controller.FocusControl.Supported)
            {
                try
                {
                    if (_Controller.FocusControl.WaitForFocusSupported)
                    {
                        _Controller.FocusControl.Configure(new FocusSettings { Mode = FocusMode.Continuous });
                    }
                    else
                    {
                        _Controller.FocusControl.Configure(new FocusSettings { Mode = FocusMode.Auto });
                    }
                }
                catch
                {
                    _Controller.FocusControl.Configure(new FocusSettings { Mode = FocusMode.Auto });
                }
            }

            if (_Controller.FlashControl.Supported) _Controller.FlashControl.Auto = true;
            if (_Controller.ExposureControl.Supported) _Controller.ExposureControl.SetAutoAsync(true);

            _Image = new WriteableBitmap((int)Window.Current.Bounds.Width, (int)Window.Current.Bounds.Height);
            //  filters
            _Effects = new FilterEffect(_Source);
            _Effects.Filters = new IFilter[] { new GrayscaleFilter() };

            _Render = new WriteableBitmapRenderer(_Effects, _Image);
            _Ready = true;
        }

        private void InitConfig(ScanningConfig config)
        {
            //  register the plugin - must be done foreach not using the full bitmask
            if (config.Code == BarcodeLib.Scanner.MWB_CODE_MASK_39)
            {
                BarcodeLib.Scanner.MWBregisterCode(BarcodeLib.Scanner.MWB_CODE_MASK_39, config.UserName, config.Key);
                MWBsetScanningRect(BarcodeLib.Scanner.MWB_CODE_MASK_39, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeLib.Scanner.MWB_CODE_MASK_93)
            {
                BarcodeLib.Scanner.MWBregisterCode(BarcodeLib.Scanner.MWB_CODE_MASK_93, config.UserName, config.Key);
                MWBsetScanningRect(BarcodeLib.Scanner.MWB_CODE_MASK_93, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeLib.Scanner.MWB_CODE_MASK_25)
            {
                BarcodeLib.Scanner.MWBregisterCode(BarcodeLib.Scanner.MWB_CODE_MASK_25, config.UserName, config.Key);
                MWBsetScanningRect(BarcodeLib.Scanner.MWB_CODE_MASK_25, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeLib.Scanner.MWB_CODE_MASK_128)
            {
                BarcodeLib.Scanner.MWBregisterCode(BarcodeLib.Scanner.MWB_CODE_MASK_128, config.UserName, config.Key);
                MWBsetScanningRect(BarcodeLib.Scanner.MWB_CODE_MASK_128, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeLib.Scanner.MWB_CODE_MASK_AZTEC)
            {
                BarcodeLib.Scanner.MWBregisterCode(BarcodeLib.Scanner.MWB_CODE_MASK_AZTEC, config.UserName, config.Key);
                MWBsetScanningRect(BarcodeLib.Scanner.MWB_CODE_MASK_AZTEC, RECT_FULL_2D);
            }
            else if (config.Code == BarcodeLib.Scanner.MWB_CODE_MASK_DM)
            {
                BarcodeLib.Scanner.MWBregisterCode(BarcodeLib.Scanner.MWB_CODE_MASK_DM, config.UserName, config.Key);
                MWBsetScanningRect(BarcodeLib.Scanner.MWB_CODE_MASK_DM, RECT_FULL_2D);
            }
            else if (config.Code == BarcodeLib.Scanner.MWB_CODE_MASK_EANUPC)
            {
                BarcodeLib.Scanner.MWBregisterCode(BarcodeLib.Scanner.MWB_CODE_MASK_EANUPC, config.UserName, config.Key);
                MWBsetScanningRect(BarcodeLib.Scanner.MWB_CODE_MASK_EANUPC, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeLib.Scanner.MWB_CODE_MASK_QR)
            {
                BarcodeLib.Scanner.MWBregisterCode(BarcodeLib.Scanner.MWB_CODE_MASK_QR, config.UserName, config.Key);
                MWBsetScanningRect(BarcodeLib.Scanner.MWB_CODE_MASK_QR, RECT_FULL_2D);
            }
            else if (config.Code == BarcodeLib.Scanner.MWB_CODE_MASK_PDF)
            {
                BarcodeLib.Scanner.MWBregisterCode(BarcodeLib.Scanner.MWB_CODE_MASK_PDF, config.UserName, config.Key);
                MWBsetScanningRect(BarcodeLib.Scanner.MWB_CODE_MASK_PDF, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeLib.Scanner.MWB_CODE_MASK_RSS)
            {
                BarcodeLib.Scanner.MWBregisterCode(BarcodeLib.Scanner.MWB_CODE_MASK_RSS, config.UserName, config.Key);
                MWBsetScanningRect(BarcodeLib.Scanner.MWB_CODE_MASK_RSS, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeLib.Scanner.MWB_CODE_MASK_CODABAR)
            {
                BarcodeLib.Scanner.MWBregisterCode(BarcodeLib.Scanner.MWB_CODE_MASK_CODABAR, config.UserName, config.Key);
                MWBsetScanningRect(BarcodeLib.Scanner.MWB_CODE_MASK_CODABAR, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeLib.Scanner.MWB_CODE_MASK_DOTCODE)
            {
                BarcodeLib.Scanner.MWBregisterCode(BarcodeLib.Scanner.MWB_CODE_MASK_DOTCODE, config.UserName, config.Key);
                MWBsetScanningRect(BarcodeLib.Scanner.MWB_CODE_MASK_DOTCODE, RECT_DOTCODE);
            }
            else if (config.Code == BarcodeLib.Scanner.MWB_CODE_MASK_11)
            {
                BarcodeLib.Scanner.MWBregisterCode(BarcodeLib.Scanner.MWB_CODE_MASK_11, config.UserName, config.Key);
                MWBsetScanningRect(BarcodeLib.Scanner.MWB_CODE_MASK_11, RECT_FULL_1D);
            }
            else if (config.Code == BarcodeLib.Scanner.MWB_CODE_MASK_MSI)
            {
                BarcodeLib.Scanner.MWBregisterCode(BarcodeLib.Scanner.MWB_CODE_MASK_MSI, config.UserName, config.Key);
                MWBsetScanningRect(BarcodeLib.Scanner.MWB_CODE_MASK_MSI, RECT_FULL_1D);
            }
        }

        #endregion Initializations

        #region Operations

        public override async Task DetectAsync(Action<List<ScanItem>> codeFound)
        {
            _Done = false;
            //  wait for ready
            await WaitForReadyAsync(2000);

            _CodesFound = codeFound;

            System.Diagnostics.Debug.WriteLine("Scanning");

        }

        public override async Task DetectDataAsync(Action<byte[]> dataFound)
        {
            _Done = false;
            //  wait for ready
            await WaitForReadyAsync(2000);

            _DataFound = dataFound;
        }

        public override async Task BeginPreviewAsync(object preview)
        {
            if (!(preview is Image)) return;

            await InitCamera();
            _Rendering = false;

            _ImagePreview = (Image)preview;
            _ImagePreview.Source = _Image;
            _Source.StartPreviewAsync();
        }

        public async Task WaitForReadyAsync(int timeoutMs)
        {
            var now = DateTime.Now;
            while (!_Ready)
            {
                if (DateTime.Now.Subtract(now).TotalMilliseconds > timeoutMs) break;
                await Task.Delay(100);
            }
        }

        public override async Task StopScanning()
        {
            _Done = true;
            _Ready = false;

            //  wait for render cycle to end
            DateTime start = DateTime.Now;
            while (_Rendering)
            {
                if (DateTime.Now.Subtract(start).TotalMilliseconds > 1000) break;
                await Task.Delay(10);
            }

            await _Source.StopPreviewAsync();
            Cleanup();
        }

        #endregion Operations

        #region Private Methods

        private async void FindCodes(byte[] data, int width, int height)
        {
            byte[] bytes = new byte[data.Length / 4];
            System.Diagnostics.Debug.WriteLine("Refining Data ({0}. {1}, {2})", data.Length, width, height);
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = data[i * 4];
            }

            System.Diagnostics.Debug.WriteLine("Finding Codes {0}", _Threads);
            byte[] resultData = new byte[10000];

            int result = BarcodeLib.Scanner.MWBscanGrayscaleImage(bytes, Convert.ToInt32(width), Convert.ToInt32(height), resultData);
            if ((result < 0) || _Done)
            {
                System.Diagnostics.Debug.WriteLine("Scan Result: {0} - Fail", result);
                resultData = null;
                _Threads--;
                return;
            }

            string resultText = string.Empty;
            var type = BarcodeLib.Scanner.MWBgetResultType();
            System.Diagnostics.Debug.WriteLine("Scan Result type: {0}", type);
            if (BarcodeLib.Scanner.MWBgetResultType() < 0)
            {
                System.Diagnostics.Debug.WriteLine("Scan Result - Fail: {0}", type);
                resultData = null;
                _Threads--;
                return;
            }
            if (type == 2)
            {
                MWResults results = new MWResults(resultData);
                if (results.count == 0)
                {
                    var s = System.Text.Encoding.UTF8.GetString(resultData, 0, resultData.Length);
                    System.Diagnostics.Debug.WriteLine("No Data.");
                    System.Diagnostics.Debug.WriteLine(s);
                    resultData = null;
                    _Threads--;
                    return;
                }
                MWResult scanResult = results.getResult(0);
                resultText = scanResult.text;
                scanResult = null;
            }
            else
            {
                resultText = System.Text.Encoding.UTF8.GetString(resultData, 0, resultData.Length);
            }

            //  return the result
            if (!string.IsNullOrEmpty(resultText))
            {
                _Done = true;

                if (_CodesFound != null)
                {
                    Utilities.Dispatch(() =>
                    {
                        ScanResults = DecodeScan(resultText);
                        _CodesFound(ScanResults);
                    });
                }
                else if (_DataFound != null)
                {
                    Utilities.Dispatch(() =>
                    {
                        _DataFound(resultData);
                    });
                }
            }
            resultData = null;
            _Threads--;
        }

        private void MWBsetScanningRect(int codeMask, Windows.Foundation.Rect rect)
        {
            BarcodeLib.Scanner.MWBsetScanningRect(codeMask, (float)rect.Left, (float)rect.Top, (float)rect.Width, (float)rect.Height);
        }

        private void Cleanup()
        {
            System.Diagnostics.Debug.WriteLine("Cleaning up scanner");
            try
            {
                _Ready = false;
                _Done = true;
                _Rendering = true;

                _Controller = null;
                _ImagePreview = null;
                _Effects = null;

                _Render.Dispose();
                _Render = null;
                _Image = null;

                _Source.StopPreviewAsync();
                _Source.PreviewFrameAvailable -= OnPreviewFrameAvailable;
                _Source.Dispose();
                _Source = null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Cleaning up scanner. {0}", ex.Message);
            }
        }

        byte[] AddRemoveData(byte[] data)
        {
            lock (this)
            {
                if (_ImageData == null) _ImageData = new List<byte[]>();
                if (data == null)
                {
                    if (_ImageData.Count == 0) return null;
                    var d = _ImageData[0];
                    _ImageData.RemoveAt(0);
                    return d;
                }
                else
                {
                    _ImageData.Add(data);
                    if (_ImageData.Count > _MaxThreads) _ImageData.RemoveAt(0);
                    return null;
                }
            }
        }

        void ProcessStart(object context)
        {
            if (_Threads >= _MaxThreads) return;
            //  get data
            var data = AddRemoveData(null);
            if (data == null) return;
            _Threads++;
            FindCodes(data, _Width, _Height);            
        }

        #endregion Private Methods

        #region Event Handlers

        void OnPreviewFrameAvailable(IImageSize imageSize)
        {
            Utilities.Dispatch(() => PreviewFrameAvailable(imageSize));
        }

        async void PreviewFrameAvailable(IImageSize imageSize)
        {
            if (!_Ready || _Rendering || _Done) return;
            _Rendering = true;

            await _Render.RenderAsync();

            Utilities.Dispatch(() =>
            {
                _Image.Invalidate();
            });

            if (_Controller.FocusControl.Supported)
            {
                if ((_Controller.FocusControl.FocusState != MediaCaptureFocusState.Searching) && (_Controller.FocusControl.FocusState != MediaCaptureFocusState.Focused)) _Controller.FocusControl.FocusAsync();
            }

            if (!_Controller.FocusControl.Supported || _Controller.FocusControl.FocusState == MediaCaptureFocusState.Focused)
            {
                //  extract codes
                if ((_CodesFound != null) || (_DataFound != null))
                {
                    if (_Controller.FocusControl.FocusState == MediaCaptureFocusState.Focused)
                    {
                        if (_Width == 0) _Width = _Image.PixelWidth;
                        if (_Height == 0) _Height = _Image.PixelHeight;
                        AddRemoveData(_Image.PixelBuffer.ToArray());
                        //if (_Threads < _MaxThreads)
                        //{
                        //    _Threads++;
                        //    var data = _Image.PixelBuffer.ToArray();
                        //    int width = _Image.PixelWidth, height = _Image.PixelHeight;
                        //    Task.Run(async () => FindCodes(data, width, height));
                        //}
                    }
                }
            }

            _Rendering = false;
        }

        #endregion Event Handlers
    }
}
