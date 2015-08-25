using Android.App;
using Android.Content;

using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;

using System;
using System.Threading;

namespace Scanning.Droid
{
    public static class Utilities
    {
        public static Context GetActivityContext()
        {
            return Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
        }

        public static void MaskException(Action action)
        {
            try
            {
                action();
            }
            catch
            {

            }
        }

        public static void Dispatch(Action action)
        {
            if (Application.SynchronizationContext == SynchronizationContext.Current)
            {
                action();
            }
            else
            {
                Application.SynchronizationContext.Post(x => MaskException(action), null);
            }
        }
    }
}