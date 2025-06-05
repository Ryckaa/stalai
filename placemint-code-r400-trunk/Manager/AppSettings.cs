using System.Xml.Serialization;

//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Manager
{
    /// <summary>
    /// Class containing application settings.
    /// </summary>
    [XmlRoot("AppSettings")]
    public class AppSettings : IDeepCloneable<AppSettings>, IFileNotFound
    {
        private const bool DEFAULT_MIN_TO_TRAY = true;
        private const bool DEFAULT_DRAG_DROP_SWAP = true;
        private const bool DEFAULT_SWAP_ON_NEW_FIND = false;
        private const bool DEFAULT_HOTKEY_SWAP = false;
        private const bool DEFAULT_GROUP_HOTKEY = true;
        private const bool DEFAULT_RIPPLE_FORWARD = false;
        private const bool DEFAULT_PAUSED = false;
        private const bool DEFAULT_OVERLAY_CLOSEST = false;
        private const bool DEFAULT_OR_TITLE_MATCHING = false;
        private const bool DEFAULT_BREADTH_FIRST_FREE_SLOT = false;
        private const int DEFAULT_UPDATE_FREQUENCY = 200;
        private const string DEFAULT_REGEX_LIST_FILENAME = "PlaceMint_regexList.xml";
        private const string DEFAULT_SLOT_TEMPLATE_FILENAME = "PlaceMint_slotTemplateList.xml";
        private const string DEFAULT_CONFIG_FILENAME = "PlaceMint_WindowGroups.config";
        private const LoggingLevel DEFAULT_LOGGING_LEVEL = LoggingLevel.Fatal;
        static readonly RectangleWrap DEFAULT_WINDOW_RECT = new RectangleWrap(50, 50, 600, 300);
        static readonly Hotkey DEFAULT_PAUSED_HOTKEY = new Hotkey(ModifyingKeys.Alt | ModifyingKeys.Shift, System.Windows.Forms.Keys.P);
        static readonly Hotkey DEFAULT_RIPPLE_HOTKEY = new Hotkey(ModifyingKeys.Alt | ModifyingKeys.Shift, System.Windows.Forms.Keys.R);

        private bool _minToTray;
        private bool _dragDropSwap;
        private bool _swapOnNewFind;
        private bool _hotkeySwap;
        private bool _groupHotkeys;
        private bool _rippleForward;
        private bool _paused;
        private bool _overlayClosest;
        private bool _orTitleMatching;
        private bool _breadthFirstFreeSlot;
        private int _updateFrequency;
        private string _regexListFileName;
        private string _slotTemplateListFileName;
        private string _configFileName;
        private RectangleWrap _windowRect;
        private LoggingLevel _loggingLevel;
        private Hotkey _pauseHotkey;
        private Hotkey _rippleHotkey;
        private RecentFiles _recent;

        public AppSettings()
        {
            _minToTray = DEFAULT_MIN_TO_TRAY;
            _dragDropSwap = DEFAULT_DRAG_DROP_SWAP;
            _swapOnNewFind = DEFAULT_SWAP_ON_NEW_FIND;
            _hotkeySwap = DEFAULT_HOTKEY_SWAP;
            _groupHotkeys = DEFAULT_GROUP_HOTKEY;
            _rippleForward = DEFAULT_RIPPLE_FORWARD;
            _paused = DEFAULT_PAUSED;
            _overlayClosest = DEFAULT_OVERLAY_CLOSEST;
            _orTitleMatching = DEFAULT_OR_TITLE_MATCHING;
            _breadthFirstFreeSlot = DEFAULT_BREADTH_FIRST_FREE_SLOT;
            _updateFrequency = DEFAULT_UPDATE_FREQUENCY;
            _regexListFileName = DEFAULT_REGEX_LIST_FILENAME;
            _slotTemplateListFileName = DEFAULT_SLOT_TEMPLATE_FILENAME;
            _configFileName = DEFAULT_CONFIG_FILENAME;
            _windowRect = DEFAULT_WINDOW_RECT;
            _loggingLevel = DEFAULT_LOGGING_LEVEL;
            _pauseHotkey = DEFAULT_PAUSED_HOTKEY;
            _rippleHotkey = DEFAULT_RIPPLE_HOTKEY;
            _recent = new RecentFiles();
        }

        public bool MinToTray
        {
            get { return _minToTray; }
            set { _minToTray = value; }
        }
        public bool DragDropSwap
        {
            get { return _dragDropSwap; }
            set { _dragDropSwap = value; }
        }
        public bool SwapOnNewFind
        {
            get { return _swapOnNewFind; }
            set { _swapOnNewFind = value; }
        }
        public bool HotkeySwap
        {
            get { return _hotkeySwap; }
            set { _hotkeySwap = value; }
        }
        public bool GroupHotkeys
        {
            get { return _groupHotkeys; }
            set { _groupHotkeys = value; }
        }
        public bool RippleForward
        {
            get { return _rippleForward; }
            set { _rippleForward = value; }
        }
        public bool Paused
        {
            get { return _paused; }
            set { _paused = value; }
        }
        public bool OverlayClosest
        {
            get { return _overlayClosest; }
            set { _overlayClosest = value; }
        }
        public bool OrTitleMatching
        {
            get { return _orTitleMatching; }
            set { _orTitleMatching = value; }
        }
        public bool BreadthFirstFreeSlot
        {
            get { return _breadthFirstFreeSlot; }
            set { _breadthFirstFreeSlot = value; }
        }
        public int UpdateFrequency
        {
            get { return _updateFrequency; }
            set { _updateFrequency = value; }
        }
        public string SlotTemplateListFileName
        {
            get { return _slotTemplateListFileName; }
            set { _slotTemplateListFileName = value; }
        }
        public string RegexListFileName
        {
            get { return _regexListFileName; }
            set { _regexListFileName = value; }
        }
        public string ConfigFileName
        {
            get { return _configFileName; }
            set 
            {
                _configFileName = value;
                _recent.Add(_configFileName);
            }
        }
        public RectangleWrap WindowRect
        {
            get { return _windowRect; }
            set { _windowRect = value; }
        }
        public LoggingLevel LogLevel
        {
            get { return _loggingLevel; }
            set { _loggingLevel = value; }
        }
        public Hotkey PauseHotkey
        {
            get { return _pauseHotkey; }
            set { _pauseHotkey = value; }
        }
        public Hotkey RippleHotkey
        {
            get { return _rippleHotkey; }
            set { _rippleHotkey = value; }
        }
        public RecentFiles Recent
        {
            get { return _recent; }
            set { _recent = value; }
        }

        public AppSettings DeepClone()
        {
            AppSettings appS = new AppSettings();
            appS.MinToTray = this.MinToTray;
            appS.DragDropSwap = this.DragDropSwap;
            appS.SwapOnNewFind = this.SwapOnNewFind;
            appS.HotkeySwap = this.HotkeySwap;
            appS.GroupHotkeys = this.GroupHotkeys;
            appS.RippleForward = this.RippleForward;
            appS.Paused = this.Paused;
            appS.OverlayClosest = this.OverlayClosest;
            appS.OrTitleMatching = this.OrTitleMatching;
            appS.BreadthFirstFreeSlot = this.BreadthFirstFreeSlot;
            appS.RegexListFileName = this.RegexListFileName;
            appS.SlotTemplateListFileName = this.SlotTemplateListFileName;
            appS.UpdateFrequency = this.UpdateFrequency;
            appS.ConfigFileName = this.ConfigFileName;
            appS.WindowRect = this.WindowRect;
            appS.LogLevel = this.LogLevel;
            appS.PauseHotkey = this.PauseHotkey.DeepClone();
            appS.RippleHotkey = this.RippleHotkey.DeepClone();
            appS.Recent = this.Recent.DeepClone();
            return appS;
        }

        public string FileNotFoundMsg()
        {
            return Properties.Resources.appSettingsFileNotFound;
        }
    }
}