using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppSDKGallery.Helpers
{
    internal class AssetsHelper
    {
        /// <summary>
        /// Gets the full file path to an asset. Works for both packaged and unpackaged.
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string GetAsset(params string[] paths)
        {
            return Path.Join(GetAssetsFolder(), Path.Join(paths));
        }

        /// <summary>
        /// Gets the full file path to the assets folder. Works for both packaged and unpackaged.
        /// </summary>
        /// <returns></returns>
        public static string GetAssetsFolder()
        {
            if (DesktopAppHelper.IsContainerized())
            {
                return Path.Join(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "Assets");
            }

            return Path.Join(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "Assets");
        }
    }
}
