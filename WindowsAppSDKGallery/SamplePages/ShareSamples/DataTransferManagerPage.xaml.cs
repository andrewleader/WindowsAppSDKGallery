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
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WindowsAppSDKGallery.Helpers;
using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppSDKGallery.SamplePages.ShareSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DataTransferManagerPage : Page
    {
        public static DataTransferManager _dataTransferManager;

        public DataTransferManagerPage()
        {
            this.InitializeComponent();

            // Initialize the DataTransferManager (only needs to be initialized once for lifetime of app)
            if (_dataTransferManager == null)
            {
                _dataTransferManager = DataTransferManagerHelper.GetForWindow(WindowHelper.GetHwndForCurrentWindow());
                _dataTransferManager.DataRequested += _dataTransferManager_DataRequested;
            }
        }

        private void _dataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            args.Request.Data.SetText("Text from the Windows App SDK Gallery!");
            args.Request.Data.RequestedOperation = DataPackageOperation.Copy;
            args.Request.Data.Properties.Title = "DataTransferManager demo";
        }

        public static class DataTransferManagerHelper
        {
            static readonly Guid _dtm_iid = new Guid(0xa5caee9b, 0x8708, 0x49d1, 0x8d, 0x36, 0x67, 0xd2, 0x5a, 0x8d, 0xa0, 0x0c);

            static IDataTransferManagerInterop DataTransferManagerInterop => DataTransferManager.As<IDataTransferManagerInterop>();

            public static DataTransferManager GetForWindow(IntPtr hwnd)
            {
                IntPtr result;
                result = DataTransferManagerInterop.GetForWindow(WindowHelper.GetHwndForCurrentWindow(), _dtm_iid);
                DataTransferManager dataTransferManager = MarshalInterface<DataTransferManager>.FromAbi(result);
                return (dataTransferManager);
            }

            public static void ShowShareUIForWindow(IntPtr hwnd)
            {
                DataTransferManagerInterop.ShowShareUIForWindow(hwnd);
            }

            [ComImport]
            [Guid("3A3DCD6C-3EAB-43DC-BCDE-45671CE800C8")]
            [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            public interface IDataTransferManagerInterop
            {
                IntPtr GetForWindow([In] IntPtr appWindow, [In] ref Guid riid);
                void ShowShareUIForWindow(IntPtr appWindow);
            }
        }

        private void ButtonShareTextToEmail_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManagerHelper.ShowShareUIForWindow(WindowHelper.GetHwndForCurrentWindow());
        }
    }
}
