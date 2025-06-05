using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics.CodeAnalysis;

//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Manager
{
    /// <summary>
    /// Wrapper for Win32 Dll Calls
    /// </summary>
    public static class WindowsApi
    {
        #region Constant Fields
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public static readonly IntPtr HWND_TOP = new IntPtr(0);
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public const int SW_HIDE = 0;
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public const int SW_NORMAL = 1;
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public const int SW_MINIMIZE = 2;
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public const int SW_MAXIMIZE = 3;
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public const int SW_SHOW = 5;
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public const int SW_RESTORE = 9;
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public const int WM_LBUTTONUP = 0x202;
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public const int GWL_STYLE = -16;
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public const long WS_CHILD = 0x40000000L;
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public const uint GA_ROOT = 2;
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public const uint SWP_NOSIZE = 0x0001;
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public const uint SWP_NOMOVE = 0x0002;
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public const uint SWP_NOZORDER = 0x0004;
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public const uint SWP_NOACTIVATE = 0x0010;
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public const uint SWP_SHOWWINDOW = 0x0040;
        #endregion

        #region DLL Imports
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [SuppressMessage("Microsoft.Naming", "CA1711")]
        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(int hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumWindowsProc lpEnumFunc, IntPtr lParam);
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [SuppressMessage("Microsoft.Naming", "CA1726")]
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr GetAncestor(IntPtr hwnd, uint gaFlags);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [SuppressMessage("Microsoft.Naming", "CA1702")]
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [SuppressMessage("Microsoft.Naming", "CA1702")]
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [SuppressMessage("Microsoft.Naming", "CA1720")]
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [DllImport("user32.dll")]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
        [SuppressMessage("Microsoft.Naming", "CA1702")]
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [SuppressMessage("Microsoft.Naming", "CA1702")]
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [SuppressMessage("Microsoft.Naming", "CA1720")]
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        [SuppressMessage("Microsoft.Naming", "CA1702")]
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        [SuppressMessage("Microsoft.Naming", "CA1702")]
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [SuppressMessage("Microsoft.Naming", "CA1702")]
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [SuppressMessage("Microsoft.Naming", "CA1711")]
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int SetWindowsHookEx(int idHook, MouseHookProc lpfn, IntPtr hMod, int dwThreadId);
        [SuppressMessage("Microsoft.Naming", "CA1702")]
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        [SuppressMessage("Microsoft.Naming", "CA1726")]
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X,
            int Y, int cx, int cy, uint uFlags);
        [SuppressMessage("Microsoft.Naming", "CA1702")]
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [SuppressMessage("Microsoft.Naming", "CA1711")]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int UnhookWindowsHookEx(int idHook);
        [SuppressMessage("Microsoft.Naming", "CA1702")]
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT Point);
        #endregion

        // Nested Types
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public delegate int MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam);
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            [SuppressMessage("Microsoft.Naming", "CA1704")]
            public int x, y;
        }
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left, Top, Right, Bottom;
        }
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            [SuppressMessage("Microsoft.Naming", "CA1726")]
            public uint length, flags, showCmd;
            public WindowsApi.POINT ptMinPosition, ptMaxPosition;
            public WindowsApi.RECT rcNormalPosition;
        }
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public static WindowsApi.POINT makePOINT(Point p)
        {
            WindowsApi.POINT P;
            P.x = p.X;
            P.y = p.Y;
            return P;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct MouseLLHookStruct
        {
            public WindowsApi.POINT Point;
            public int MouseData;
            [SuppressMessage("Microsoft.Naming", "CA1726")]
            public int Flags;
            public int Time;
            public int ExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        public class MouseHookStruct
        {
            public WindowsApi.POINT pt;
            [SuppressMessage("Microsoft.Naming", "CA1704")]
            public int hwnd;
            [SuppressMessage("Microsoft.Naming", "CA1702")]
            public int wHitTestCode;
            [SuppressMessage("Microsoft.Naming", "CA1704")]
            public int dwExtraInfo;
        }
    }
}