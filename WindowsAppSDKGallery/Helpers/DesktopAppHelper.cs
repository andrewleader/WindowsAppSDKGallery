using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace WindowsAppSDKGallery.Helpers
{
    /// <summary>
    /// Code from https://github.com/CommunityToolkit/WindowsCommunityToolkit/blob/main/Microsoft.Toolkit.Uwp.Notifications/Toasts/Compat/Desktop/DesktopBridgeHelpers.cs
    /// </summary>
    internal class DesktopAppHelper
    {
        private const long APPMODEL_ERROR_NO_PACKAGE = 15700L;

        private static bool? _hasIdentity;

        public static bool HasIdentity()
        {
            if (_hasIdentity == null)
            {
                if (IsWindows7OrLower)
                {
                    _hasIdentity = false;
                }
                else
                {
                    int length = 0;
                    var sb = new StringBuilder(0);
                    NativeMethods.GetCurrentPackageFullName(ref length, sb);

                    sb = new StringBuilder(length);
                    int error = NativeMethods.GetCurrentPackageFullName(ref length, sb);

                    _hasIdentity = error != APPMODEL_ERROR_NO_PACKAGE;
                }
            }

            return _hasIdentity.Value;
        }

        private static bool? _isContainerized;

        /// <summary>
        /// Returns true if the app is running in a container (MSIX) or false if not running in a container (sparse or plain Win32)
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsContainerized()
        {
            if (_isContainerized == null)
            {
                // If MSIX or sparse
                if (HasIdentity())
                {
                    // Sparse is identified if EXE is running outside of installed package location
                    var packageInstalledLocation = Package.Current.InstalledLocation.Path;
                    var actualExeFullPath = Process.GetCurrentProcess().MainModule.FileName;

                    // If inside package location
                    if (actualExeFullPath.StartsWith(packageInstalledLocation))
                    {
                        _isContainerized = true;
                    }
                    else
                    {
                        _isContainerized = false;
                    }
                }

                // Plain Win32
                else
                {
                    _isContainerized = false;
                }
            }

            return _isContainerized.Value;
        }

        private static bool IsWindows7OrLower
        {
            get
            {
                int versionMajor = Environment.OSVersion.Version.Major;
                int versionMinor = Environment.OSVersion.Version.Minor;
                double version = versionMajor + ((double)versionMinor / 10);
                return version <= 6.1;
            }
        }
    }
}
