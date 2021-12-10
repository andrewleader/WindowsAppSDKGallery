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
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppSDKGallery.SamplePages.BackgroundTaskSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InternetAvailablePage : Page
    {
        private const string TaskName = "InternetAvailableTask";
        private bool _isRegistered;

        public InternetAvailablePage()
        {
            this.InitializeComponent();

            UpdateUI();
        }

        private void UpdateUI()
        {
            _isRegistered = BackgroundTaskRegistration.AllTasks.Any(i => i.Value.Name == TaskName);

            ButtonRegisterTask.Content = _isRegistered ? "Unregister background task" : "Register background task";
        }

        private void ButtonRegisterTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isRegistered)
                {
                    // Unregister
                    BackgroundTaskRegistration.AllTasks.FirstOrDefault(i => i.Value.Name == TaskName).Value?.Unregister(true);

                    UpdateUI();
                }
                else
                {
                    var builder = new BackgroundTaskBuilder()
                    {
                        Name = TaskName
                    };

                    builder.SetTaskEntryPointClsid(typeof(InternetAvailableBackgroundTask).GUID);
                    builder.SetTrigger(new SystemTrigger(SystemTriggerType.InternetAvailable, false));

                    builder.Register();

                    UpdateUI();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
