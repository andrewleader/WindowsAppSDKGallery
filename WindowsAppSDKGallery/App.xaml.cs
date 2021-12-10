using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WindowsAppSDKGallery.DataModel;
using WindowsAppSDKGallery.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppSDKGallery
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static DispatcherQueue DispatcherQueue { get; private set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            // Get the app-level dispatcher
            DispatcherQueue = global::Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();

            // Register for activation redirection
            Microsoft.Windows.AppLifecycle.AppInstance.GetCurrent().Activated += App_Activated;

            // Register for toast activation
            ToastNotificationManagerCompat.OnActivated += ToastNotificationManagerCompat_OnActivated;

            // If we weren't launched by a toast, launch our window like normal
            // (Otherwise if launched by a toast, our OnActivated callback will be triggered)
            if (!ToastNotificationManagerCompat.WasCurrentProcessToastActivated())
            {
                LaunchAndBringToForegroundIfNeeded();
            }
        }

        private void LaunchAndBringToForegroundIfNeeded()
        {
            if (m_window == null)
            {
                m_window = new MainWindow();
                m_window.Activate();

                // Additionally we show using our helper, since if activated via a toast, it doesn't
                // activate the window correctly
                WindowHelper.ShowWindow(m_window);
            }
            else
            {
                WindowHelper.ShowWindow(m_window);
            }
        }

        private void App_Activated(object sender, Microsoft.Windows.AppLifecycle.AppActivationArguments e)
        {
            WindowHelper.ShowWindow(App.Window);
        }

        private void ToastNotificationManagerCompat_OnActivated(ToastNotificationActivatedEventArgsCompat e)
        {
            // Use the dispatcher from the window if present, otherwise the app dispatcher
            var dispatcherQueue = App.Window?.DispatcherQueue ?? App.DispatcherQueue;

            dispatcherQueue.TryEnqueue(delegate
            {
                HandleToastActivation(e);
            });
        }

        private void HandleToastActivation(ToastNotificationActivatedEventArgsCompat e)
        {
            var args = ToastArguments.Parse(e.Argument);

            args.TryGetValue("action", out string action);
            if (action == null)
            {
                action = "";
            }

            switch (action)
            {
                // Send a background message
                case "sendMessage":
                    string message = e.UserInput["textBox"].ToString();
                    new ToastContentBuilder()
                        .AddText("Here's what you typed...")
                        .AddText(message)
                        .Show();

                    // If the UI app isn't open
                    if (App.Current == null)
                    {
                        // Close since we're done
                        Process.GetCurrentProcess().Kill();
                    }

                    break;

                case "viewToastSample":
                    LaunchAndBringToForegroundIfNeeded();

                    // Open the toast sample
                    App.Window.OpenSample(typeof(SamplePages.NotificationsSamples.ToastNotificationPage));

                    break;

                default:
                    LaunchAndBringToForegroundIfNeeded();
                    break;
            }
        }

        private static MainWindow m_window;
        public static MainWindow Window => m_window;
    }
}
