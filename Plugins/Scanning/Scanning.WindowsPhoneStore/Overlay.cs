using System;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Scanning.WindowsPhoneStore
{
    public class Overlay
    {
        #region Fields

        Grid _Parent;
        Canvas _Overlay;
        WriteableBitmap _Image;
        DispatcherTimer _Timer;
        Rect _Rectangle;

        Brush _Transparent;
        Brush _Red;

        bool _On = false;

        #endregion Fields

        #region Constructors

        public Overlay(Image imageControl, Rect rect)
        {
            _Parent = (Grid)imageControl.Parent;
            _Rectangle = rect;

            _Transparent = new SolidColorBrush(Colors.Transparent);
            _Red = new SolidColorBrush(Colors.Red);
        }

        #endregion Constructors

        #region Methods

        public void AddOverlay()
        {
            _Overlay = new Canvas();
            _Overlay.Width = _Parent.ActualWidth;
            _Overlay.Height = _Parent.ActualHeight;
            _Overlay.Background = new SolidColorBrush(Colors.Transparent);

            _Timer = new DispatcherTimer();
            _Timer.Interval = TimeSpan.FromMilliseconds(100);
            _Timer.Tick += OnTick;
            _Timer.Start();
        }

        public void RemoveOverlay()
        {
            if (_Overlay == null) return;

            _Timer.Stop();
            _Timer = null;

            _Parent.Children.Remove(_Overlay);
            _Overlay = null;
        }

        #endregion Methods

        #region Event Handlers

        void OnTick(object sender, object e)
        {
            if (_On)
            {
                //  clear children
                _Overlay.Children.Clear();
                _On = false;
            }
            else
            {
                //  draw a red rectangle 
                double left = _Overlay.Width * _Rectangle.X, top = _Overlay.Height * _Rectangle.Y;
                double right = _Overlay.Width * _Rectangle.Width, bottom = _Overlay.Height * _Rectangle.Height;
                Windows.UI.Xaml.Shapes.Rectangle rectangle = new Windows.UI.Xaml.Shapes.Rectangle();
                rectangle.Margin = new Thickness(left, top, right, bottom);
                rectangle.Stroke = _Red;
                rectangle.StrokeThickness = 2;
                _Overlay.Children.Add(rectangle);
                //  draw cross-hair - vertical
                Windows.UI.Xaml.Shapes.Rectangle vertical = new Windows.UI.Xaml.Shapes.Rectangle();
                double centreX = _Overlay.Width / 2;
                vertical.Margin = new Thickness(centreX, top, centreX, bottom);
                vertical.Stroke = _Red;
                vertical.StrokeThickness = 2;
                _Overlay.Children.Add(vertical);
                //  draw cross-hair - horizontal
                double centreY = _Overlay.Height / 2;
                Windows.UI.Xaml.Shapes.Rectangle horizontal = new Windows.UI.Xaml.Shapes.Rectangle();
                horizontal.Margin = new Thickness(left, centreY, right, centreY);
                horizontal.Stroke = _Red;
                horizontal.StrokeThickness = 2;
                _Overlay.Children.Add(horizontal);

                _On = true;
            }
        }

        #endregion Event Handlers
    }
}
