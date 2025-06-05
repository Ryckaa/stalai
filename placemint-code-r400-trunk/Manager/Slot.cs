using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PlaceMint.Manager
{
    using PMException;
    using Properties;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Container class that will hold a window.
    /// </summary>
    [Serializable]
    public class Slot : IEquatable<Slot>, IDeepCloneable<Slot>, IFileNotFound, IWIterator<IntPtr>
    {
        /// <summary>
        /// Used to define a slot as empty
        /// </summary>
        public static IntPtr EMPTY = IntPtr.Zero;
        /// <summary>
        /// Slot needs to be able to have at least one window to control
        /// </summary>
        public static int MIN_SIZE = 1;
        /// <summary>
        /// Allowing Int32.MaxValue causes the program to run out of memory
        /// so set a upper value that is high, but not too high
        /// </summary>
        public static int MAX_SIZE = 10000;

        private RectangleWrap _shape;
        private Point _center;
        private Hotkey _hotkey;
        private List<IntPtr> _hwnds;
        private int _maxWindowCount;

        //TODO: Edit constructors (and simplify?)
        #region Constructors
        /// <summary>
        /// Constructor for a slot with out a window
        /// </summary>
        public Slot()
            : this(new RectangleWrap(), new Hotkey(), MIN_SIZE, new List<IntPtr>()) { }

        /// <summary>
        /// Constructor for a slot with out a window
        /// All other empty constructors are wrappers for this
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="hotkey"></param>
        /// <param name="size">Number of windows the slot can hold</param>
        public Slot(RectangleWrap shape, Hotkey hotkey, int size)
            : this(shape, hotkey, size, new List<IntPtr>()) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="hotkey"></param>
        /// <param name="size">Number of windows the slot can hold</param>
        /// <param name="hwnds">List of windows to load into slot</param>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public Slot(RectangleWrap shape, Hotkey hotkey, int size, List<IntPtr> hwnds)
        {
            if (size < hwnds.Count)
                throw new IllegalInitializationException("Size was smaller than the number of hWNDs in the list.");
            if (size < MIN_SIZE)
                throw new IllegalInitializationException(String.Format("Size must be at least {0}.", MIN_SIZE));
            if (size > MAX_SIZE)
                throw new IllegalInitializationException(String.Format("Size must be at less than {0}.", MAX_SIZE));
            _hwnds = new List<IntPtr>(size);
            foreach (IntPtr hwnd in hwnds)
            {
                if (hwnd == Slot.EMPTY)
                    throw new IllegalInitializationException("Don't pass EMPTY in the constructor.");
                _hwnds.Add(hwnd);
            }
            _maxWindowCount = size;
            _shape = shape;
            _center = Slot.CalculateCenter(_shape);
            _hotkey = hotkey;
        }
        #endregion

        /// <summary>
        /// Number of windows currently associated with stack
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [XmlIgnore]
        public List<IntPtr> Hwnds
        {
            get { return _hwnds; }
            set { _hwnds = value; }
        }

        /// <summary>
        /// False unless there is a window
        /// </summary>
        [XmlIgnore]
        public bool IsEmpty
        {
            get { return (_hwnds.Count == 0); }
        }

        /// <summary>
        /// False when only one window cna be held
        /// </summary>
        [XmlIgnore]
        public bool IsStack
        {
            get { return (_maxWindowCount > 1); }
        }

        /// <summary>
        /// False when maxWindowCount is reached
        /// </summary>
        [XmlIgnore]
        public bool HasFree
        {
            get { return (_hwnds.Count != _maxWindowCount); }
        }

        /// <summary>
        /// Maximum number of windows
        /// </summary>
        [XmlAttribute("size")]
        public int Size
        {
            get { return _maxWindowCount; }
            set { _maxWindowCount = value; }
        }

        /// <summary>
        /// Number of windows currently associated with stack
        /// </summary>
        [XmlIgnore]
        public int Count
        {
            get { return _hwnds.Count; }
        }

        /// <summary>
        /// Get the window shape
        /// </summary>
        [XmlElement("shape", typeof(RectangleWrap))]
        public RectangleWrap Shape
        {
            get { return _shape; }
            set
            {
                _shape = value;
                _center = Slot.CalculateCenter(_shape);
            }
        }
        /// <summary>
        /// Get the window shape
        /// </summary>
        [XmlElement("hotkey", typeof(Hotkey))]
        public Hotkey Hotkey
        {
            get { return _hotkey; }
            set { _hotkey = value; }
        }
        /// <summary>
        /// Point at the center of rectangle
        /// </summary>
        [XmlIgnore]
        public Point Center
        {
            get { return _center; }
        }

        /// <summary>
        /// Checks if a window can be placed in the slot at the specified depth
        /// </summary>
        /// <param name="depth"></param>
        /// <returns></returns>
        public bool IsStackSlotFree(int depth)
        {
            Logger.Trace("isStackSlotFree|depth|{0}|maxWindowCount|{1}|count|{2}|", depth, _maxWindowCount, Count);
            if (depth < _maxWindowCount)
            {
                if (Count - 1 < depth)
                {
                    Logger.Trace("Count -1 < depth");
                    return true;
                }
                int winCount = 0;
                for (int i = 0; i < Count; i++)
                {
                    Logger.Trace("_hwnds[{0}] == {1}", i, _hwnds[i]);
                    if (_hwnds[winCount] != Slot.EMPTY)
                    {
                        winCount++;
                    }
                }
                if (winCount < depth)
                {
                    Logger.Trace("found empty -> true");
                    return true;
                }
                Logger.Trace("all full -> false");
            }
            else
            {
                Logger.Trace("depth > _maxWindowCount -> false");
            }
            return false;
        }

        /// <summary>
        /// Adds a hWND for a slot to track
        /// </summary>
        /// <param name="hwnd"></param>
        /// <exception cref="StackFullException"></exception>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public void AddHwnd(IntPtr hwnd)
        {
            if (!HasFree)
                throw new StackFullException("Slot can not hold more hWNDs.");

            _hwnds.Add(hwnd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toRemove"></param>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public void RemoveHwnds(List<IntPtr> toRemove)
        {
            foreach (IntPtr hwnd in toRemove)
            {
                _hwnds.Remove(hwnd);
            }
        }

        /// <summary>
        /// Tests if two objects are the same
        /// </summary>
        /// <param name="other">slot to test against.</param>
        /// <returns></returns>
        public bool Equals(Slot other)
        {
            if ((System.Object)other == null || !this.Shape.Equals(other.Shape) ||
                    this.Size != other.Size || this.Count != other.Count)
                return false;
            for (int i = 0; i < this.Count; i++)
            {
                if (this._hwnds[i] != other.Hwnds[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Tests if two objects are the same
        /// </summary>
        /// <param name="obj">object to test against.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            return Equals(obj as Slot);
        }

        /// <summary>
        /// Get object hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int result = Shape.GetHashCode();
            foreach (IntPtr hwnd in _hwnds)
                result *= (int)hwnd;
            return result;
        }

        /// <summary>
        /// String representation of String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Slot:\n  _maxWindowCount|");
            sb.Append(_maxWindowCount);
            sb.Append("|\n  _hwnds|");
            if (_hwnds.Count == 0)
            {
                sb.Append("EMPTY");
            }
            else
            {
                for(int i = 0; i < _hwnds.Count; i++)
                {
                    sb.Append(_hwnds[i].ToString());
                    if (i != _hwnds.Count - 1)
                    {
                        sb.Append(", ");
                    }
                }
            }
            sb.Append("|\n  isEmpty|");
            sb.Append(IsEmpty);
            sb.Append("|\n  Hotkey|");
            sb.Append(_hotkey.ToString());
            sb.Append("|\n  x|");
            sb.Append(_shape.X);
            sb.Append("|y|");
            sb.Append(_shape.Y);
            sb.Append("|w|");
            sb.Append(_shape.Width);
            sb.Append("|h|");
            sb.Append(_shape.Height);
            sb.Append("|");
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>New object with all of the same values.</returns>
        public Slot DeepClone()
        {
            return new Slot(this.Shape.DeepClone(), this.Hotkey.DeepClone(), this.Size, this.Hwnds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string FileNotFoundMsg() { return ""; }

        #region Iterator methods
        private int _it_index = 0;

        /// <summary>
        /// Reset Slot iterator to first element.
        /// <para>Call everytime before starting an itteration.</para>
        /// </summary>
        public void IteratorReset()
        {
            _it_index = 0;
        }

        /// <summary>
        /// Move to the next element.
        /// </summary>
        /// <exception cref="OutOfRangeException">Thrown if the last element has already been reached.</exception>
        public void IteratorNext()
        {
            if (_it_index >= _hwnds.Count)
            {
                throw new OutOfRangeException(String.Format(Resources.illegalHWNDIndex, _hwnds.Count, _it_index + 1));
            }
            _it_index++;
        }

        /// <summary>
        /// Are there more slots?
        /// </summary>
        /// <returns></returns>
        public bool IteratorHasMore()
        {
            return (_it_index < _hwnds.Count);
        }

        /// <summary>
        /// The current element (W/R access).
        /// </summary>
        [XmlIgnore]
        public IntPtr IteratorCurrent
        {
            get { return _hwnds[_it_index]; }
            set { _hwnds[_it_index] = value; }
        }
        #endregion

        #region Static

        /// <summary>
        /// Predicate for matching EMPTY
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public static bool MatchEmpty(IntPtr hwnd)
        {
            return (hwnd == Slot.EMPTY);
        }
        /// <summary>
        /// Calculate the center of the suplied rectangle
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public static Point CalculateCenter(RectangleWrap shape)
        {
            return new Point(shape.X + (shape.Width / 2), shape.Y + (shape.Height / 2));
        }

        /// <summary>
        /// Checks if a window is minimized
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public static bool IsMinimized(IntPtr hwnd)
        {
            return isState(hwnd, WindowsApi.SW_MINIMIZE);
        }

        /// <summary>
        /// Checks if any of the windows are minimized
        /// </summary>
        /// <param name="hwnds">List of windows</param>
        /// <returns>True if atleast one window is minimized, false if list is empty</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public static bool IsMinimized(List<IntPtr> hwnds)
        {
            foreach (IntPtr hwnd in hwnds)
            {
                if (isState(hwnd, WindowsApi.SW_MINIMIZE))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if a window is maximized
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public static bool IsMaximized(IntPtr hwnd)
        {
            return isState(hwnd, WindowsApi.SW_MAXIMIZE);
        }

        /// <summary>
        /// Checks if any of the windows are maximized
        /// </summary>
        /// <param name="hwnds">List of windows</param>
        /// <returns>True if atleast one window is maximized, false if list is empty</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public static bool IsMaximized(List<IntPtr> hwnds)
        {
            foreach (IntPtr hwnd in hwnds)
            {
                if (isState(hwnd, WindowsApi.SW_MAXIMIZE))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool isState(IntPtr hwnd, int state)
        {
            WindowsApi.WINDOWPLACEMENT structure = new WindowsApi.WINDOWPLACEMENT();
            structure.length = Convert.ToUInt32(Marshal.SizeOf(structure));
            WindowsApi.GetWindowPlacement(hwnd, ref structure);
            return (structure.showCmd == state);
        }

        /// <summary>
        /// Adjust the window.
        /// </summary>
        /// <param name="hwnd">Window to adjust.</param>
        /// <param name="shape">Characteristics window should have.</param>
        /// <param name="movable">Can the window be move.</param>
        /// <param name="sizable">Can the window be resized.</param>
        /// <param name="sendF5">Attemp to refresh wiondow with f5.</param>
        /// <exception cref="Win32Exception">Thrown if there was Win32 error.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public static void MoveAndSizeWin(IntPtr hwnd, RectangleWrap shape, bool movable, bool sizable, bool sendF5)
        {
            MoveAndSizeWin(hwnd, shape, movable, sizable, sendF5, false);
        }

        /// <summary>
        /// Adjust the window.
        /// </summary>
        /// <param name="hwnd">Window to adjust.</param>
        /// <param name="shape">Characteristics window should have.</param>
        /// <param name="movable">Can the window be move.</param>
        /// <param name="sizable">Can the window be resized.</param>
        /// <param name="sendF5">Attemp to refresh wiondow with f5.</param>
        /// <param name="skipResizeCheck">Skip check for no size change</param>
        /// <exception cref="Win32Exception">Thrown if there was Win32 error.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public static void MoveAndSizeWin(IntPtr hwnd, RectangleWrap shape, bool movable, bool sizable, bool sendF5, bool skipResizeCheck)
        {
            bool success;
            if (sendF5 && !skipResizeCheck)
            {
                WindowsApi.RECT oldRect;
                success = WindowsApi.GetWindowRect(hwnd, out oldRect);
                if (!success)
                {
                    // Get the last Win32 error code
                    int errorCode = Marshal.GetLastWin32Error();
                    string errorMessage = String.Format(Resources.setWindowPosFailureFormat, "move and size", errorCode);
                    throw new Win32Exception(errorMessage);
                }
                if ((oldRect.Right - oldRect.Left) == shape.Width && (oldRect.Bottom - oldRect.Top) == shape.Height)
                {
                    // Don't sendF5 if the window size has not changed
                    sendF5 = false;
                }
            }
            success = WindowsApi.SetWindowPos(hwnd, WindowsApi.HWND_TOP, shape.X, shape.Y, shape.Width, shape.Height,
                 WindowsApi.SWP_NOZORDER | WindowsApi.SWP_NOACTIVATE |
                 ((movable) ? 0 : WindowsApi.SWP_NOMOVE) | ((sizable) ? 0 : WindowsApi.SWP_NOSIZE));
            if (!success)
            {
                // Get the last Win32 error code
                int errorCode = Marshal.GetLastWin32Error();
                string errorMessage = String.Format(Resources.setWindowPosFailureFormat, "move and size", errorCode);
                throw new Win32Exception(errorMessage);
            }
            if (sendF5)
            {
                //Attempt to cause redraw
                Slot.ShowWin(hwnd);
                SendKeys.SendWait("{F5}");
            }
        }

        /// <summary>
        /// Sends the window to the bottom of all the windows.
        /// </summary>
        /// <param name="hwnd"></param>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public static void SendToBottom(IntPtr hwnd)
        {
            bool success = WindowsApi.SetWindowPos(hwnd, WindowsApi.HWND_BOTTOM, 0, 0, 0, 0,
                 WindowsApi.SWP_NOACTIVATE | WindowsApi.SWP_NOMOVE | WindowsApi.SWP_NOSIZE);
            if (!success)
            {
                // Get the last Win32 error code
                int errorCode = Marshal.GetLastWin32Error();
                string errorMessage = String.Format(Resources.setWindowPosFailureFormat, "send to bottom", errorCode);
                throw new Win32Exception(errorMessage);
            }
        }

        /// <summary>
        /// Minimize the window.
        /// </summary>
        /// <exception cref="NoHwndException">Thrown if there was an attempt to access the hWND, 
        /// when hWND is null.</exception>
        /// <exception cref="Win32Exception">Thrown if there was Win32 error.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public static void MinimizeWin(IntPtr hwnd)
        {
            showWindowWrap(hwnd, WindowsApi.SW_MINIMIZE, "minimize");
        }

        /// <summary>
        /// Maximize the window.
        /// </summary>
        /// <exception cref="NoHwndException">Thrown if there was an attempt to access the hWND, 
        /// when hWND is null.</exception>
        /// <exception cref="Win32Exception">Thrown if there was Win32 error.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public static void MaximizeWin(IntPtr hwnd)
        {
            showWindowWrap(hwnd, WindowsApi.SW_MAXIMIZE, "maximize");
        }

        /// <summary>
        /// Restore the window.
        /// </summary>
        /// <exception cref="NoHwndException">Thrown if there was an attempt to access the hWND, 
        /// when hWND is null.</exception>
        /// <exception cref="Win32Exception">Thrown if there was Win32 error.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public static void RestoreWin(IntPtr hwnd)
        {
            showWindowWrap(hwnd, WindowsApi.SW_RESTORE, "restore");
        }

        /// <summary>
        /// Wrapper for methods that use ShowWindow
        /// </summary>
        /// <param name="hwnd">Window to adjust.</param>
        /// <param name="nCmdShow"></param>
        /// <param name="action">Text to be used as part of error message</param>
        /// <exception cref="NoHwndException">Thrown if there was an attempt to access the hWND, 
        /// when hWND is null.</exception>
        /// <exception cref="Win32Exception">Thrown if there was Win32 error.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        private static void showWindowWrap(IntPtr hwnd, int nCmdShow, string action)
        {
            bool success = WindowsApi.ShowWindow(hwnd, nCmdShow);
            if (!success)
            {
                // Get the last Win32 error code
                int errorCode = Marshal.GetLastWin32Error();
                string errorMessage = String.Format(Resources.showWindowFailureFormat, action, errorCode);
                throw new Win32Exception(errorMessage);
            }
        }

        /// <summary>
        /// Move window to the foreground
        /// </summary>
        /// <param name="hwnd">Window to adjust.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public static void ShowWin(IntPtr hwnd)
        {
            bool success = WindowsApi.SetForegroundWindow(hwnd);
            if (!success)
            {
                // Get the last Win32 error code
                int errorCode = Marshal.GetLastWin32Error();
                string errorMessage = String.Format(Resources.setWindowPosFailureFormat, "set as foreground", errorCode);
                throw new Win32Exception(errorMessage);
            }
        }
        #endregion
    }
}