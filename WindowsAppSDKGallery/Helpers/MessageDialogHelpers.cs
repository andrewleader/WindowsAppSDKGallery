using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Popups;

namespace WindowsAppSDKGallery.Helpers
{
    internal static class MessageDialogHelpers
    {
        public static IAsyncOperation<IUICommand> ShowOverCurrentWindowAsync(this MessageDialog dialog)
        {
            WinRT.Interop.InitializeWithWindow.Initialize(dialog, WindowHelper.GetHwndForCurrentWindow());
            return dialog.ShowAsync();
        }
    }
}
