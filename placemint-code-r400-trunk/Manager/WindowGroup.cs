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
    /// Collection of slots that contain windows with titles
    /// that match a specified regular expression.
    /// </summary>
    [XmlRoot("WindowGroup")]
    public class WindowGroup : IWIterator<Slot>, IDeepCloneable<WindowGroup>, ISwap, IFileNotFound
    {
        private const int NO_FREE_SLOT = -1;
        private const int USE_MAX_DEPTH = -1;
        private const int DRAGGING_MARGIN = 40;
        private const int RESIZING_MARGIN = 10;
        enum RecheckMatchResult { Match, NoMatch, NoChange }

        private const bool defaultMovable = true;
        private const bool defaultSizable = true;
        private const bool defaultMinable = true;
        private const bool defaultMaxable = true;
        private const bool defaultSendF5 = false;

        private GroupConfiguration _parent;
        private SlotList _slots;
        private WindowInfoDictionary _windows;
        private List<IntPtr> _noSwap;
        private TitleList _winTitleRegexList;
        private ClassList _winClassRegexList;
        private Hotkey _show;
        private Hotkey _restore;
        private Hotkey _min;
        private Hotkey _max;
        private Hotkey _bottom;
        private string _windowGroupTitle;

        private bool _movable;
        private bool _sizeable;
        private bool _minable;
        private bool _maxable;
        private bool _sendF5;

        private AppSettings _tempSettings;

        private delegate int FirstEmpty(int i, int depth);

        #region Constructors
        /// <summary>
        /// Default WindowGroup constructor
        /// </summary>
        public WindowGroup()
            : this(new TitleList(), new ClassList(), new SlotList(), "",
                defaultMovable, defaultSizable, defaultMinable, defaultMaxable, defaultSendF5,
                new Hotkey(), new Hotkey(), new Hotkey(), new Hotkey(), new Hotkey()) { }

        /// <summary>
        /// WindowGroup constructor
        /// </summary>
        /// <param name="winTitleRegexList">Regular expression that will be used to match window titles</param>
        /// <param name="winClassRegexList">Regular expression that will be used to match window classes</param>
        /// <param name="slots">A list of slots.</param>
        /// <param name="groupTitle">Display title for the window group.</param>
        public WindowGroup(TitleList winTitleRegexList, ClassList winClassRegexList, SlotList slots,
                string groupTitle)
            : this(winTitleRegexList, winClassRegexList, slots, groupTitle,
                defaultMovable, defaultSizable, defaultMinable, defaultMaxable, defaultSendF5,
                new Hotkey(), new Hotkey(), new Hotkey(), new Hotkey(), new Hotkey()) { }

        /// <summary>
        /// WindowGroup constructor
        /// </summary>
        /// <param name="winTitleRegexList">Regular expression that will be used to match window titles</param>
        /// <param name="winClassRegexList">Regular expression that will be used to match window classes</param>
        /// <param name="slots">A list of slots.</param>
        /// <param name="groupTitle">Display title for the window group.</param>
        /// <param name="movable"></param>
        /// <param name="sizable"></param>
        /// <param name="minable"></param>
        /// <param name="maxable"></param>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public WindowGroup(TitleList winTitleRegexList, ClassList winClassRegexList, SlotList slots,
                string groupTitle, bool movable, bool sizable, bool minable, bool maxable)
            : this(winTitleRegexList, winClassRegexList, slots, groupTitle, movable, sizable, minable, maxable,
                defaultSendF5, new Hotkey(), new Hotkey(), new Hotkey(), new Hotkey(), new Hotkey()) { }

        /// <summary>
        /// WindowGroup constructor
        /// </summary>
        /// <param name="winTitleRegexList">Regular expression that will be used to match window titles</param>
        /// <param name="winClassRegexList">Regular expression that will be used to match window classes</param>
        /// <param name="slots">A list of slots.</param>
        /// <param name="groupTitle">Display title for the window group.</param>
        /// <param name="movable"></param>
        /// <param name="sizable"></param>
        /// <param name="minable"></param>
        /// <param name="maxable"></param>
        /// <param name="sendF5"></param>
        /// <param name="show"></param>
        /// <param name="restore"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="bottom"></param>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public WindowGroup(TitleList winTitleRegexList, ClassList winClassRegexList, SlotList slots,
            string groupTitle, bool movable, bool sizable, bool minable, bool maxable, bool sendF5,
            Hotkey show, Hotkey restore, Hotkey min, Hotkey max, Hotkey bottom)
        {
            //Set _slots capacity and fill it with EmptySlots
            _slots = new SlotList(slots.Count);

            foreach (Slot s in slots)
            {
                _slots.Add(new Slot(s.Shape, s.Hotkey, s.Size));
            }

            _windows = new WindowInfoDictionary();
            _winTitleRegexList = winTitleRegexList;
            _winClassRegexList = winClassRegexList;
            _windowGroupTitle = groupTitle;
            _movable = movable;
            _sizeable = sizable;
            _minable = minable;
            _maxable = maxable;
            _sendF5 = sendF5;
            _show = show;
            _restore = restore;
            _min = min;
            _max = max;
            _bottom = bottom;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Get slots
        /// </summary>
        [XmlArray("Slots"), XmlArrayItem("Slot", typeof(Slot))]
        public SlotList Slots
        {
            get { return _slots; }
            set { _slots = value; }
        }

        /// <summary>
        /// Get the list of title match regular expression
        /// </summary>
        [XmlElement("TitleRegExMatchList", typeof(TitleList))]
        public TitleList WinTitleRegexList
        {
            get { return _winTitleRegexList; }
            set { _winTitleRegexList = value; }
        }

        /// <summary>
        /// Get the list of title match regular expression
        /// </summary>
        [XmlElement("ClassRegExMatchList", typeof(ClassList))]
        public ClassList WinClassRegexList
        {
            get { return _winClassRegexList; }
            set { _winClassRegexList = value; }
        }
        /// <summary>
        /// Get the window group title
        /// </summary>
        [XmlElement("Title")]
        public string WindowGroupTitle
        {
            get { return _windowGroupTitle; }
            set { _windowGroupTitle = value; }
        }
        /// <summary>
        /// Get the hotkey to show group
        /// </summary>
        [XmlElement("Show")]
        public Hotkey Show
        {
            get { return _show; }
            set { _show = value; }
        }
        /// <summary>
        /// Get the hotkey to restore group
        /// </summary>
        [XmlElement("Restore")]
        public Hotkey Restore
        {
            get { return _restore; }
            set { _restore = value; }
        }
        /// <summary>
        /// Get the hotkey to minimize group
        /// </summary>
        [XmlElement("Minimize")]
        public Hotkey Minimize
        {
            get { return _min; }
            set { _min = value; }
        }
        /// <summary>
        /// Get the hotkey to maximize group
        /// </summary>
        [XmlElement("Maximize")]
        public Hotkey Maximize
        {
            get { return _max; }
            set { _max = value; }
        }
        /// <summary>
        /// Get the hotkey to maximize group
        /// </summary>
        [XmlElement("Bottom")]
        public Hotkey Bottom
        {
            get { return _bottom; }
            set { _bottom = value; }
        }
        /// <summary>
        /// Can the size of a window be changed
        /// </summary>
        [XmlAttribute("sizable")]
        public bool Sizable
        {
            get { return _sizeable; }
            set { _sizeable = value; }
        }
        /// <summary>
        /// Can the size of a window be changed
        /// </summary>
        [XmlAttribute("movable")]
        public bool Movable
        {
            get { return _movable; }
            set { _movable = value; }
        }
        /// <summary>
        /// Can the window be minimized
        /// </summary>
        [XmlAttribute("minable")]
        public bool Minable
        {
            get { return _minable; }
            set { _minable = value; }
        }
        /// <summary>
        /// Can the window be minimized
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [XmlAttribute("maxable")]
        public bool Maxable
        {
            get { return _maxable; }
            set { _maxable = value; }
        }
        /// <summary>
        /// Should F5 be sent when the location or shape of a window is changed
        /// </summary>
        [XmlAttribute("sendF5")]
        public bool SendF5
        {
            get { return _sendF5; }
            set { _sendF5 = value; }
        }
        /// <summary>
        /// Get Parent GroupConfiguration
        /// </summary>
        [XmlIgnore]
        public GroupConfiguration Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
        /// <summary>
        /// Get Parent GroupConfiguration
        /// </summary>
        [XmlIgnore]
        public WindowInfoDictionary Windows
        {
            get { return _windows; }
        }

        /// <summary>
        /// Get number of slots in the group.
        /// </summary>
        public int SlotsCount
        {
            get { return _slots.Count; }
        }

        /// <summary>
        /// Get total number of windows that all of the slots can hold.
        /// </summary>
        [XmlIgnore]
        public int TotalPossibleCount
        {
            get
            {
                int count = 0;
                foreach (Slot s in Slots)
                {
                    count += s.Size;
                }
                return count;
            }
        }

        /// <summary>
        /// Get number of slots that are occupied by windows.
        /// </summary>
        public int OccupiedCount
        {
            get { return _windows.Count; }
        }

        /// <summary>
        /// Get the windows in all of the slots.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [XmlIgnore]
        public List<IntPtr> Hwnds
        {
            get
            {
                List<IntPtr> HWNDs = new List<IntPtr>();
                foreach (Slot s in Slots)
                {
                    HWNDs.AddRange(s.Hwnds);
                }
                return HWNDs;
            }
        }

        private int MaxDepth
        {
            get
            {
                int maxDepth = 0;
                foreach (Slot slot in _slots)
                {
                    if (slot.Size > maxDepth)
                    {
                        maxDepth = slot.Size;
                    }
                }
                return maxDepth;
            }
        }
        #endregion

        /// <summary>
        /// Find first slot that has free space. Start at the first index.
        /// </summary>
        /// <param name="endAt">Index to stop searching</param>
        /// <exception cref="OutOfRangeException">Thrown if startAt is out of range.</exception>
        /// <exception cref="AllSlotsFullException">Thrown if all slots are full.</exception>
        /// <returns>Index of first slot that has free sapce</returns>
        private int firstEmptySlot(int endAt)
        {
            if (_tempSettings.BreadthFirstFreeSlot)
            {
                return firstEmptySlotBF(endAt, USE_MAX_DEPTH);
            }
            Logger.Debug("endAtSlot|{0}|, Slots.Count|{1}|\n{2}", endAt, _slots.Count, _slots.ToString());
            if (endAt < 0 || endAt > _slots.Count)
            {
                throw new OutOfRangeException(String.Format(Resources.illegalSlotIndex, _slots.Count, endAt + 1));
            }
            int freeSlot = 0;
            while (freeSlot < endAt && !_slots[freeSlot].HasFree)
            {
                freeSlot++;
            }
            if (freeSlot == endAt)
            {
                throw new AllSlotsFullException();
            }
            return freeSlot;
        }

        /// <summary>
        /// Find first slot that is an EmptySlot. Search all slots.
        /// </summary>
        /// <exception cref="AllSlotsFullException">Thrown if all slots are full.</exception>
        /// <returns>Index of first EmptySlot</returns>
        private int firstEmptySlot()
        {
            return firstEmptySlot(_slots.Count);
        }

        /// <summary>
        /// Find first slot that has free space. Start at the first index. Override for FirstEmpty delegate
        /// </summary>
        /// <param name="endAt">Index to stop searching</param>
        /// <param name="depth">Unused</param>
        /// <exception cref="OutOfRangeException">Thrown if startAt is out of range.</exception>
        /// <exception cref="AllSlotsFullException">Thrown if all slots are full.</exception>
        /// <returns>Index of first slot that has free sapce</returns>
        private int firstEmptySlot(int endAt, int depth)
        {
            return firstEmptySlot(endAt);
        }

        /// <summary>
        /// Find first slot that has free space, searching across all slots before decending into slots. Start at the first index.
        /// </summary>
        /// <param name="endAtSlot">Index to stop searching</param>
        /// <param name="endAtDepth">Depth to reach before stoping the search</param>
        /// <exception cref="OutOfRangeException">Thrown if startAt is out of range.</exception>
        /// <exception cref="AllSlotsFullException">Thrown if all slots are full.</exception>
        /// <returns>Index of first slot that has free sapce</returns>
        private int firstEmptySlotBF(int endAtSlot, int endAtDepth)
        {
            int freeSlot = NO_FREE_SLOT;
            int maxDepth = MaxDepth;
            if (endAtDepth == USE_MAX_DEPTH)
            {
                endAtDepth = maxDepth;
            }
            Logger.Trace("BF:endAtSlot|{0}|, endAtDepth|{1}|, Slots.Count|{2}|", endAtSlot, endAtDepth, _slots.Count);
            if (endAtSlot < 0 || endAtSlot > _slots.Count)
            {
                throw new OutOfRangeException(String.Format(Resources.illegalSlotIndex, _slots.Count, endAtSlot + 1));
            }
            bool breaker = false;
            for (int depthTraverse = 0; depthTraverse < maxDepth; depthTraverse++)
            {
                for (int i = 0; i < _slots.Count; i++)
                {
                    Logger.Trace("firstEmptySlotBF:depthTraverse|{0}|i|{1}", depthTraverse, i);
                    if (i == endAtSlot && depthTraverse == endAtDepth)
                    {
                        Logger.Trace("endAt stopped");
                        breaker = true;
                        break;
                    }
                    if (_slots[i].IsStackSlotFree(depthTraverse))
                    {
                        freeSlot = i;
                        Logger.Trace("freeSlot: {0}", freeSlot);
                        breaker = true;
                        break;
                    }
                }
                if (breaker)
                {
                    break;
                }
            }
            if (freeSlot == NO_FREE_SLOT)
            {
                throw new AllSlotsFullException();
            }
            return freeSlot;
        }

        private bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam)
        {
            Logger.Trace("EnumWindowsProc:hWnd|{0}|, Slots.Count|{1}|, OccupiedCount|{2}|", hwnd, _slots.Count, OccupiedCount);
            //skip if full, window is already in a slot of any groups in the configuration, or window is not visible
            if (OccupiedCount != TotalPossibleCount && !_parent.WindowExists(hwnd) && WindowsApi.IsWindowVisible(hwnd))
            {
                string theClass, theTitle;
                if (isMatch(hwnd, out theClass, out theTitle))
                {
                    Logger.Trace("Window Matches");
                    //just add to the first free slot for enumeration
                    //finding the closest slot will be done later
                    _slots[firstEmptySlot()].AddHwnd(hwnd);
                    _windows.Add(hwnd, new WindowInfo(theClass, theTitle));
                    _noSwap.Add(hwnd);
                }
            }
            return true; //Continue the Enum loop
        }

        /// <summary>
        /// Returns the caption of a windows by given HWND identifier.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public static string GetWindowTitle(IntPtr hwnd)
        {
            StringBuilder title = new StringBuilder(256);
            int titleLength = WindowsApi.GetWindowText(hwnd, title, title.Capacity + 1);
            title.Length = titleLength;

            return title.ToString();
        }

        /// <summary>
        /// Returns the caption of a windows by given HWND identifier.
        /// </summary>
        private string GetClassName(IntPtr hwnd)
        {
            StringBuilder className = new StringBuilder(256);
            int classLength = WindowsApi.GetClassName(hwnd, className, className.Capacity + 1);
            className.Length = classLength;

            return className.ToString();
        }

        private RecheckMatchResult isMatchRecheck(IntPtr hwnd, WindowInfo oldInfo, bool strongRecheck,
            out string theClass, out string theTitle)
        {
            theTitle = GetWindowTitle(hwnd);
            theClass = GetClassName(hwnd);

            if (!strongRecheck && theClass.Equals(oldInfo.TheClass) && theTitle.Equals(oldInfo.TheTitle))
            {
                return RecheckMatchResult.NoChange;
            }

            return (isMatchWorker(theClass, theTitle, true)) ? RecheckMatchResult.Match : RecheckMatchResult.NoMatch;
        }

        private bool isMatch(IntPtr hwnd, out string theClass, out string theTitle)
        {
            theTitle = GetWindowTitle(hwnd);
            theClass = GetClassName(hwnd);
            return isMatchWorker(theClass, theTitle, false);
        }

        private bool isMatchWorker(string theClass, string theTitle, bool recheck)
        {
            if (theClass == "tooltips_class32")
            {
                return false;
            }
            if (!recheck && Logger.Level == LoggingLevel.Trace)
            {
                Logger.Trace(_slots.ToString());
                Logger.Trace(_windows.ToString());
            }
            Logger.Trace("theTitle|{0}|theClass|{1}|\n{2}|\n{3}", theTitle, theClass, _winTitleRegexList.ToString(),
                _winClassRegexList.ToString());
            bool classListEmpty = _winClassRegexList.Count == 0;
            bool titleListEmpty = _winTitleRegexList.Count == 0;
            bool matchesClass = !classListEmpty && _winClassRegexList.IsMatch(theClass);
            bool matchesTitle = !titleListEmpty && _winTitleRegexList.IsMatch(theTitle);
            if (_tempSettings.OrTitleMatching)
            {
                return matchesClass || matchesTitle;
            }
            //Window matches if:
            //  (there are no class expressions AND the title matches a title expression) OR
            //  (the class matches a class expression AND
            //      (there are no title expressions OR the title matches a title expression))
            return ((classListEmpty && matchesTitle) ||
                (matchesClass && (titleListEmpty || _winTitleRegexList.IsMatch(theTitle))));
        }

        /// <summary>
        /// <para>Check to see that all windows that match the regular expression
        /// are placed in slots.</para>
        /// Check that all windows in slots still exist.
        /// </summary>
        /// <returns>True if there were window changes.</returns>
        /// <exception cref="Win32Exception">Thrown if there is a Win32 Error</exception>
        public bool Refresh(AppSettings appSetting)
        {
            return Refresh(appSetting, null);
        }

        /// <summary>
        /// <para>Check to see that all windows that match the regular expression
        /// are placed in slots.</para>
        /// Check that all windows in slots still exist.
        /// </summary>
        /// <returns>True if there were window changes.</returns>
        /// <exception cref="Win32Exception">Thrown if there is a Win32 Error</exception>
        public bool Refresh(AppSettings appSetting, IDrawOverlay drawOverlay)
        {
            bool strongRecheck = _tempSettings != null && _tempSettings.OrTitleMatching != appSetting.OrTitleMatching;
            _tempSettings = appSetting;
            if (Logger.Level == LoggingLevel.Trace)
            {
                Logger.Trace("Refresh group:{0}", this.ToString());
            }
            bool result = false;
            //Clear slots that don't have windows anymore
            foreach(Slot slot in _slots)
            {
                if (!slot.IsEmpty)
                {
                    List<IntPtr> toRemove = new List<IntPtr>();
                    foreach (IntPtr hwnd in slot.Hwnds)
                    {
                        if (!_windows.ContainsKey(hwnd))
                        {
                            throw new HwndMissingException(hwnd);
                        }
                        if (WindowsApi.IsWindowVisible(hwnd))
                        {
                            if (!_minable && Slot.IsMinimized(hwnd))
                            {
                                //restore windows that are not allowed to be minimized
                                Logger.Debug("Restore unallowed minimization: {0}", _windows[hwnd]);
                                try
                                {
                                    Slot.RestoreWin(hwnd);
                                }
                                catch (Win32Exception e)
                                {
                                    Logger.Debug(e.Message);
                                }
                            }
                            if (!_maxable && Slot.IsMaximized(hwnd))
                            {
                                //restore windows that are not allowed to be minimized
                                Logger.Debug("Restore unallowed maximization: {0}", _windows[hwnd]);
                                try
                                {
                                    Slot.RestoreWin(hwnd);
                                }
                                catch (Win32Exception e)
                                {
                                    Logger.Debug(e.Message);
                                }
                            }
                            //check window title still matches
                            string theTitle, theClass;
                            switch (isMatchRecheck(hwnd, _windows[hwnd], strongRecheck, out theClass, out theTitle))
                            {
                                case RecheckMatchResult.Match:
                                    _windows[hwnd].TheTitle = theTitle;
                                    _windows[hwnd].TheClass = theClass;
                                    result = true;
                                    break;
                                case RecheckMatchResult.NoMatch:
                                    //title or class doesn't match, so remove and make the slot empty
                                    Logger.Debug("Window changed from |{0}| to |{1}:{2}| so remove", _windows[hwnd], theTitle, theClass);
                                    toRemove.Add(hwnd);
                                    result = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            //window is closed, so remove title and make the slot empty
                            Logger.Debug("Window closed: {0}", _windows[hwnd]);
                            toRemove.Add(hwnd);
                            result = true;
                        }
                    }
                    slot.RemoveHwnds(toRemove);
                    foreach (IntPtr hwnd in toRemove)
                    {
                        Logger.Debug("Removing window: {0}-{1}", hwnd, _windows[hwnd].ToString());
                        _windows.Remove(hwnd);
                    }
                }
            }
            int preEnumCount = OccupiedCount;
            Logger.Trace("Start enumeration");
            _noSwap = new List<IntPtr>();
            WindowsApi.EnumWindowsProc enumfunc = new WindowsApi.EnumWindowsProc(EnumWindowsProc);
            bool success = WindowsApi.EnumDesktopWindows(IntPtr.Zero, enumfunc, IntPtr.Zero);
            if (!success)
            {
                // Get the last Win32 error code
                int errorCode = Marshal.GetLastWin32Error();

                string errorMessage = String.Format(Resources.enumDesktopWindowsFailureFormat, errorCode);
                throw new Win32Exception(errorMessage);
            }
            if (preEnumCount != OccupiedCount)
            {
                result = true;
            }

            //position windows
            for (int i = 0; i < Slots.Count; i++)
            {
                //skip if:
                //  empty, minimized, maximized, or drag is happening
                //minimized && maximized windows will never have correct location or size
                //but they are allowed to be in this state, so skip
                Logger.Trace("Start positioning");

                for (int j = 0; j < Slots[i].Hwnds.Count; j++)
                {
                    IntPtr hwnd = Slots[i].Hwnds[j];
                    WindowsApi.RECT rect;
                    WindowsApi.GetWindowRect(hwnd, out rect);
                    if (!isWindowResizing(hwnd, rect))
                    {
                        if (isWindowDragging(hwnd, rect))
                        {
                            int closest = ClosestSlot(rect);
                            if (appSetting.OverlayClosest)
                            {
                                drawOverlay.DrawOverlay(Slots[closest].Shape);
                            }
                        }
                        else if (!Slot.IsMinimized(hwnd) && !Slot.IsMaximized(hwnd))
                        {
                            if (Slots[i].Shape.X != rect.Left || Slots[i].Shape.Y != rect.Top)
                            {
                                Logger.Debug("Position Slots[{0}].HWNDs[j]({1})", i, j, _windows[hwnd]);
                                //we need to move window
                                //only swap if enabled and not in newFind special case
                                bool inNoSwap = _noSwap.Contains(hwnd);
                                if (appSetting.DragDropSwap && (appSetting.SwapOnNewFind || !_noSwap.Contains(hwnd)))
                                {
                                    int closest = ClosestSlot(rect);
                                    if (i != closest)
                                    {
                                        Logger.Debug("Swap with Slots[{0}]", closest);
                                        if (SwapHwnds(i, j, closest))
                                        {
                                            try
                                            {
                                                //hWND is not owned by closest
                                                Slot.MoveAndSizeWin(hwnd, Slots[closest].Shape, Movable, Sizable, SendF5, inNoSwap);
                                            }
                                            catch (Win32Exception e)
                                            {
                                                Logger.Debug(e.Message);
                                            }
                                            //result is true because windows swapped
                                            result = true;
                                        }
                                    }
                                }
                                if (!Slots[i].IsEmpty && Slots[i].Hwnds[j] != Slot.EMPTY)
                                {
                                    try
                                    {
                                        //move the window that was associated with closest or move current window back to it's assigned slot
                                        Slot.MoveAndSizeWin(Slots[i].Hwnds[j], Slots[i].Shape, Movable, Sizable, SendF5, inNoSwap);
                                    }
                                    catch (Win32Exception e)
                                    {
                                        Logger.Debug(e.Message);
                                    }
                                }
                            }
                            else if (Slots[i].Shape.Width != rect.Right - rect.Left || Slots[i].Shape.Height != rect.Bottom - rect.Top)
                            {
                                try
                                {
                                    //force the window back to the slot size
                                    Slot.MoveAndSizeWin(Slots[i].Hwnds[j], Slots[i].Shape, Movable, Sizable, SendF5, false);
                                }
                                catch (Win32Exception e)
                                {
                                    Logger.Debug(e.Message);
                                }
                            }
                        }
                    }
                }
                Slots[i].Hwnds.RemoveAll(Slot.MatchEmpty);
            }

            if (appSetting.RippleForward)
            {
                rippleForward(ref result);
            }
            return result;
        }

        private void rippleForward(ref bool result)
        {
            if (_tempSettings.BreadthFirstFreeSlot)
            {
                rippleForwardBF(ref result);
            }
            else
            {
                for (int i = 0; i < Slots.Count; i++)
                {
                    if (!Slots[i].IsEmpty)
                    {
                        for (int j = 0; j < Slots[i].Hwnds.Count; j++)
                        {
                            rippleWorker(i, j, new FirstEmpty(firstEmptySlot), ref result);
                        }
                    }
                    Slots[i].Hwnds.RemoveAll(Slot.MatchEmpty);
                }
            }
        }

        private void rippleForwardBF(ref bool result)
        {
            Logger.Trace("start rippleForwardBF: {0}", _slots.ToString());
            int maxDepth = MaxDepth;
            for (int depth = 0; depth < maxDepth; depth++)
            {
                for (int i = 0; i < Slots.Count; i++)
                {
                    Logger.Trace("depth|{0}|i|{1}|--size|{2}|count|{3}|", depth, i, Slots[i].Size, Slots[i].Hwnds.Count);
                    if (depth < Slots[i].Size && depth < Slots[i].Hwnds.Count)
                    {
                        rippleWorker(i, depth, new FirstEmpty(firstEmptySlotBF), ref result);
                    }
                }
            }
            foreach (Slot s in Slots)
            {
                s.Hwnds.RemoveAll(Slot.MatchEmpty);
            }
            Logger.Trace("end rippleForwardBF: {0}", _slots.ToString());
        }

        private void rippleWorker(int i, int depth, FirstEmpty firstEmpty, ref bool result)
        {
            IntPtr hwnd = Slots[i].Hwnds[depth];
            if (hwnd != Slot.EMPTY)
            {
                try
                {
                    int first = firstEmpty(i, depth);
                    Logger.Trace("first|{0}|i|{1}|do_swap|{2}|", first, i, first != i);
                    if (first != i)
                    {
                        SwapHwnds(i, depth, first);
                        //i is now empty and first is not
                        try
                        {
                            Slot.MoveAndSizeWin(hwnd, Slots[first].Shape, Movable, Sizable, SendF5);
                        }
                        catch (Win32Exception err)
                        {
                            Logger.Debug(err.Message);
                        }
                        result = true;
                    }
                }
                catch (AllSlotsFullException)
                {
                    Logger.Trace("No Free Slot");
                }
            }
        }

        /// <summary>
        /// Find the slot closest to the center point
        /// </summary>
        /// <remarks>Wrapper to be used for main loop swapping</remarks>
        /// <param name="rect"></param>
        /// <returns>index of closest slot</returns>
        public int ClosestSlot(WindowsApi.RECT rect)
        {
            RectangleWrap rectW = new RectangleWrap(rect);
            return ClosestSlot(Slot.CalculateCenter(rectW), rectW);
        }

        /// <summary>
        /// Find the slot closest to the center point
        /// </summary>
        /// <param name="winCenter"></param>
        /// <param name="rect">Shape to use for adjustment when _sizeable is false</param>
        /// <returns>index of closest slot</returns>
        public int ClosestSlot(Point winCenter, RectangleWrap rect)
        {
            Logger.Trace("winCenter ({0}, {1})", winCenter.X, winCenter.Y);
            int index = 0;
            double dist = double.MaxValue;
            double curDist = 0;
            for (int i = 0; i < Slots.Count; i++)
            {
                if (_sizeable)
                {
                    curDist = distance(Slots[i].Center, winCenter);
                }
                else
                {
                    //make a shape using the size of the window that is being re-positioned
                    RectangleWrap tempSlotShape = new RectangleWrap(Slots[i].Shape.X, Slots[i].Shape.Y, rect.Width, rect.Height);
                    curDist = distance(Slot.CalculateCenter(tempSlotShape), winCenter);
                }
                Logger.Trace("{0} dist|{1}| curDist|{2}|", i, dist, curDist);
                if (dist > (curDist))
                {
                    index = i;
                    dist = curDist;
                }
            }
            Logger.Trace("closest = {0}", index);
            return index;
        }

        private double distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow((double)(p1.X - p2.X), 2.0)
                + Math.Pow((double)(p1.Y - p2.Y), 2.0));
        }

        /// <summary>
        /// Tests if two objects are the same. Slots are NOT checked to have matching hWND's.
        /// </summary>
        /// <param name="other">WindowGroup to test against.</param>
        /// <returns></returns>
        public bool Equals(WindowGroup other)
        {
            if (other == null)
            {
                return false;
            }
            if ((Object)this == (Object)other) //identity compare
            {
                return true;
            }
            if (this.Minable != other.Minable || this.Movable != other.Movable ||
                    this.Sizable != other.Sizable || !this._windowGroupTitle.Equals(other._windowGroupTitle) ||
                    !this._winTitleRegexList.Equals(other._winTitleRegexList) || !this._show.Equals(other._show) ||
                    !this._restore.Equals(other._restore) || !this._min.Equals(other._min) ||
                    !this._max.Equals(other._max) || !this._bottom.Equals(other._bottom) ||
                    this.SlotsCount != other.SlotsCount)
            {
                return false;
            }
            for (int i = 0; i < this.SlotsCount; i++)
            {
                if (!this._slots[i].Shape.Equals(other._slots[i].Shape))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Tests if two objects are the same
        /// </summary>
        /// <param name="obj">object to test against.</param>
        /// <returns></returns>
        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            WindowGroup wg = obj as WindowGroup;
            return Equals(wg);
        }

        /// <summary>
        /// Get object hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _slots.GetHashCode() + _winTitleRegexList.GetHashCode() + _windowGroupTitle.GetHashCode();
        }

        /// <summary>
        /// String representation of WindowGroup
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Group:\n_windowGroupTitle|");
            sb.Append(_windowGroupTitle);
            sb.Append("|\n_winTitleRegexList|");
            sb.Append(_winTitleRegexList.ToString());
            sb.Append("|\n_winClassRegexList|");
            sb.Append(_winClassRegexList.ToString());
            sb.Append("|\n");
            sb.AppendLine(_slots.ToString());
            sb.AppendLine(_windows.ToString());
            sb.Append("\n  Show|");
            sb.Append(_show.ToString());
            sb.Append("\n  Restore|");
            sb.Append(_restore.ToString());
            sb.Append("\n  Min|");
            sb.Append(_min.ToString());
            sb.Append("\n  Max|");
            sb.Append(_max.ToString());
            sb.Append("\n  Bottom|");
            sb.Append(_bottom.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>New object with all of the same values.</returns>
        public WindowGroup DeepClone()
        {
            WindowGroup wg = new WindowGroup();
            wg.WindowGroupTitle = this.WindowGroupTitle;
            foreach (Slot slot in this.Slots)
            {
                wg.Slots.Add(slot.DeepClone());
            }
            wg._winTitleRegexList = this._winTitleRegexList.DeepClone();
            wg._winClassRegexList = this._winClassRegexList.DeepClone();
            wg._windows = this._windows.DeepClone();
            wg.Movable = this.Movable;
            wg.Sizable = this.Sizable;
            wg.Minable = this.Minable;
            wg.Maxable = this.Maxable;
            wg.SendF5 = this.SendF5;
            wg.Show = this.Show;
            wg.Restore = this.Restore;
            wg.Minimize = this.Minimize;
            wg.Maximize = this.Maximize;
            wg.Bottom = this.Bottom;
            return wg;
        }

        /// <summary>
        /// Get the slot and hwnd indecies of a hwnd.
        /// </summary>
        /// <param name="hwnd">Window being searched for.</param>
        /// <param name="iSlot">Index of slot window was found in.</param>
        /// <param name="iHwnd">Index of slot hwnd.</param>
        /// <returns>True if hwnd was found.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public bool FindWindow(IntPtr hwnd, out int iSlot, out int iHwnd)
        {
            iSlot = -1;
            iHwnd = -1;
            for (int k = 0; k < _slots.Count; k++)
            {
                if (!_slots[k].IsEmpty && _slots[k].Hwnds.Contains(hwnd))
                {
                    iSlot = k;
                    iHwnd = _slots[k].Hwnds.IndexOf(hwnd);
                    break;
                }
            }
            return (iSlot != -1);
        }

        /// <summary>
        /// Swap two Slots
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <exception cref="OutOfRangeException">Thrown is the index is out of range.</exception>
        public void SwapItems(int index1, int index2)
        {
            if (index1 < 0 && index1 >= _slots.Count && index2 < 0 && index2 >= _slots.Count)
            {
                throw new OutOfRangeException(Resources.illegalSlotIndex);
            }
            Slot slot = _slots[index1];
            _slots[index1] = _slots[index2];
            _slots[index2] = slot;
        }

        /// <summary>
        /// Move a handle of one slot to another, swap if necessary
        /// Call
        ///     Slots[i].Hwnds.RemoveAll(Slot.MatchEmpty);
        /// after itteracting over a slot to clean out EMPTYs
        /// </summary>
        /// <param name="currentSlot">Index of the slot being moved from</param>
        /// <param name="currentHwnd">Index of the hWND being moved from</param>
        /// <param name="targetSlot">Index of the slot being moved to</param>
        /// <returns>true when targetSlot should be moved/sized</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public bool SwapHwnds(int currentSlot, int currentHwnd, int targetSlot)
        {
            IntPtr temp;
            return SwapHwnds(currentSlot, currentHwnd, targetSlot, out temp);
        }

        /// <summary>
        /// Move a handle of one slot to another, swap if necessary
        /// Call
        ///     Slots[i].Hwnds.RemoveAll(Slot.MatchEmpty);
        /// after itteracting over a slot to clean out EMPTYs
        /// </summary>
        /// <param name="currentSlot">Index of the slot being moved from</param>
        /// <param name="currentHwnd">Index of the hWND being moved from</param>
        /// <param name="targetSlot">Index of the slot being moved to</param>
        /// <param name="targetHwnd">The hWND of the other wind that was moved</param>
        /// <returns>true when targetSlot should be moved/sized</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public bool SwapHwnds(int currentSlot, int currentHwnd, int targetSlot, out IntPtr targetHwnd)
        {
            IntPtr hwnd = Slots[currentSlot].Hwnds[currentHwnd];
            targetHwnd = Slot.EMPTY;
            if (!Slots[targetSlot].IsStack)
            {
                //_____ -> single
                if (Slots[currentSlot].IsStack)
                {
                    if (Slots[targetSlot].IsEmpty)
                    {
                        Slots[targetSlot].Hwnds.Add(hwnd);
                        //can't remove yet because it will break the itterator, do so later
                        Slots[currentSlot].Hwnds[currentHwnd] = Slot.EMPTY;
                    }
                    else
                    {
                        targetHwnd = Slots[targetSlot].Hwnds[0];
                        Slots[currentSlot].Hwnds[currentHwnd] = Slots[targetSlot].Hwnds[0];
                        Slots[targetSlot].Hwnds[0] = hwnd;
                    }
                }
                else
                {
                    if (Slots[targetSlot].IsEmpty)
                    {
                        Slots[currentSlot].Hwnds.Clear();
                        Slots[targetSlot].Hwnds.Add(hwnd);
                    }
                    else
                    {
                        targetHwnd = Slots[targetSlot].Hwnds[0];
                        Slots[currentSlot].Hwnds[0] = _slots[targetSlot].Hwnds[0];
                        Slots[targetSlot].Hwnds[0] = hwnd;
                    }
                }
            }
            else
            {
                //_____ -> stack
                if (Slots[targetSlot].HasFree)
                {
                    Slots[targetSlot].Hwnds.Add(hwnd);
                    //can't remove yet because it will break the itterator, do so later
                    Slots[currentSlot].Hwnds[currentHwnd] = Slot.EMPTY;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        [SuppressMessage("Microsoft.Naming", "CA1704")]
        private bool isWindowResizing(IntPtr hwnd, WindowsApi.RECT rect)
        {

            Point pos = Control.MousePosition;
            return Control.MouseButtons.Equals(MouseButtons.Left) && hwnd.Equals(WindowsApi.GetForegroundWindow()) &&
                ((isWithinWidth(pos, rect) && 
                    isWithinMargin(pos.Y, rect.Top, RESIZING_MARGIN) || isWithinMargin(pos.Y, rect.Bottom, RESIZING_MARGIN)) ||
                (isWithinHeight(pos, rect) && 
                    isWithinMargin(pos.X, rect.Right, RESIZING_MARGIN) || isWithinMargin(pos.X, rect.Left, RESIZING_MARGIN)));
        }

        [SuppressMessage("Microsoft.Naming", "CA1704")]
        private bool isWindowDragging(IntPtr hwnd, WindowsApi.RECT rect)
        {
            Point pos = Control.MousePosition;
            return Control.MouseButtons.Equals(MouseButtons.Left) && hwnd.Equals(WindowsApi.GetForegroundWindow()) &&
                isWithinWidth(pos, rect) && pos.Y >= rect.Top && pos.Y <= rect.Top + DRAGGING_MARGIN;
        }

        private bool isWithinWidth(Point pos, WindowsApi.RECT rect)
        {
            return pos.X >= rect.Left && pos.X <= rect.Right;
        }

        private bool isWithinHeight(Point pos, WindowsApi.RECT rect)
        {
            return pos.Y >= rect.Top && pos.Y <= rect.Bottom;
        }

        private bool isWithinMargin(int value, int center, int range)
        {
            double delta = range/2.0;
            return value >= center - delta && value <= center + delta;
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
            if (_it_index >= _slots.Count)
            {
                throw new OutOfRangeException(String.Format(Resources.illegalSlotIndex, _slots.Count, _it_index + 1));
            }
            _it_index++;
        }

        /// <summary>
        /// Are there more slots?
        /// </summary>
        /// <returns></returns>
        public bool IteratorHasMore()
        {
            return (_it_index < _slots.Count);
        }

        /// <summary>
        /// The current element (W/R access).
        /// </summary>
        [XmlIgnore]
        public Slot IteratorCurrent
        {
            get { return _slots[_it_index]; }
            set { _slots[_it_index] = value; }
        }
        #endregion
    }
}