using System;

using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace Scanning.WindowsPhoneStore
{
    public static class Utilities
    {
        /// <summary>
        /// Dispatch an action to the UI thread
        /// </summary>
        /// <param name="action"></param>
        public static void Dispatch(Action action)
        {
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
        }
    }
}
