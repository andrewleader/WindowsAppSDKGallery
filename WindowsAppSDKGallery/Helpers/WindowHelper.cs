﻿using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppSDKGallery.Helpers
{
    public static class WindowHelper
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void ShowWindow(Window window)
        {
            // Bring the window to the foreground... first get the window handle...
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            // Restore window if minimized... requires DLL import above
            ShowWindow(hwnd, 0x00000009);

            // And call SetForegroundWindow... requires DLL import above
            SetForegroundWindow(hwnd);
        }

        public static AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.Window);
            WindowId myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(myWndId);
        }
    }
}
