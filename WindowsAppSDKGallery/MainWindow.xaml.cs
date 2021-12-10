using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
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
using WindowsAppSDKGallery.DataModel;
using WindowsAppSDKGallery.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppSDKGallery
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            AddNavigationMenuItems();
            SetUnselected();
        }

        private void AddNavigationMenuItems()
        {
            foreach (var group in SampleInfoDataSource.Groups)
            {
                var item = new NavigationViewItem() { Content = group.Title, DataContext = group };
                AutomationProperties.SetName(item, group.Title);

                foreach (var sample in group.Items)
                {
                    var subitem = new NavigationViewItem { Content = sample.Title, DataContext = sample };
                    AutomationProperties.SetName(subitem, sample.Title);

                    item.MenuItems.Add(subitem);
                }

                NavigationViewControl.MenuItems.Add(item);
            }
        }

        private void NavigationViewControl_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var sample = args.SelectedItemContainer.DataContext as SampleInfoDataItem;
            if (sample != null)
            {
                NavigationViewControl.Header = sample;
                rootFrame.Visibility = Visibility.Visible;
                rootFrame.Navigate(typeof(SamplePage), sample);
            }
            else
            {
                SetUnselected();
            }
        }

        private void SetUnselected()
        {
            NavigationViewControl.Header = "Select a sample on the left.";
            rootFrame.Visibility = Visibility.Collapsed;
        }

        public void OpenSample(Type pageType)
        {
            var sampleItem = SampleInfoDataSource.Groups.SelectMany(i => i.Items).FirstOrDefault(i => i.PageType == pageType);

            if (sampleItem != null)
            {
                foreach (var group in NavigationViewControl.MenuItems.OfType<NavigationViewItem>())
                {
                    foreach (var item in group.MenuItems.OfType<NavigationViewItem>())
                    {
                        if (item.DataContext == sampleItem)
                        {
                            NavigationViewControl.SelectedItem = item;
                            return;
                        }
                    }
                }
            }
        }
    }
}
