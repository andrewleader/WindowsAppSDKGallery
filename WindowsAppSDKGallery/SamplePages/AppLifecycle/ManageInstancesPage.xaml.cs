using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.AppLifecycle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WindowsAppSDKGallery.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppSDKGallery.SamplePages.AppLifecycle
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManageInstancesPage : Page
    {
        public ManageInstancesPage()
        {
            this.InitializeComponent();
        }

        private void GetInstances_ExecuteApi(object sender, ExecuteApiArgs e)
        {
            var instances = AppInstance.GetInstances();

            GetInstances.ReturnedObject = instances.ToList();
        }

        private void FindInstance_ExecuteApi(object sender, ExecuteApiArgs e)
        {
            var key = e.Parameters["key"];

            var instance = AppInstance.FindOrRegisterForKey(key);
            FindInstance.ReturnedObject = instance;
        }
    }
}
