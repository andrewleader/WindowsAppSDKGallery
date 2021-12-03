using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowsAppSDKGallery.SamplePages.MediaSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayVideosPage : Page
    {
        public PlayVideosPage()
        {
            this.InitializeComponent();

            LoadVideo();
        }

        private async void LoadVideo()
        {
            try
            {
                await WebViewForVideo.EnsureCoreWebView2Async();

                // To access local files: https://github.com/MicrosoftEdge/WebView2Feedback/issues/642 need WebView2 1.0.707-prerelease
                var assetsFolderPath = Path.Join(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "Assets");
                WebViewForVideo.CoreWebView2.SetVirtualHostNameToFolderMapping("LocalAssets", assetsFolderPath, Microsoft.Web.WebView2.Core.CoreWebView2HostResourceAccessKind.Allow);

                string src = "http://LocalAssets/AdaptiveIntroVideo.mp4";

                string html = "<html><body><video playsinline autoplay muted width=\"100%\" height=\"100%\" data-loop-delay=\"5000\" src=\"" + src + "\"></video></body></html>";
                WebViewForVideo.NavigateToString(html);
            }
            catch (Exception ex)
            {
                // This will fail on unpackaged apps since it depends on Package.Current, we'd have to look up the file via different APIs...
            }
        }
    }
}
