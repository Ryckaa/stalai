using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace PlaceMint.Manager
{
    using PMException;

    /// <summary>
    /// Handles the low level process of seting a mouse hook and handing back information about the window.
    /// </summary>
    public class WindowDetailsGrabber
    {
        //Declare _mouseHookProcedure as a MouseHookProc type.
        private WindowsApi.MouseHookProc _mouseHookProcedure;
        private const int NO_HOOK = 0;
        private int _hHook = NO_HOOK;

        /// <summary>
        /// Default constructor
        /// </summary>
        public WindowDetailsGrabber()
        {
            _mouseHookProcedure = new WindowsApi.MouseHookProc(MouseHookProc);
        }

        private int MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            //Marshall the data from the callback.
            WindowsApi.MouseLLHookStruct MyMouseHookStruct = (WindowsApi.MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(WindowsApi.MouseLLHookStruct));

            if (nCode < 0)
            {
                return WindowsApi.CallNextHookEx(_hHook, nCode, wParam, lParam);
            }
            else
            {
                if (wParam.ToInt32() == WindowsApi.WM_LBUTTONUP)
                {
                    IntPtr hwnd = WindowsApi.WindowFromPoint(MyMouseHookStruct.Point);
                    IntPtr style = WindowsApi.GetWindowLongPtr(hwnd, WindowsApi.GWL_STYLE);
                    long value = style.ToInt64();
                    if ((style.ToInt64() & WindowsApi.WS_CHILD) != 0)
                    {
                        hwnd = WindowsApi.GetAncestor(hwnd, WindowsApi.GA_ROOT);
                    }
                    StringBuilder title = new StringBuilder(256);
                    StringBuilder clazz = new StringBuilder(256);
                    WindowsApi.RECT shape;
                    int len = WindowsApi.GetWindowText(hwnd, title, title.Capacity + 1);
                    title.Length = len;
                    len = WindowsApi.GetClassName(hwnd, clazz, clazz.Capacity + 1);
                    clazz.Length = len;
                    WindowsApi.GetWindowRect(hwnd, out shape);
                    MouseClicked(this, new WindowDetailEventArgs(title.ToString(), clazz.ToString(), new RectangleWrap(shape)));
                }
                return WindowsApi.CallNextHookEx(_hHook, nCode, wParam, lParam);
            }
        }

        /// <summary>
        /// Event Args for the event that is fired after the hot key has been pressed.
        /// </summary>
        public class WindowDetailEventArgs : EventArgs
        {
            private string _title;
            private string _class;
            private RectangleWrap _shape;

            internal WindowDetailEventArgs(string title, string clazz, RectangleWrap shape)
            {
                _title = title;
                _class = clazz;
                _shape = shape;
            }

            public string Title
            {
                get { return _title; }
            }

            public string Class
            {
                get { return _class; }
            }

            public RectangleWrap Shape
            {
                get { return _shape; }
            }
        }

        private event EventHandler<WindowDetailEventArgs> MouseClicked;
        private const int WH_MOUSE = 7;
        private const int WH_MOUSE_LL = 14;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        /// <exception cref="Win32Exception">When the mouse hook fails</exception>
        public void RegisterEventHandler(EventHandler<WindowDetailEventArgs> handler)
        {
            // install Mouse hook only if it is not installed and must be installed
            if (_hHook == NO_HOOK)
            {
                //install hook
                _hHook = WindowsApi.SetWindowsHookEx(WH_MOUSE_LL, _mouseHookProcedure,
                    Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
                            //(IntPtr)0, AppDomain.GetCurrentThreadId());//Thread.CurrentThread.ManagedThreadId);
                //If SetWindowsHookEx fails.
                if (_hHook == NO_HOOK)
                {
                    //Initializes and throws a new instance of the Win32Exception class with the specified error. 
                    throw new Win32Exception(string.Format("Mouse hook not set: {0}", Marshal.GetLastWin32Error()));
                }
            }
            MouseClicked += handler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        /// <exception cref="Win32Exception">When the mouse hook fails</exception>
        public void UnregisterEventHandler(EventHandler<WindowDetailEventArgs> handler)
        {
            MouseClicked -= handler;
            if (MouseClicked == null && WindowsApi.UnhookWindowsHookEx(_hHook) == 0)
            {
                //Initializes and throws a new instance of the Win32Exception class with the specified error. 
                throw new Win32Exception(string.Format("Mouse hook not unset: {0}", Marshal.GetLastWin32Error()));
            }
            _hHook = NO_HOOK;
        }
    }
}
