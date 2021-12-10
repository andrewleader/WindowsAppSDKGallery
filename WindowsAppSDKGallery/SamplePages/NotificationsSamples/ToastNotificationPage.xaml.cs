using Microsoft.Toolkit.Uwp.Notifications;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppSDKGallery.SamplePages.NotificationsSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ToastNotificationPage : Page
    {
        public ToastNotificationPage()
        {
            this.InitializeComponent();
        }

        private void ButtonShowToast_Click(object sender, RoutedEventArgs e)
        {
            new ToastContentBuilder()
                .AddText("Toast from Windows App SDK Gallery")
                .AddText("Click the toast to open the app and navigate back to the toast sample. Or type a message to try background activation.")
                .AddArgument("action", "viewToastSample")
                .AddInputTextBox("textBox", "Type some text")
                .AddButton(new ToastButton()
                    .SetTextBoxId("textBox")
                    .SetContent("Send")
                    .SetBackgroundActivation()
                    .AddArgument("action", "sendMessage"))
                .Show();
        }

        private void ClearNotifications_ExecuteApi(object sender, Controls.ExecuteApiArgs e)
        {
            ToastNotificationManagerCompat.History.Clear();
        }
    }
}
