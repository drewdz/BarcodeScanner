using System;
using System.Linq;

using Windows.UI.Xaml.Controls;

namespace BarcodeScanner.WindowsPhoneStore.Extensions
{
    public static class PageExtensions
    {
        public static void RemoveFromBackStack(this Page page, Type type)
        {
            if (page.Frame.BackStack.Count == 0) return;
            var b = page.Frame.BackStack.Where(item => item.SourcePageType == type).FirstOrDefault();
            if (b != null) page.Frame.BackStack.Remove(b);
        }

        public static void RemoveSelfFromBackStack(this Page page)
        {
            if (page.Frame.BackStack.Count == 0) return;
            var b = page.Frame.BackStack.Where(item => item.SourcePageType == page.GetType()).FirstOrDefault();
            if (b != null) page.Frame.BackStack.Remove(b);
        }

        public static void BackStackClear(this Page page)
        {
            page.Frame.BackStack.Clear();
        }

        public static void BackStackClearTo(this Page page)
        {
            for (int i = page.Frame.BackStack.Count - 1; i >= 0; i--)
            {
                //  stop at the first instance of this page from the back
                if (page.Frame.BackStack[i].SourcePageType == page.GetType()) break;
                //  remove the page
                page.Frame.BackStack.Remove(page.Frame.BackStack[i]);
            }
        }
    }
}
