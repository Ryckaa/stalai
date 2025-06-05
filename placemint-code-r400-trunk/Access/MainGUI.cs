using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace PlaceMint.Access
{
    using PlaceMint.Manager;
    using PlaceMint.Manager.PMException;
    using PlaceMint.Access.Properties;

    /// <summary>
    /// Main window of application
    /// </summary>
    public partial class MainGUI : Form, IDrawOverlay
    {
        private enum WindowAction { Show, Maximize, Minimize, Restore, Bottom };

        private GroupConfiguration _config;
        private AppSettings _appSettings;
        private Overlay _closestDrag;
        private bool _forceDebug = false;
        private bool _forceTrace = false;
        private bool _firstRun = false;
        /// <summary>
        /// Hook for slot hotkeys
        /// </summary>
        public KeyboardHook slotHook = new KeyboardHook();
        /// <summary>
        /// Hook for group hotkeys
        /// </summary>
        public KeyboardHook groupHook = new KeyboardHook();
        /// <summary>
        /// Hook for general hotkeys
        /// </summary>
        public KeyboardHook generalHook = new KeyboardHook();

        /// <summary>
        /// Constructor
        /// </summary>
        public MainGUI(string[] args)
        {
            bool forceWorkingDir = true;
            bool forcePause = false;
            //parse command arguments
            foreach (string arg in args)
            {
                switch (arg)
                {
                    case "/nwd":
                        forceWorkingDir = false;
                        break;
                    case "/debug":
                        _forceDebug = true;
                        break;
                    case "/trace":
                        _forceTrace = true;
                        break;
                    case "/pause":
                        forcePause = true;
                        break;
                }
            }
            if (forceWorkingDir)
            {
                Environment.CurrentDirectory = Application.StartupPath;
            }
            try
            {
                _appSettings = XmlReadWrite<AppSettings>.Load(Resources.appSettingsFileName);
            }
            catch (PlaceMintException e)
            {
                if (e is PMFileNotFoundException || e is PMPathTooLongException ||
                    e is WrongXmlFormatException || e is InvalidXmlValueException ||
                    e is EmptyPathException)
                {
                    if (e is WrongXmlFormatException || e is InvalidXmlValueException)
                    {
                        Program.msgBoxShow(this, string.Format(Resources.appSettingsFileFailFormat, e.Message));
                    }
                    else
                    {
                        _firstRun = true;
                    }
                    _appSettings = new AppSettings(); //set to defaults
                    e.Log("applicaiton settings");
                }
                else
                {
                    throw;
                }
            }
            if (forcePause)
            {
                _appSettings.Paused = true;
            }
            setLoggingLevel();

            Logger.Debug("--- Start PlaceMint ---\n{0}", Program.systemInfo());
            
            StringBuilder sb = new StringBuilder();
            foreach (string arg in args)
            {
                sb.Append("--");
                sb.Append(arg);
                sb.Append("\n");
            }
            Logger.Debug("Arguments:{0}\nFirst run:{1}", sb.ToString(), _firstRun);
            Logger.Trace(_appSettings.ToString());

            InitializeComponent();
            this.Text = Program.TITLE_BASE;

            this.Location = new Point(_appSettings.WindowRect.X, _appSettings.WindowRect.Y);
            this.Size = new Size(_appSettings.WindowRect.Width, _appSettings.WindowRect.Height);

            if (!_firstRun)
            {
                loadWorker(_appSettings.ConfigFileName, true);
            }
            setRefreshTimer();
            //register handler for general hotkeys
            generalHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(generalHook_KeyPressed);
            clearAndInitializeGeneralHotkeys();
        }

        void slotHook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            Logger.Debug("slotHook pressed:", e.Hotkey.ToString());
            for (int i = 0; i < _config.Groups.Count; i++)
            {
                WindowGroup group = _config.Groups[i];
                for (int j = 0; j < group.Slots.Count; j++)
                {
                    if (group.Slots[j].Hotkey.Equals(e.Hotkey))
                    {
                        int swapToS = j;
                        IntPtr hwnd = WindowsApi.GetForegroundWindow();
                        int swapWithS, swapWithH;
                        if (!group.FindWindow(hwnd, out swapWithS, out swapWithH))
                        {
                            return;
                        }
                        Logger.Debug("matches group|{0}|slot|{1}|hwnd|{2}|\nswap with|{3}|", i, swapWithS, swapWithH, swapToS);
                        IntPtr targetHWND;
                        if (group.SwapHwnds(swapWithS, swapWithH, swapToS, out targetHWND))
                        {
                            group.Slots[swapWithS].Hwnds.RemoveAll(Slot.MatchEmpty);
                            group.Slots[swapToS].Hwnds.RemoveAll(Slot.MatchEmpty);
                            if (targetHWND != Slot.EMPTY)
                            {
                                try
                                {
                                    Slot.MoveAndSizeWin(targetHWND, group.Slots[swapWithS].Shape, group.Movable, group.Sizable, group.SendF5);
                                }
                                catch (Win32Exception err)
                                {
                                    Logger.Debug(err.Message);
                                }
                            }
                            if (!group.Slots[swapToS].IsEmpty)
                            {
                                try
                                {
                                    Slot.MoveAndSizeWin(hwnd, group.Slots[swapToS].Shape, group.Movable, group.Sizable, group.SendF5);
                                }
                                catch (Win32Exception err)
                                {
                                    Logger.Debug(err.Message);
                                }
                            }
                            buildTreeView();
                        }
                        return;
                    }
                }
            }
        }

        void groupHook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            Logger.Debug("groupHook pressed:", e.Hotkey.ToString());
            for (int i = 0; i < _config.Groups.Count; i++)
            {
                WindowGroup group = _config.Groups[i];
                if (group.Show.Equals(e.Hotkey))
                {
                    Logger.Debug("Show:matches group|{0}|", i);
                    hwndListAction(_config.Groups[i].Hwnds, WindowAction.Show);
                    break;
                }
                if (group.Restore.Equals(e.Hotkey))
                {
                    Logger.Debug("Restore:matches group|{0}|", i);
                    hwndListAction(_config.Groups[i].Hwnds, WindowAction.Restore);
                    break;
                }
                if (group.Minimize.Equals(e.Hotkey))
                {
                    Logger.Debug("Minimize:matches group|{0}|", i);
                    hwndListAction(_config.Groups[i].Hwnds, WindowAction.Minimize);
                    break;
                }
                if (group.Maximize.Equals(e.Hotkey))
                {
                    Logger.Debug("Maximize:matches group|{0}|", i);
                    hwndListAction(_config.Groups[i].Hwnds, WindowAction.Maximize);
                    break;
                }
                if (group.Bottom.Equals(e.Hotkey))
                {
                    Logger.Debug("Bottom:matches group|{0}|", i);
                    hwndListAction(_config.Groups[i].Hwnds, WindowAction.Bottom);
                    break;
                }
            }
        }

        void generalHook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            Logger.Debug("generalHook pressed:", e.Hotkey.ToString());
            if (e.Hotkey.Equals(_appSettings.PauseHotkey))
            {
                togglePaused(sender, new EventArgs());
            }
            else if (!_appSettings.Paused && e.Hotkey.Equals(_appSettings.RippleHotkey))
            {
                rippleItem_Click(sender, new EventArgs());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public GroupConfiguration Config
        {
            get { return _config; }
            set { _config = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public AppSettings Settings
        {
            get { return _appSettings; }
            set { _appSettings = value; }
        }

        private void MainGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logger.Trace("Save settings for close:\n{0}", _appSettings.ToString());
            XmlReadWrite<AppSettings>.Save(_appSettings, Resources.appSettingsFileName);
            Logger.Debug("### Closing PlaceMint ###");
        }

        private void recordMainGuiSize()
        {
            _appSettings.WindowRect.X = this.Location.X;
            _appSettings.WindowRect.Y = this.Location.Y;
            _appSettings.WindowRect.Width = this.Size.Width;
            _appSettings.WindowRect.Height = this.Size.Height;
            Logger.Debug(_appSettings.WindowRect.ToString());
        }

        private void exitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadItem_Click(object sender, EventArgs e)
        {
            string newName = null;
            if (FileDialogHelper.Load(_appSettings.ConfigFileName, out newName) == DialogResult.OK)
            {
                loadWorker(newName);
            }
        }

        private void loadWorker(string file)
        {
            loadWorker(file, false);
        }

        private void loadWorker(string file, bool newOnException)
        {
            if (!_appSettings.Paused)
            {
                refreshTimer.Enabled = false;
            }
            try
            {
                Logger.Debug("Load configuration");
                GroupConfiguration newConfig;
                GroupConfiguration.Load(file, out newConfig);
                foreach (WindowGroup group in newConfig.Groups)
                {
                    group.Parent = newConfig;
                }
                List<String> duplicates = newConfig.FindDuplicates(true);
                if (duplicates.Count > 0)
                {
                    StringBuilder builder = new StringBuilder(Resources.regexReusePre);
                    foreach (string title in duplicates)
                    {
                        builder.Append(title);
                        builder.Append("\n");
                    }
                    builder.Append(Resources.regexReuseLoadPost);
                    GroupConfiguration.Save(newConfig, file);
                    Program.msgBoxShow(this, builder.ToString());
                }
                _config = newConfig;
                _appSettings.ConfigFileName = file;
                _appSettings.Recent.Add(file);
                configureForPausedState();
                if (!_appSettings.Paused)
                {
                    _config.RefreshGroups(_appSettings.DeepClone(), this);
                }
            }
            catch (PlaceMintException ex)
            {
                if (ex is WrongXmlFormatException)
                {
                    Program.msgBoxShow(this, Resources.windowGroupLoadFormatFailure);
                    Logger.Debug("Wrong file type");
                }
                else if (ex is InvalidXmlValueException)
                {
                    Program.msgBoxShow(this, string.Format(Resources.configurationIllegalValueFormat, ex.Message));
                }
                else if (ex is InvalidRegexException)
                {
                    Program.msgBoxShow(this, string.Format(Resources.invalidRegexLoadedFormat, ex.Message));
                }
                else if (ex is PMFileNotFoundException || ex is PMPathTooLongException)
                {
                    Program.msgBoxShow(this, (_firstRun) ? Resources.firstRun : ex.Message,
                        (_firstRun) ? Program.MessageLevel.None : Program.MessageLevel.Error);
                }
                else if (ex is EmptyPathException)
                {
                    Program.msgBoxShow(this, string.Format(Resources.emptyPathFormat, "configuration"));
                }
                else
                {
                    throw;
                }
                if (newOnException)
                {
                    _config = new GroupConfiguration();
                }
            }
            if (!_appSettings.Paused)
            {
                refreshTimer.Enabled = true;
            }
            buildRecentMenu();
            buildTreeView();
        }

        /// <summary>
        /// Refrresh the recent menu to reflect the recently opened files.
        /// </summary>
        private void buildRecentMenu()
        {
            RecentFiles recent = _appSettings.Recent.DeepClone();
            this.taskBarContextMenu.SuspendLayout();
            recentItem.DropDownItems.Clear();
            tbRecentItem.DropDownItems.Clear();
            recentItem.Enabled = (recent.Count != 0);
            tbRecentItem.Enabled = (recent.Count != 0);
            EventHandler click = new EventHandler(recent_Click);
            for (int i = 0; i < recent.Count; i++)
            {
                recentItem.DropDownItems.Add(new RecentStripButton(recent.Files[i], click));
                tbRecentItem.DropDownItems.Add(new RecentStripButton(recent.Files[i], click));
            }
            this.taskBarContextMenu.ResumeLayout();
        }

        void recent_Click(object sender, EventArgs e)
        {
            RecentStripButton recent = (RecentStripButton)sender;
            if (recent == null)
            {
                throw new IllegalSenderException(String.Format(Resources.illegalSenderFormat,
                    "recent_Click", "a RecentStripButton"));
            }
            loadWorker(recent.File);
        }

        /// <summary>
        /// Refresh the main tree view because the configuration has changed.
        /// </summary>
        public void buildTreeView()
        {
            Logger.Trace("buildTreeView");
            List<List<bool>> expandedStacks = new List<List<bool>>(treeView.Nodes.Count);
            List<bool> expandedGroups = new List<bool>(treeView.Nodes.Count);
            for(int i = 0; i < treeView.Nodes.Count; i++)
            {
                expandedGroups.Add(treeView.Nodes[i].IsExpanded);
                int childCount = treeView.Nodes[i].GetNodeCount(false);
                expandedStacks.Add(new List<bool>(childCount));
                for(int j = 0; j < childCount; j++)
                {
                    expandedStacks[i].Add(treeView.Nodes[i].Nodes[j].IsExpanded);
                }
            }
            treeView.BeginUpdate();
            treeView.Nodes.Clear();
            foreach (WindowGroup group in _config.Groups)
            {
                TreeNode node = treeView.Nodes.Add(group.WindowGroupTitle);
                foreach (Slot slot in group.Slots)
                {
                    TreeNode slotNode;
                    if (slot.IsStack)
                    {
                        slotNode = node.Nodes.Add(String.Format("Stack ({0}/{1})", slot.Count, slot.Size));
                        TreeNode stackNode;
                        foreach (IntPtr hwnd in slot.Hwnds)
                        {
                            stackNode = slotNode.Nodes.Add(group.Windows[hwnd].TheTitle);
                            stackNode.Tag = hwnd;
                        }
                        if (node.Index < expandedStacks.Count && slotNode.Index < expandedStacks[node.Index].Count
                            && expandedStacks[node.Index][slotNode.Index])
                        {
                            slotNode.Expand();
                        }
                    }
                    else if (slot.IsEmpty)
                    {
                        slotNode = node.Nodes.Add("");
                        slotNode.Tag = Slot.EMPTY;
                    }
                    else
                    {
                        slotNode = node.Nodes.Add(group.Windows[slot.Hwnds[0]].TheTitle);
                        slotNode.Tag = slot.Hwnds[0];
                    }
                }
                if (node.Index < expandedGroups.Count && expandedGroups[node.Index])
                {
                    node.Expand();
                }
            }
            treeView.EndUpdate();
        }

        private void treeView_DoubleClick(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                MouseEventArgs ee = e as MouseEventArgs;
                if (ee.Button == MouseButtons.Left)
                {
                    TreeNode node = treeView.GetNodeAt(ee.Location);
                    if (node == null)
                    {
                        Logger.Debug("Attempt to show (double click) a node but it was null");
                    }
                    else if (node.Nodes.Count == 0)
                    {
                        treeAction(node, WindowAction.Show);
                    }
                }
            }
        }

        private void treeView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                treeAction(WindowAction.Show);
            }
        }

        /// <summary>
        /// Default node to selected node
        /// </summary>
        /// <param name="action"></param>
        private void treeAction(WindowAction action)
        {
            treeAction(treeView.SelectedNode, action);
        }

        /// <summary>
        /// perform specified action based on supplied node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        private void treeAction(TreeNode node, WindowAction action)
        {
            if (node == null)
            {
                Logger.Debug("Attempt to {0} (menu) a node but it was null", action.ToString());
                return;
            }
            List<IntPtr> HWNDs = new List<IntPtr>();
            switch(node.Level)
            {
                case 0:
                    HWNDs = _config.Groups[node.Index].Hwnds;
                    break;
                case 1:
                    if (action == WindowAction.Show)
                    {
                        if (_config.Groups[node.Parent.Index].Slots[node.Index].IsEmpty)
                        {
                            return; //no window to act on
                        }
                        //only one will stay shown, so just show first
                        HWNDs.Add(_config.Groups[node.Parent.Index].Slots[node.Index].Hwnds[0]);
                    }
                    else
                    {
                        HWNDs = _config.Groups[node.Parent.Index].Slots[node.Index].Hwnds;
                    }
                    break;
                case 2:
                    HWNDs.Add(_config.Groups[node.Parent.Parent.Index].Slots[node.Parent.Index].Hwnds[node.Index]);
                    break;
            }
            hwndListAction(HWNDs, action);
        }

        private void hwndListAction(List<IntPtr> HWNDs, WindowAction action)
        {
            foreach (IntPtr hwnd in HWNDs)
            {
                try
                {
                    switch (action)
                    {
                        case WindowAction.Show:
                            if (Slot.IsMinimized(hwnd))
                            {
                                Slot.RestoreWin(hwnd);
                            }
                            Slot.ShowWin(hwnd);
                            break;
                        case WindowAction.Maximize:
                            Slot.MaximizeWin(hwnd);
                            break;
                        case WindowAction.Minimize:
                            Slot.MinimizeWin(hwnd);
                            break;
                        case WindowAction.Restore:
                            Slot.RestoreWin(hwnd);
                            break;
                        case WindowAction.Bottom:
                            Slot.SendToBottom(hwnd);
                            break;
                    }
                    //Windows needs some delay between actions
                    Thread.Sleep(5);
                }
                catch (Win32Exception e)
                {
                    Logger.Debug(e.Message);
                }
            }
        }

        /// <summary>
        /// Set visibility of nodeContextMenu Items and show menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                TreeNode node = treeView.GetNodeAt(p);
                if (node != null)
                {
                    treeView.SelectedNode = node;
                    bool showMenu = false;
                    WindowGroup group;
                    switch(node.Level)
                    {
                        case 0:
                            group = _config.Groups[node.Index];
                            foreach (Slot slot in group.Slots)
                            {
                                if (!slot.IsEmpty)
                                {
                                    showMenu = true;
                                    break;
                                }
                            }
                            if (showMenu)
                            {
                                nodeContextMenu.Items["minimizeItem"].Visible = group.Minable;
                                nodeContextMenu.Items["maximizeItem"].Visible = group.Maxable;
                                nodeContextMenu.Items["restoreItem"].Visible = group.Minable || group.Maxable;
                            }
                            break;
                    case 1:
                        Slot s = _config.Groups[node.Parent.Index].Slots[node.Index];
                        if (!s.IsEmpty)
                        {
                            showMenu = true;
                            WindowGroup wg = _config.Groups[node.Parent.Index];
                            nodeContextMenu.Items["minimizeItem"].Visible = wg.Minable && !Slot.IsMinimized(s.Hwnds);
                            nodeContextMenu.Items["maximizeItem"].Visible = wg.Maxable && !Slot.IsMaximized(s.Hwnds);
                            nodeContextMenu.Items["restoreItem"].Visible =
                                wg.Minable && Slot.IsMinimized(s.Hwnds) || wg.Maxable && Slot.IsMaximized(s.Hwnds);
                        }
                        break;
                    case 2:
                        showMenu = true;
                        group = _config.Groups[node.Parent.Parent.Index];
                        IntPtr hwnd = group.Slots[node.Parent.Index].Hwnds[node.Index];
                        nodeContextMenu.Items["minimizeItem"].Visible = group.Minable && !Slot.IsMinimized(hwnd);
                        nodeContextMenu.Items["maximizeItem"].Visible = group.Maxable && !Slot.IsMaximized(hwnd);
                        nodeContextMenu.Items["restoreItem"].Visible =
                            group.Minable && Slot.IsMinimized(hwnd) || group.Maxable && Slot.IsMaximized(hwnd);
                        break;
                    }
                    if (showMenu)
                    {
                        nodeContextMenu.Show(treeView, p);
                    }
                }
            }
        }

        private void showItem_Click(object sender, EventArgs e)
        {
            treeAction(WindowAction.Show);
        }

        private void restoreItem_Click(object sender, EventArgs e)
        {
            treeAction(WindowAction.Restore);
        }

        private void maximizeItem_Click(object sender, EventArgs e)
        {
            treeAction(WindowAction.Maximize);
        }

        private void minimizeItem_Click(object sender, EventArgs e)
        {
            treeAction(WindowAction.Minimize);
        }

        private void toBottomItem_Click(object sender, EventArgs e)
        {
            treeAction(WindowAction.Bottom);
        }

        private void aboutItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.Show();
        }

        private void MainGUI_Resize(object sender, EventArgs e)
        {
            if ((WindowState == FormWindowState.Minimized) && _appSettings.MinToTray)
            {
                this.Hide();
                this.taskIcon.Visible = true;
            }
        }

        private void helpItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Resources.helpUrl);
        }

        private void taskIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.taskIcon.Visible = false;
        }

        private void configureItem_Click(object sender, EventArgs e)
        {
            ConfigureGUI config = new ConfigureGUI(_config, _appSettings, this, _firstRun);
            config.Show();
        }

        private void optionsItem_Click(object sender, EventArgs e)
        {
            OptionsGUI options = new OptionsGUI(_appSettings, this);
            options.Show();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (_config.RefreshGroups(_appSettings.DeepClone(), this))
            {
                buildTreeView();
            }
        }

        /// <summary>
        /// Set the refreshTimers interval to update frequency.
        /// </summary>
        public void setRefreshTimer()
        {
            this.refreshTimer.Interval = _appSettings.UpdateFrequency;
        }

        /// <summary>
        /// Used to set logging level and let command line arguments override appSettings 
        /// </summary>
        public void setLoggingLevel()
        {
            if (_forceTrace)
            {
                Logger.Level = LoggingLevel.Trace;
            }
            else if (_forceDebug)
            {
                Logger.Level = LoggingLevel.Debug;
            }
            else
            {
                Logger.Level = _appSettings.LogLevel;
            }
        }

        /// <summary>
        /// Unregister existing hotkeys and register all current hotkeys
        /// </summary>
        public void clearAndInitializeSlotHotkeys()
        {
            slotHook.ClearAllHotkeys();
            foreach (WindowGroup group in _config.Groups)
            {
                foreach (Slot slot in group.Slots)
                {
                    if (slot.Hotkey.IsSet)
                    {
                        try
                        {
                            slotHook.RegisterHotkey(slot.Hotkey);
                        }
                        catch (HotkeyAlreadyExistsException ex)
                        {
                            hotkeyExceptionHandle(ex);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Unregister existing hotkeys and register all current hotkeys
        /// </summary>
        public void clearAndInitializeGeneralHotkeys()
        {
            generalHook.ClearAllHotkeys();
            try
            {
                generalHook.RegisterHotkey(_appSettings.PauseHotkey);
                if (!_appSettings.RippleForward)
                {
                    generalHook.RegisterHotkey(_appSettings.RippleHotkey);
                }
            }
            catch (HotkeyAlreadyExistsException ex)
            {
                hotkeyExceptionHandle(ex);
            }
        }

        /// <summary>
        /// Unregister existing hotkeys and register all current hotkeys
        /// </summary>
        public void clearAndInitializeGroupHotkeys()
        {
            groupHook.ClearAllHotkeys();
            //wrap each register call so that one fail doesn't block others.
            foreach (WindowGroup group in _config.Groups)
            {
                if (group.Show.IsSet)
                {
                    try
                    {
                        groupHook.RegisterHotkey(group.Show);
                    }
                    catch (HotkeyAlreadyExistsException ex)
                    {
                        hotkeyExceptionHandle(ex);
                    }
                }
                if ((group.Minable || group.Maxable) && group.Restore.IsSet)
                {
                    try
                    {
                        groupHook.RegisterHotkey(group.Restore);
                    }
                    catch (HotkeyAlreadyExistsException ex)
                    {
                        hotkeyExceptionHandle(ex);
                    }
                }
                if (group.Minable && group.Minimize.IsSet)
                {
                    try
                    {
                        groupHook.RegisterHotkey(group.Minimize);
                    }
                    catch (HotkeyAlreadyExistsException ex)
                    {
                        hotkeyExceptionHandle(ex);
                    }
                }
                if (group.Maxable && group.Maximize.IsSet)
                {
                    try
                    {
                        groupHook.RegisterHotkey(group.Maximize);
                    }
                    catch (HotkeyAlreadyExistsException ex)
                    {
                        hotkeyExceptionHandle(ex);
                    }
                }
                if (group.Bottom.IsSet)
                {
                    try
                    {
                        groupHook.RegisterHotkey(group.Bottom);
                    }
                    catch (HotkeyAlreadyExistsException ex)
                    {
                        hotkeyExceptionHandle(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Checks duplicate hotkeys. If duplicates are found clear them and alert user.
        /// </summary>
        /// <param name="config">GroupConfiguration to check</param>
        /// <param name="settings">AppSettings to check</param>
        /// <param name="clear">true if duplicate hotkeys should be cleared</param>
        /// <param name="silent">true if warning message box should be surpressed</param>
        /// <returns>false if any hotkeys were duplicates</returns>
        public bool validateHotkeys(GroupConfiguration config, AppSettings settings, bool clear, bool silent)
        {
            List<Hotkey> hotkeys = new List<Hotkey>();
            int duplicateHotkeys = 0;
            //check settings hotkeys first as they should take presidence
            duplicateHotkeys += checkHotkey(settings.PauseHotkey, hotkeys, clear);
            duplicateHotkeys += checkHotkey(settings.RippleHotkey, hotkeys, clear);
            //now check configuration
            foreach (WindowGroup group in config.Groups)
            {
                duplicateHotkeys += checkHotkey(group.Show, hotkeys, clear);
                duplicateHotkeys += checkHotkey(group.Restore, hotkeys, clear);
                duplicateHotkeys += checkHotkey(group.Minimize, hotkeys, clear);
                duplicateHotkeys += checkHotkey(group.Maximize, hotkeys, clear);
                duplicateHotkeys += checkHotkey(group.Bottom, hotkeys, clear);
                foreach (Slot slot in group.Slots)
                {
                    duplicateHotkeys += checkHotkey(slot.Hotkey, hotkeys, clear);
                }
            }
            if (duplicateHotkeys != 0)
            {
                if (!silent)
                {
                    String msg = Resources.duplicateHotkeys;
                    if(clear)
                    {
                        msg += Resources.duplicateHotkeysClear;
                    }
                    Program.msgBoxShow(this, msg, Resources.duplicateHotkeysDialogTitle);
                }
                Logger.Trace("There were {0} duplicate hotkeys.", duplicateHotkeys);
                return false;
            }
            return true;
        }

        private int checkHotkey(Hotkey hotkey, List<Hotkey> hotkeys, bool clear)
        {
            if (hotkey.IsSet)
            {
                if (hotkeys.Contains(hotkey))
                {
                    if (clear)
                    {
                        hotkey.Key = Keys.None;
                        hotkey.ModKeys = ModifyingKeys.None;
                    }
                    return 1;
                }
                hotkeys.Add(hotkey);
            }
            return 0;
        }

        private void togglePaused(object sender, EventArgs e)
        {
            _appSettings.Paused = !_appSettings.Paused;
            configureForPausedState();
        }

        /// <summary>
        /// Make changes to slots, tree, icon and hotkeys based on paused state
        /// </summary>
        public void configureForPausedState()
        {
            treeView.Enabled = !_appSettings.Paused;
            pauseItem.Checked = _appSettings.Paused;
            tbPauseItem.Checked = _appSettings.Paused;
            if (_appSettings.Paused)
            {
                _config.ClearSlots();
                buildTreeView();
                this.Text = Program.TITLE_BASE + Resources.paused;
                this.Icon = new Icon(Resources.icon_paused, new Size(16, 16));
                this.taskIcon.Icon = new Icon(Resources.icon_paused, new Size(16, 16));
                //clear hotkeys when paused
                groupHook.ClearAllHotkeys();
                groupHook.KeyPressed -= groupHook_KeyPressed;
                slotHook.ClearAllHotkeys();
                slotHook.KeyPressed -= slotHook_KeyPressed;
            }
            else
            {
                this.Text = Program.TITLE_BASE;
                this.Icon = new Icon(Resources.icon, new Size(16, 16));
                this.taskIcon.Icon = new Icon(Resources.icon, new Size(16, 16));
                validateHotkeys(_config, _appSettings, true, false);
                //register hotkeys
                if (_appSettings.GroupHotkeys)
                {
                    clearAndInitializeGroupHotkeys();
                    groupHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(groupHook_KeyPressed);
                }
                if (_appSettings.HotkeySwap)
                {
                    clearAndInitializeSlotHotkeys();
                    slotHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(slotHook_KeyPressed);
                }
            }
            refreshTimer.Enabled = !_appSettings.Paused;
        }

        private void hotkeyExceptionHandle(HotkeyAlreadyExistsException ex)
        {
            string msg = ex.Message + "\nIt is already somewhere on the system. This hotkey will not work until you edit it.";
            Logger.Debug(msg);
            Program.msgBoxShow(this, msg);
        }

        private void MainGUI_ResizeEnd(object sender, EventArgs e)
        {
            recordMainGuiSize();
        }

        private void rippleItem_Click(object sender, EventArgs e)
        {
            AppSettings temp = _appSettings.DeepClone();
            temp.RippleForward = true;
            if (_config.RefreshGroups(temp, this))
            {
                buildTreeView();
            }
        }

        /// <inheritdoc />
        public void DrawOverlay(RectangleWrap shape)
        {
            if (_closestDrag != null)
            {
                if (_closestDrag.shapeEquals(shape) && _closestDrag.Visible)
                {
                    return;
                }
                else
                {
                    _closestDrag.Hide();
                }
            }
            _closestDrag = new Overlay(shape);
            _closestDrag.Show();
        }
    }
}