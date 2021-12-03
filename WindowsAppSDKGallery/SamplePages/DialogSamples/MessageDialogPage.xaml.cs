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
using Windows.UI.Popups;
using WindowsAppSDKGallery.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppSDKGallery.SamplePages.DialogSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MessageDialogPage : Page
    {
        public MessageDialogPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Sets the HWND for the dialog so it can be displayed.
        /// </summary>
        /// <param name="dialog"></param>
        /// <returns></returns>
        private MessageDialog InitWindow(MessageDialog dialog)
        {
            WinRT.Interop.InitializeWithWindow.Initialize(dialog, WindowHelper.GetHwndForCurrentWindow());
            return dialog;
        }

        private async void ContentOnly_ExecuteApi(object sender, Controls.ExecuteApiArgs e)
        {
            ContentOnly.IsEnabled = false;

            await InitWindow(new MessageDialog(e.Parameters["content"])).ShowAsync();

            ContentOnly.IsEnabled = true;
        }

        private async void TitleAndContent_ExecuteApi(object sender, Controls.ExecuteApiArgs e)
        {
            TitleAndContent.IsEnabled = false;

            await InitWindow(new MessageDialog(e.Parameters["content"], e.Parameters["title"])).ShowAsync();

            TitleAndContent.IsEnabled = true;
        }
    }
}
