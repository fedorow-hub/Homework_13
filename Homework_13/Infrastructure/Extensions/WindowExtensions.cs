using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Homework_13.Infrastructure.Extensions
{
    static class WindowExtensions
    {
        private const string User32 = "user32.dll";
        [DllImport(User32, CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint  msg, IntPtr wParam, IntPtr lParam);
        public static IntPtr SendMessage(this Window window, WM msg, SC wParam, IntPtr lParam = default)
        {
            return SendMessage(window.GetWindowHandle(), 
                (uint)msg, 
                (IntPtr)wParam, 
                lParam == default 
                    ? (IntPtr)' ' 
                    : lParam);
        }

        public static IntPtr GetWindowHandle(this Window window)
        {
            var helper = new WindowInteropHelper(window);
            return helper.Handle;
        }
    }
}
