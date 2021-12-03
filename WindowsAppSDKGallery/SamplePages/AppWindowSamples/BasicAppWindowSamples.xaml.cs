using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Windows.UI.Popups;
using WindowsAppSDKGallery.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppSDKGallery.SamplePages.AppWindowSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BasicAppWindowSamples : Page
    {
        private AppWindow _appWindow;

        public BasicAppWindowSamples()
        {
            this.InitializeComponent();

            try
            {
                // Requires a workaround in app.manifest due to bug https://github.com/microsoft/WindowsAppSDK/issues/1815
                IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.Window);
                WindowId myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
                _appWindow = AppWindow.GetFromWindowId(myWndId);
            }
            catch (Exception ex)
            {
                IsEnabled = false;
                _ = new MessageDialog(ex.ToString(), "Error initializing AppWindow").ShowOverCurrentWindowAsync();
            }
        }

        private void MoveWindow_ExecuteApi(object sender, Controls.ExecuteApiArgs e)
        {
            int x, y;
            int.TryParse(e.Parameters["x"], out x);
            int.TryParse(e.Parameters["y"], out y);

            _appWindow.Move(new PointInt32(x, y));
        }

        private void ResizeWindow_ExecuteApi(object sender, Controls.ExecuteApiArgs e)
        {
            int width, height;
            int.TryParse(e.Parameters["width"], out width);
            int.TryParse(e.Parameters["height"], out height);

            _appWindow.Resize(new SizeInt32(width, height));
        }
    }
}
