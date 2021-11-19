using Hardcodet.Wpf.TaskbarNotification;
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
using WindowsAppSDKGallery.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppSDKGallery.SamplePages.SystemTraySamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SystemTrayIconPage : Page
    {
        private static TaskbarIcon _icon;

        public SystemTrayIconPage()
        {
            this.InitializeComponent();
        }

        private void UpdateButton()
        {
            if (_icon != null)
            {
                ButtonShowHide.Content = "Hide icon";
            }
            else
            {
                ButtonShowHide.Content = "Show icon";
            }
        }

        private void ButtonShowHide_Click(object sender, RoutedEventArgs e)
        {
            if (_icon == null)
            {
                var contextMenu = new System.Windows.Controls.ContextMenu();

                var showWindow = new System.Windows.Controls.MenuItem
                {
                    Header = "Show window"
                };
                showWindow.Click += ShowWindow_Click;
                contextMenu.Items.Add(showWindow);

                var hideWindow = new System.Windows.Controls.MenuItem
                {
                    Header = "Hide window"
                };
                hideWindow.Click += HideWindow_Click;
                contextMenu.Items.Add(hideWindow);

                contextMenu.Items.Add(new System.Windows.Controls.Separator());

                var exit = new System.Windows.Controls.MenuItem
                {
                    Header = "Exit"
                };
                exit.Click += Exit_Click;
                contextMenu.Items.Add(exit);

                _icon = new TaskbarIcon()
                {
                    Visibility = System.Windows.Visibility.Visible,
                    ToolTipText = "Windows App SDK",
                    MenuActivation = PopupActivationMode.All,
                    Icon = new System.Drawing.Icon(Path.Join(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "Assets", "msicon.ico")),
                    ContextMenu = contextMenu
                };
            }
            else
            {
                _icon.Dispose();
                _icon = null;
            }

            UpdateButton();
        }

        private void Exit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void HideWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            WindowHelper.GetAppWindowForCurrentWindow().Hide();
        }

        private void ShowWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            WindowHelper.ShowWindow(App.Window);
        }
    }
}
