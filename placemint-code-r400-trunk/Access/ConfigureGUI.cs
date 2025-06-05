using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;

using PlaceMint.Manager;
using PlaceMint.Manager.PMException;
namespace PlaceMint.Access
{
    using Properties;

    /// <summary>
    /// Used to configure the Window Groups used in the setup
    /// </summary>
    public partial class ConfigureGUI : Form
    {
        private const int TOOL_TIP_TIME = 1500; //ms
        private const int TOOL_TIP_OFF_X = 20; //px
        private const int TOOL_TIP_OFF_Y = 20; //px
        private const string NEW_GROUP_FORMAT = "New Group {0}";
        private const string NEW_SLOT_FORMAT = "Slot {0}";

        private GroupConfiguration _config;
        private GroupConfiguration _configToLoadOnClose;
        private AppSettings _appSettings;
        private List<List<SampleSlotGUI>> _sampleSlots;
        private MainGUI _parent;
        private List<TextBox> _shapeBoxes = new List<TextBox>(2);
        private List<TextBox> _locationBoxes = new List<TextBox>(2);
        private SlotTemplateList _slotTemplateList = new SlotTemplateList();
        private WindowDetailsGUI _detailsGrabber;

        /// <summary>
        /// Build the GUI.
        /// Make deep clones so values of the MainGUI are not changed unintentionally.
        /// </summary>
        /// <param name="config">GroupConfiguration to use to initialize controls.</param>
        /// <param name="appSettings">Used to get save values</param>
        /// <param name="parent">Calling GUI</param>
        /// <param name="firstRun">True if message boxes should cater to a first run</param>
        public ConfigureGUI(GroupConfiguration config, AppSettings appSettings, MainGUI parent, bool firstRun)
        {
            _config = config.DeepClone();
            //slots must be emptied so cloneSlot doesn't assign hwnd to multiple slots
            _config.ClearSlots();
            _configToLoadOnClose = null;
            _appSettings = appSettings.DeepClone();
            if (Logger.Level == LoggingLevel.Trace)
            {
                Logger.Trace("config: {0}\nappSettings: {1}", _config.ToString(), _appSettings.ToString());
            }
            _sampleSlots = new List<List<SampleSlotGUI>>(_config.Groups.Count);
            _parent = parent;
            _parent.Enabled = false;
            this.Owner = _parent;
            InitializeComponent();
            this.Icon = new Icon(Resources.icon, new Size(32, 32));
            _locationBoxes.Add(this.xTextBox);
            _locationBoxes.Add(this.yTextBox);
            _shapeBoxes.Add(this.widthTextBox);
            _shapeBoxes.Add(this.heightTextBox);

            titleRegexSelector.ActiveGroupRegexList =
                new RegexSelector.ActiveGroupRegexListDelegate(getActiveGroupTitleList);
            classRegexSelector.ActiveGroupRegexList =
                new RegexSelector.ActiveGroupRegexListDelegate(getActiveGroupClassList);

            setSlotTemplateList(firstRun);
            setRegexList(firstRun);
            confirmRegexList();

            Logger.Debug("Build sampleSlot list");
            groupListBox.BeginUpdate();
            int groupIndex = 0;
            foreach (WindowGroup group in _config.Groups)
            {
                int slotIndex = 0;
                groupListBox.Items.Add(group.WindowGroupTitle);
                List<SampleSlotGUI> tempList = new List<SampleSlotGUI>(group.Slots.Count);
                foreach (Slot slot in group.Slots)
                {
                    tempList.Add(new SampleSlotGUI(slot.Shape, groupIndex, slotIndex, this));
                    slotIndex++;
                }
                _sampleSlots.Add(tempList); //initialize sampleSlots
                groupIndex++;
            }
            if (_config.Groups.Count == 0)
            {
                groupItemsEnabled(false, true);
                slotItemsEnabled(false, true);
            }
            else
            {
                groupListBox.SelectedIndex = 0;
            }
            groupListBox.EndUpdate();
        }

        /// <summary>
        /// 
        /// </summary>
        public SlotTemplateList SlotTemplateList
        {
            get { return _slotTemplateList; }
        }

        private void exitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            WindowGroup group = getActiveGroup();
            Logger.Debug("Group:{0}", groupListBox.SelectedIndex);

            slotListBox.BeginUpdate();
            slotListBox.Items.Clear();
            for (int i = 0; i < group.SlotsCount; i++)
            {
                slotListBox.Items.Add(String.Format(NEW_SLOT_FORMAT, i + 1));
            }
            slotListBox.EndUpdate();
            titleRegexSelector.checkRegexList(true);
            classRegexSelector.checkRegexList(true);
            if (group.SlotsCount == 0)
            {
                //Values
                slotItemsClear();
                //Disable
                slotItemsEnabled(false, true);
            } else {
                //Values
                slotListBox.SelectedIndex = 0;
                xTextBox.Text = group.Slots[0].Shape.X.ToString();
                yTextBox.Text = group.Slots[0].Shape.Y.ToString();
                widthTextBox.Text = group.Slots[0].Shape.Width.ToString();
                heightTextBox.Text = group.Slots[0].Shape.Height.ToString();
                showCheckBox.Checked = _sampleSlots[groupListBox.SelectedIndex][0].Visible;
                slotHotkey.Hotkey = group.Slots[0].Hotkey;
                //Enable
                slotItemsEnabled(true);
            }
            disableGroupTitleValidating();
            groupTitleTextBox.Text = group.WindowGroupTitle;
            enableGroupTitleValidating();
            disableGroupCheckStateChanged();
            moveCheckBox.Checked = group.Movable;
            sizeCheckBox.Checked = group.Sizable;
            minCheckBox.Checked = group.Minable;
            maxCheckBox.Checked = group.Maxable;
            sendF5CheckBox.Checked = group.SendF5;
            enableGroupCheckStateChanged();
            disableGroupHotkeyChange();
            showHotkey.Hotkey = group.Show;
            restoreHotkey.Hotkey = group.Restore;
            minHotkey.Hotkey = group.Minimize;
            maxHotkey.Hotkey = group.Maximize;
            bottomHotkey.Hotkey = group.Bottom;
            enableGroupHotkeyChange();
            restoreGroupBox.Enabled = group.Minable || group.Maxable;
            minGroupBox.Enabled = group.Minable;
            maxGroupBox.Enabled = group.Maxable;

            downGroupButton.Enabled = (groupListBox.SelectedIndex != _config.Groups.Count - 1
                && groupListBox.Items.Count != 0);
            upGroupButton.Enabled = (groupListBox.SelectedIndex != 0 && groupListBox.Items.Count != 0);
        }

        /// <summary>
        /// Sets which items should be checked in the regex list box
        /// </summary>
        public void checkRegexList()
        {
            if (groupListBox.Items.Count == 0)
            {
                return;
            }
            titleRegexSelector.checkRegexList();
            classRegexSelector.checkRegexList();
        }

        /// <summary>
        /// Confirm each Regex has a valid title. If not add to the custom expression.
        /// </summary>
        public void confirmRegexList()
        {
            List<String> added = new List<string>();

            //Update match incase the regex list has been updated
            for (int i = 0; i < _config.Groups.Count; i++)
            {
                WindowGroup group = _config.Groups[i];
                //if the name isn't found, add it to the regex list
                foreach (TitleRegex regex in group.WinTitleRegexList)
                {
                    if (titleRegexSelector.confirmRegex(regex))
                    {
                        added.Add(regex.Title);
                    }
                }
                foreach (ClassRegex regex in group.WinClassRegexList)
                {
                    if (classRegexSelector.confirmRegex(regex))
                    {
                        added.Add(regex.Title);
                    }
                }
            }
            if (added.Count != 0 && !_appSettings.RegexListFileName.Equals(""))
            {
                TitleList titles = (TitleList)titleRegexSelector.getSortedList();
                ClassList classes = (ClassList)classRegexSelector.getSortedList();
                bool addedOne = (added.Count == 1);
                try
                {
                    RegexList.Save(titles, classes, _appSettings.RegexListFileName);
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < added.Count - 1; i++)
                    {
                        builder.Append(added[i]);
                        builder.Append(", ");
                    }
                    if (!addedOne)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(added[added.Count - 1]);
                    builder.Append((addedOne) ? " was" : " were");
                    string msg = string.Format(Resources.regexTitleNotFound, builder.ToString(), (addedOne) ? "It was" : "They were");
                    setRegexList();
                    Program.msgBoxShow(this, msg);
                }
                catch (EmptyPathException e)
                {
                    Program.msgBoxShow(this, ((addedOne) ? "An expression" : "Some expressions") + " could not be found");
                }
            }
        }

        private void slotListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Slot slot = getActiveSlot();
            Logger.Debug("Group:{0}, Slot:{1}", groupListBox.SelectedIndex, slotListBox.SelectedIndex);

            disableLocationTextValidation();
            xTextBox.Text = slot.Shape.X.ToString();
            yTextBox.Text = slot.Shape.Y.ToString();
            enableLocationTextValidation();
            disableShapeTextValidation();
            widthTextBox.Text = slot.Shape.Width.ToString();
            heightTextBox.Text = slot.Shape.Height.ToString();
            enableShapeTextValidation();

            disableSlotHotkeyChange();
            slotHotkey.Hotkey = slot.Hotkey;
            enableSlotHotkeyChange();

            showCheckBox.Checked = _sampleSlots[groupListBox.SelectedIndex][slotListBox.SelectedIndex].Visible;

            downSlotButton.Enabled = (slotListBox.SelectedIndex !=
                _config.Groups[groupListBox.SelectedIndex].Slots.Count - 1);
            upSlotButton.Enabled = (slotListBox.SelectedIndex != 0);

            maxWinCountTextBox.Text = slot.Size.ToString();
        }

        private void showCheckBox_Click(object sender, EventArgs e)
        {
            Logger.Debug("Show sampelSlot {0}", showCheckBox.Checked);
            _sampleSlots[groupListBox.SelectedIndex][slotListBox.SelectedIndex].Visible = showCheckBox.Checked;
        }

        private void saveItem_Click(object sender, EventArgs e)
        {
            Logger.Debug("Save clicked");
            if (!validatedSave())
            {
                return;
            }
            GroupConfiguration.Save(_config, _appSettings.ConfigFileName);
            //save a copy of the current configuration so changes between now and formClose aren't saved
            _configToLoadOnClose = _config.DeepClone();
            Program.msgBoxShow(this, "Save complete.", Program.MessageLevel.None);
        }

        private void saveAsItem_Click(object sender, EventArgs e)
        {
            Logger.Debug("Save As clicked");
            if (!validatedSave())
            {
                return;
            }
            string newName = null;
            if (FileDialogHelper.Save(_appSettings.ConfigFileName, out newName) == DialogResult.OK)
            {
                GroupConfiguration.Save(_config, newName);
            }
        }

        private bool validatedSave()
        {
            if (!confirmTextBoxes())
            {
                return false; //confimTextBoxes handles notification
            }

            List<String> duplicates = _config.FindDuplicates(false);
            if (duplicates.Count != 0)
            {
                StringBuilder builder = new StringBuilder(Resources.regexReusePre);
                foreach (string title in duplicates)
                {
                    builder.Append(title);
                    builder.Append("\n");
                }
                builder.Append(Resources.regexReuseSavePost);
                Program.msgBoxShow(this, builder.ToString());
                return false;
            }
            if (!_parent.validateHotkeys(_config, _appSettings, false, true))
            {
                Program.msgBoxShow(this, Resources.saveCancelHotkeys);
                return false;
            }
            return true;
        }

        private bool confirmTextBoxes()
        {
            foreach (TextBox tBox in _shapeBoxes)
            {
                if(tBox.Modified)
                {
                    CancelEventArgs e = new CancelEventArgs();
                    shapeTextBox_Validating(tBox, e);
                    if (e.Cancel)
                    {
                        Logger.Debug(Resources.saveCanceledFormat, tBox.Name);
                        tBox.Focus();
                        return false;
                    }
                    shapeTextBox_Validated(tBox, e);
                }
            }
            foreach (TextBox tBox in _locationBoxes)
            {
                if (tBox.Modified)
                {
                    CancelEventArgs e = new CancelEventArgs();
                    locationTextBox_Validating(tBox, e);
                    if (e.Cancel)
                    {
                        Logger.Debug(Resources.saveCanceledFormat, tBox.Name);
                        tBox.Focus();
                        return false;
                    }
                    shapeTextBox_Validated(tBox, e);
                }
            }
            if (maxWinCountTextBox.Modified)
            {
                CancelEventArgs e = new CancelEventArgs();
                maxWinCountTextBox_Validated(maxWinCountTextBox, e);
                if (e.Cancel)
                {
                    Logger.Debug(Resources.saveCanceledFormat, maxWinCountTextBox.Name);
                    maxWinCountTextBox.Focus();
                    return false;
                }
                maxWinCountTextBox_Validated(maxWinCountTextBox, e);
            }
            if (groupTitleTextBox.Modified)
            {
                CancelEventArgs e = new CancelEventArgs();
                groupTitleTextBox_Validating(groupTitleTextBox, e);
                if (e.Cancel)
                {
                    Logger.Debug(Resources.saveCanceledFormat, groupTitleTextBox.Name);
                    groupTitleTextBox.Focus();
                    return false;
                }
            }
            return true;
        }

        private void ConfigureGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_detailsGrabber != null)
            {
                _detailsGrabber.Dispose();
            }
            //if there was a change to configuration, refresh the groups and tree
            if (_configToLoadOnClose != null)
            {
                Logger.Debug("New config:", _parent.Config.ToString());
                //set parent before setting main config to avoid timmer firing before all parents are set
                foreach (WindowGroup group in _configToLoadOnClose.Groups)
                {
                    group.Parent = _configToLoadOnClose;
                }
                _parent.Config = _configToLoadOnClose;
                _parent.buildTreeView();
                _parent.validateHotkeys(_configToLoadOnClose, _appSettings, true, false);
                //We just need to refresh the hotkey lists as it's not a state change
                if (_appSettings.HotkeySwap)
                {
                    _parent.clearAndInitializeSlotHotkeys();
                }
                if (_appSettings.GroupHotkeys)
                {
                    _parent.clearAndInitializeGroupHotkeys();
                }
            }
            _parent.Enabled = true;
            _parent.Focus();
        }

        private void textBoxNumberValidating(TextBox tBox, int min, int max, CancelEventArgs e)
        {
            if (tBox == null)
            {
                throw new IllegalSenderException(String.Format(Resources.illegalSenderFormat,
                    "shapeTextBox_Validating", "a TextBox"));
            }
            if (!RegexValidation.isNum.IsMatch(tBox.Text))
            {
                toolTip.Show(Resources.intValidationFail, tBox, TOOL_TIP_OFF_X, TOOL_TIP_OFF_Y, TOOL_TIP_TIME);
                e.Cancel = true;
            }
            else
            {
                bool tooSmall = false, tooLarge = false;
                int value;
                Logger.Trace("{0}:{1}", tBox.Name, tBox.Text);
                if (Int32.TryParse(tBox.Text, out value))
                {
                    if (value < min)
                    {
                        tooSmall = true;
                    }
                    if (value > max)
                    {
                        tooLarge = true;
                    }
                }
                else
                {
                    if (maxWinCountTextBox.Text.Contains("-"))
                    {
                        tooSmall = true;
                    }
                    else
                    {
                        tooLarge = true;
                    }
                }
                if (tooLarge || tooSmall)
                {
                    string msg;
                    if (tooSmall)
                    {
                        msg = string.Format(Resources.intSizeFailFormat, "smaller", min);
                        tBox.Text = min.ToString();
                    }
                    else
                    {
                        msg = string.Format(Resources.intSizeFailFormat, "larger", max);
                        tBox.Text = max.ToString();
                    }
                    Logger.Debug(msg);
                    toolTip.Show(msg, tBox, TOOL_TIP_OFF_X, TOOL_TIP_OFF_Y, TOOL_TIP_TIME);
                    e.Cancel = true;
                }
            }
        }

        private void maxWinCountTextBox_Validating(object sender, CancelEventArgs e)
        {
            textBoxNumberValidating((TextBox)sender, Slot.MIN_SIZE, Slot.MAX_SIZE, e);
        }

        private void shapeTextBox_Validating(object sender, CancelEventArgs e)
        {
            textBoxNumberValidating((TextBox)sender, 0, Int32.MaxValue, e);
        }

        private void locationTextBox_Validating(object sender, CancelEventArgs e)
        {
            textBoxNumberValidating((TextBox)sender, Int32.MinValue, Int32.MaxValue, e);
        }

        private void maxWinCountTextBox_Validated(object sender, EventArgs e)
        {
            WindowGroup wg = getActiveGroup();
            Slot slot = getActiveSlot();
            wg.Slots[slotListBox.SelectedIndex] = new Slot(slot.Shape.DeepClone(), slot.Hotkey.DeepClone(), Int32.Parse(maxWinCountTextBox.Text));
        }

        private void shapeTextBox_Validated(object sender, EventArgs e)
        {
            TextBox tBox = sender as TextBox;
            if (tBox == null)
            {
                throw new IllegalSenderException(String.Format(Resources.illegalSenderFormat,
                    "shapeTextBox_Validated", "a TextBox"));
            }
            Slot slot = getActiveSlot();
            switch (tBox.Name)
            {
                case "xTextBox":
                    slot.Shape.X = Convert.ToInt32(tBox.Text);
                    break;
                case "yTextBox":
                    slot.Shape.Y = Convert.ToInt32(tBox.Text);
                    break;
                case "widthTextBox":
                    slot.Shape.Width = Convert.ToInt32(tBox.Text);
                    break;
                case "heightTextBox":
                    slot.Shape.Height = Convert.ToInt32(tBox.Text);
                    break;
                default:
                    throw new IllegalFallThroughException(Resources.illegalFallThrough);
            }
            _sampleSlots[groupListBox.SelectedIndex][slotListBox.SelectedIndex].updateRect(slot.Shape);
        }

        #region Event Enable/Disable
        private void disableShapeTextValidation()
        {
            foreach (TextBox tBox in _shapeBoxes)
            {
                tBox.Validating -= shapeTextBox_Validating;
                tBox.Validated -= shapeTextBox_Validated;
            }
        }
        private void enableShapeTextValidation()
        {
            foreach (TextBox tBox in _shapeBoxes)
            {
                tBox.Validating += shapeTextBox_Validating;
                tBox.Validated += shapeTextBox_Validated;
            }
        }
        private void disableLocationTextValidation()
        {
            foreach (TextBox tBox in _locationBoxes)
            {
                tBox.Validating -= shapeTextBox_Validating;
                tBox.Validated -= shapeTextBox_Validated;
            }
        }
        private void enableLocationTextValidation()
        {
            foreach (TextBox tBox in _locationBoxes)
            {
                tBox.Validating += locationTextBox_Validating;
                tBox.Validated += shapeTextBox_Validated;
            }
        }
        private void disableGroupSelectionChange()
        {
            groupListBox.SelectedIndexChanged -= groupListBox_SelectedIndexChanged;
        }
        private void enableGroupSelectionChange()
        {
            groupListBox.SelectedIndexChanged += groupListBox_SelectedIndexChanged;
        }
        private void disableGroupTitleValidating()
        {
            groupTitleTextBox.Validating -= groupTitleTextBox_Validating;
        }
        private void enableGroupTitleValidating()
        {
            groupTitleTextBox.Validating += groupTitleTextBox_Validating;
        }
        private void disableGroupCheckStateChanged()
        {
            sizeCheckBox.CheckStateChanged -= groupCheckBox_CheckStateChanged;
            moveCheckBox.CheckStateChanged -= groupCheckBox_CheckStateChanged;
            maxCheckBox.CheckStateChanged -= groupCheckBox_CheckStateChanged;
            minCheckBox.CheckStateChanged -= groupCheckBox_CheckStateChanged;
            sendF5CheckBox.CheckStateChanged -= groupCheckBox_CheckStateChanged;
        }
        private void enableGroupCheckStateChanged()
        {
            sizeCheckBox.CheckStateChanged += groupCheckBox_CheckStateChanged;
            moveCheckBox.CheckStateChanged += groupCheckBox_CheckStateChanged;
            maxCheckBox.CheckStateChanged += groupCheckBox_CheckStateChanged;
            minCheckBox.CheckStateChanged += groupCheckBox_CheckStateChanged;
            sendF5CheckBox.CheckStateChanged += groupCheckBox_CheckStateChanged;
        }
        private void disableSlotSelectionChange()
        {
            slotListBox.SelectedIndexChanged -= slotListBox_SelectedIndexChanged;
        }
        private void enableSlotSelectionChange()
        {
            slotListBox.SelectedIndexChanged += slotListBox_SelectedIndexChanged;
        }
        private void disableSlotHotkeyChange()
        {
            slotHotkey.HotkeyChange -= slotHotkey_HotkeyChange;
        }
        private void enableSlotHotkeyChange()
        {
            slotHotkey.HotkeyChange += slotHotkey_HotkeyChange;
        }
        private void disableGroupHotkeyChange()
        {
            showHotkey.HotkeyChange -= showHotkey_HotkeyChange;
            restoreHotkey.HotkeyChange -= restoreHotkey_HotkeyChange;
            minHotkey.HotkeyChange -= minHotkey_HotkeyChange;
            maxHotkey.HotkeyChange -= maxHotkey_HotkeyChange;
            bottomHotkey.HotkeyChange -= bottomHotkey_HotkeyChange;
        }
        private void enableGroupHotkeyChange()
        {
            showHotkey.HotkeyChange += showHotkey_HotkeyChange;
            restoreHotkey.HotkeyChange += restoreHotkey_HotkeyChange;
            minHotkey.HotkeyChange += minHotkey_HotkeyChange;
            maxHotkey.HotkeyChange += maxHotkey_HotkeyChange;
            bottomHotkey.HotkeyChange += bottomHotkey_HotkeyChange;
        }
#endregion

        private WindowGroup getActiveGroup()
        {
            return _config.Groups[groupListBox.SelectedIndex];
        }

        private Slot getActiveSlot()
        {
            return _config.Groups[groupListBox.SelectedIndex].Slots[slotListBox.SelectedIndex];
        }

        private void groupUpDownButton_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b == null)
            {
                throw new IllegalSenderException(String.Format(Resources.illegalSenderFormat,
                    "groupUpDownButton_Click", "a Button"));
            }
            int shift = (b.Name.StartsWith("up")) ? -1 : 1;
            //swap groups
            _config.SwapItems(groupListBox.SelectedIndex, groupListBox.SelectedIndex + shift);
            //swap sample Slots
            List<SampleSlotGUI> tempList = _sampleSlots[groupListBox.SelectedIndex];
            _sampleSlots[groupListBox.SelectedIndex] = _sampleSlots[groupListBox.SelectedIndex + shift];
            _sampleSlots[groupListBox.SelectedIndex + shift] = tempList;
            //swap group list items
            groupListBox.BeginUpdate();
            String tempStr1 = groupListBox.Items[groupListBox.SelectedIndex + shift].ToString();
            groupListBox.Items[groupListBox.SelectedIndex + shift] = groupListBox.Items[groupListBox.SelectedIndex];
            disableGroupSelectionChange();
            groupListBox.Items[groupListBox.SelectedIndex] = tempStr1;
            enableGroupSelectionChange();
            groupListBox.SelectedIndex = groupListBox.SelectedIndex + shift;
            groupListBox.EndUpdate();
        }

        private void slotUpDownButton_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b == null)
            {
                throw new IllegalSenderException(String.Format(Resources.illegalSenderFormat,
                    "slotUpDownButton_Click", "a Button"));
            }
            int shift = (b.Name.StartsWith("up")) ? -1 : 1;
            WindowGroup wg = getActiveGroup();
            wg.SwapItems(slotListBox.SelectedIndex, slotListBox.SelectedIndex + shift);
            bool tempBool = _sampleSlots[groupListBox.SelectedIndex][slotListBox.SelectedIndex].Visible;
            _sampleSlots[groupListBox.SelectedIndex][slotListBox.SelectedIndex] =
                _sampleSlots[groupListBox.SelectedIndex][slotListBox.SelectedIndex + shift];
            _sampleSlots[groupListBox.SelectedIndex][slotListBox.SelectedIndex + shift].Visible =
                tempBool;
            slotListBox.BeginUpdate();
            slotListBox.SelectedIndex += shift;
            slotListBox.EndUpdate();
        }

        private void addNewGroupButton_Click(object sender, EventArgs e)
        {
            int newGroupAppend = 1;
            foreach (WindowGroup group in _config.Groups)
            {
                Match m = RegexValidation.isNewGroup.Match(group.WindowGroupTitle);
                if (m.Success)
                {
                    int foundCount = Convert.ToInt32(m.Groups[1].ToString());
                    if (foundCount >= newGroupAppend)
                    {
                        newGroupAppend = foundCount + 1;
                    }
                }
            }
            addGroup(new WindowGroup(new TitleList(), new ClassList(),
                new SlotList(), String.Format(NEW_GROUP_FORMAT, newGroupAppend)));
            groupItemsEnabled(true);
        }

        private void addNewSlotButton_Click(object sender, EventArgs e)
        {
            addSlot(new Slot(new RectangleWrap(), new Hotkey(), Slot.MIN_SIZE));
        }

        private void cloneSelectedGroupButton_Click(object sender, EventArgs e)
        {
            WindowGroup wg = getActiveGroup().DeepClone();
            wg.WindowGroupTitle += "(Clone)";
            for (int i = 0; i < wg.Slots.Count; i++)
            {
                wg.Slots[i].Hotkey = new Hotkey();
            }
            wg.Show = new Hotkey();
            wg.Restore = new Hotkey();
            wg.Minimize = new Hotkey();
            wg.Maximize = new Hotkey();
            wg.Bottom = new Hotkey();
            wg.WinTitleRegexList = new TitleList();
            wg.WinClassRegexList = new ClassList();
            addGroup(wg);
        }
        private void cloneSelectedSlotButton_Click(object sender, EventArgs e)
        {
            //Clear hotkey so a clone does not create an invalid configuration
            Slot slot = getActiveSlot().DeepClone();
            slot.Hotkey = new Hotkey();
            addSlot(slot);
        }
        private void addGroup(WindowGroup wg)
        {
            Logger.Debug("Add new group: {0}", wg.ToString());
            _config.Groups.Add(wg);
            List<SampleSlotGUI> tempList = new List<SampleSlotGUI>(wg.Slots.Count);
            int slotIndex = 0;
            foreach (Slot slot in wg.Slots)
            {
                tempList.Add(new SampleSlotGUI(slot.Shape, groupListBox.SelectedIndex, slotIndex, this));
            }
            _sampleSlots.Add(tempList);
            groupListBox.BeginUpdate();
            groupListBox.Items.Add(wg.WindowGroupTitle);
            groupListBox.SelectedIndex = groupListBox.Items.Count - 1;
            groupListBox.EndUpdate();
        }
        private void addSlot(Slot slot)
        {
            Logger.Debug("Add new slot: {0}", slot.ToString());
            WindowGroup wg = getActiveGroup();
            wg.Slots.Add(slot);
            _sampleSlots[groupListBox.SelectedIndex].Add(new SampleSlotGUI(slot.Shape, groupListBox.SelectedIndex, wg.Slots.Count-1, this));
            slotListBox.BeginUpdate();
            slotListBox.Items.Add(String.Format(NEW_SLOT_FORMAT, slotListBox.Items.Count + 1));
            slotListBox.SelectedIndex = slotListBox.Items.Count - 1;
            slotListBox.EndUpdate();
            if (wg.Slots.Count == 1)
            {
                slotItemsEnabled(true);
            }
        }
        private void slotItemsEnabled(bool state, bool doUpDOwn)
        {
            xTextBox.Enabled = state;
            yTextBox.Enabled = state;
            widthTextBox.Enabled = state;
            heightTextBox.Enabled = state;
            showCheckBox.Enabled = state;
            cloneSelectedSlotButton.Enabled = state;
            removeSelectedSlotButton.Enabled = state;
            slotHotkey.Enabled = state;
            maxWinCountTextBox.Enabled = state;
            if (doUpDOwn)
            {
                upSlotButton.Enabled = state;
                downSlotButton.Enabled = state;
            }
        }
        private void slotItemsEnabled(bool state)
        {
            slotItemsEnabled(state, false);
        }
        private void slotItemsClear()
        {
            xTextBox.Text = "";
            yTextBox.Text = "";
            widthTextBox.Text = "";
            heightTextBox.Text = "";
            showCheckBox.Checked = false;
            slotListBox.Items.Clear();
            slotHotkey.Hotkey = new Hotkey();
        }
        private void groupItemsEnabled(bool state, bool doUpDOwn)
        {
            removeSelectedGroupButton.Enabled = state;
            groupTab.Enabled = state;
            cloneSelectedGroupButton.Enabled = state;
            if (doUpDOwn)
            {
                upGroupButton.Enabled = state;
                downGroupButton.Enabled = state;
            }
        }
        private void groupItemsEnabled(bool state)
        {
            groupItemsEnabled(state, false);
        }

        private void removeSelectedGroupButton_Click(object sender, EventArgs e)
        {
            int selected = groupListBox.SelectedIndex;
            Logger.Debug("Remove group at {0}", selected);
            _config.Groups.RemoveAt(selected);
            //close sample slots as they will not be associated with anything anymore
            foreach (SampleSlotGUI ssg in _sampleSlots[selected])
            {
                ssg.closeNoValidate();
            }
            _sampleSlots.RemoveAt(selected);
            disableGroupSelectionChange();
            groupListBox.BeginUpdate();
            groupListBox.Items.RemoveAt(selected);
            groupListBox.EndUpdate();
            enableGroupSelectionChange();
            if (groupListBox.Items.Count == 0)
            {
                groupItemsEnabled(false, true);
                slotItemsClear();
            }
            else 
            {
                groupListBox.SelectedIndex = (selected == 0) ? 0 : selected - 1;
            }
        }

        private void removeSelectedSlotButton_Click(object sender, EventArgs e)
        {
            int selected = slotListBox.SelectedIndex;
            Logger.Debug("Remove slot at {0} from group {1}", selected, groupListBox.SelectedIndex);
            WindowGroup wg = getActiveGroup();
            wg.Slots.RemoveAt(selected);
            //close sample slot as it will not be associated with anything anymore
            _sampleSlots[groupListBox.SelectedIndex][selected].closeNoValidate();
            _sampleSlots[groupListBox.SelectedIndex].RemoveAt(selected);
            disableSlotSelectionChange();
            slotListBox.BeginUpdate();
            slotListBox.Items.RemoveAt(selected);
            for (int i = selected; i < slotListBox.Items.Count; i++)
            {
                slotListBox.Items[i] = String.Format(NEW_SLOT_FORMAT, i + 1);
            }
            slotListBox.EndUpdate();
            enableSlotSelectionChange();
            if (slotListBox.Items.Count == 0)
            {
                slotItemsEnabled(false, true);
            }
            else if (selected == 0)
            {
                slotListBox.SelectedIndex = selected;
            }
            else
            {
                slotListBox.SelectedIndex = selected - 1;
            }
        }

        private void regexEditItem_Click(object sender, EventArgs e)
        {
            titleRegexSelector.storeRegexCheckedListIndex();
            classRegexSelector.storeRegexCheckedListIndex();
            EditRegex editRegex = new EditRegex(_appSettings, this);
            editRegex.Show();
        }
        

        private void slotTemplatesEdituItem_Click(object sender, EventArgs e)
        {
            EditSlotTemplateList editSlotTemp = new EditSlotTemplateList(_appSettings, this);
            editSlotTemp.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        public void setRegexList()
        {
            setRegexList(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRun"></param>
        public void setRegexList(bool firstRun)
        {
            TitleList titles = null;
            ClassList classes = null;
            try
            {
                RegexList.Load(_appSettings.RegexListFileName, out titles, out classes);
            }
            catch (PlaceMintException e)
            {
                bool showMsg = true;
                string msg = null;
                if (e is PMFileNotFoundException)
                {
                    if (firstRun)
                    {
                        showMsg = false;
                        Program.msgBoxShow(this, Resources.firstRunSlotTemplate, Program.MessageLevel.Warning);
                        RegexList.Save(new RegexList(), _appSettings.RegexListFileName);
                    }
                    else
                    {
                        msg = string.Format(Resources.regexListFileNotFound, _appSettings.RegexListFileName);
                    }
                }
                else if (e is PMPathTooLongException)
                {
                    msg = string.Format(Resources.regexListPathTooLong, _appSettings.RegexListFileName);
                }
                else if (e is WrongXmlFormatException)
                {
                    msg = string.Format(Resources.regexListloadFailureFormat, _appSettings.RegexListFileName);
                }
                else if (e is InvalidXmlValueException)
                {
                    msg = string.Format(Resources.regexListIllegalValueFormat, e.Message);
                }
                else if (e is EmptyPathException)
                {
                    msg = string.Format(Resources.emptyPathFormat, "regex list");
                }
                if (showMsg)
                {
                    if (msg == null)
                    {
                        throw;
                    }
                    else
                    {
                        Program.msgBoxShow(this, msg);
                    }
                }
                if (titles == null)
                {
                    titles = new TitleList();
                }
                if (classes == null)
                {
                    classes = new ClassList();
                }
                e.Log("regex list");
            }
            titleRegexSelector.setRegexList(titles);
            classRegexSelector.setRegexList(classes);
        }
        /// <summary>
        /// 
        /// </summary>
        public void setSlotTemplateList()
        {
            setSlotTemplateList(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRun"></param>
        public void setSlotTemplateList(bool firstRun)
        {
            try
            {
                _slotTemplateList = XmlReadWrite<SlotTemplateList>.Load(_appSettings.SlotTemplateListFileName);
                foreach (List<SampleSlotGUI> sampleSlots in _sampleSlots)
                {
                    foreach (SampleSlotGUI sampleSlot in sampleSlots)
                    {
                        sampleSlot.setSlotTemplateSelect();
                    }
                }
            }
            catch (PlaceMintException e)
            {
                bool showMsg = true;
                string msg = null;
                if (e is PMFileNotFoundException)
                {
                    if (firstRun)
                    {
                        showMsg = false;
                        Program.msgBoxShow(this, Resources.firstRunSlotTemplate, Program.MessageLevel.Warning);
                        _slotTemplateList = new SlotTemplateList();
                        XmlReadWrite<SlotTemplateList>.Save(_slotTemplateList, _appSettings.SlotTemplateListFileName);
                    }
                    else
                    {
                        msg = string.Format(Resources.slotTemplateListNotFoundFormat, _appSettings.SlotTemplateListFileName);
                    }
                }
                else if (e is PMPathTooLongException)
                {
                    msg = string.Format(Resources.slotTemplatePathTooLong, _appSettings.SlotTemplateListFileName);
                }
                else if (e is InvalidXmlValueException)
                {
                    msg = string.Format(Resources.slotTemplateIllegalValueFormat, e.Message);
                }
                else if (e is EmptyPathException)
                {
                    msg = string.Format(Resources.emptyPathFormat, "slot template");
                }
                if (showMsg)
                {
                    if (msg == null)
                    {
                        throw;
                    }
                    else
                    {
                        Program.msgBoxShow(this, msg);
                    }
                }

                if (_slotTemplateList == null)
                {
                    _slotTemplateList = new SlotTemplateList();
                }
                e.Log("slot template list");
            }
        }

        /// <summary>
        /// Update the slot information based on the sample slot
        /// </summary>
        /// <param name="sample"></param>
        public void updateSlotRect(SampleSlotGUI sample)
        {
            Slot slot = _config.Groups[sample.Group].Slots[sample.Slot];
            SampleSlotGUI sampleSlot = _sampleSlots[sample.Group][sample.Slot];
            slot.Shape.X = sampleSlot.Location.X;
            slot.Shape.Y = sampleSlot.Location.Y;
            slot.Shape.Width = sampleSlot.Size.Width;
            slot.Shape.Height = sampleSlot.Size.Height;

            if (sample.Group == groupListBox.SelectedIndex && sample.Slot == slotListBox.SelectedIndex)
            {
                disableLocationTextValidation();
                xTextBox.Text = slot.Shape.X.ToString();
                yTextBox.Text = slot.Shape.Y.ToString();
                enableLocationTextValidation();
                disableShapeTextValidation();
                widthTextBox.Text = slot.Shape.Width.ToString();
                heightTextBox.Text = slot.Shape.Height.ToString();
                enableShapeTextValidation();
            }
        }

        /// <summary>
        /// Uncheck the show box is the "closed" sample slot is the selected one
        /// </summary>
        /// <param name="sample"></param>
        public void uncheck(SampleSlotGUI sample)
        {
            if (sample.Group == groupListBox.SelectedIndex && sample.Slot == slotListBox.SelectedIndex)
            {
                showCheckBox.Checked = false;
            }
        }

        private void groupTitleTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (groupTitleTextBox.Text != (string)groupListBox.SelectedItem)
            {
                if (groupListBox.Items.Contains(groupTitleTextBox.Text))
                {
                    toolTip.Show(Resources.groupNameAlreadyExists, (TextBox)sender, TOOL_TIP_OFF_X, TOOL_TIP_OFF_Y, TOOL_TIP_TIME);
                    e.Cancel = true;
                }
                else
                {
                    WindowGroup wg = getActiveGroup();
                    wg.WindowGroupTitle = groupTitleTextBox.Text;
                    disableGroupSelectionChange();
                    int index = groupListBox.SelectedIndex;
                    groupListBox.Items.RemoveAt(index);
                    groupListBox.Items.Insert(index, groupTitleTextBox.Text);
                    groupListBox.SelectedIndex = index;
                    enableGroupSelectionChange();
                }
            }
        }

        private void groupCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            CheckBox cBox = sender as CheckBox;
            if (cBox == null)
            {
                throw new IllegalSenderException(String.Format(Resources.illegalSenderFormat,
                    "groupCheckBox_CheckStateChanged", "a CheckBox"));
            }
            WindowGroup wg = getActiveGroup();
            switch (cBox.Name)
            {
                case "sizeCheckBox":
                    wg.Sizable = cBox.Checked;
                    break;
                case "moveCheckBox":
                    wg.Movable = cBox.Checked;
                    break;
                case "maxCheckBox":
                    wg.Maxable = cBox.Checked;
                    break;
                case "minCheckBox":
                    wg.Minable = cBox.Checked;
                    break;
                case "sendF5CheckBox":
                    wg.SendF5 = cBox.Checked;
                    break;
                default:
                    throw new IllegalFallThroughException(Resources.illegalFallThrough);
            }
            if (cBox.Name == "maxCheckBox" || cBox.Name == "minCheckBox")
            {
                restoreGroupBox.Enabled = wg.Minable || wg.Maxable;
                minGroupBox.Enabled = wg.Minable;
                maxGroupBox.Enabled = wg.Maxable;
            }
        }

        private void slotHotkey_HotkeyChange()
        {
            Slot slot = getActiveSlot();
            slot.Hotkey = slotHotkey.Hotkey;
        }

        private void Hotkey_Validating(object sender, CancelEventArgs e)
        {
            bool exists = false;
            HotkeyControlCheck hotkeyControl = sender as HotkeyControlCheck;
            if (hotkeyControl == null)
            {
                throw new IllegalSenderException(String.Format(Resources.illegalSenderFormat,
                    "Hotkey_Validating", "a HotkeyControlCheck"));
            }
            for (int i = 0; i < _config.Groups.Count; i++)
            {
                WindowGroup group = _config.Groups[i];
                if (!(i == groupListBox.SelectedIndex && hotkeyControl.Name == "showHotkey") 
                    && group.Show.IsSet && group.Show.Equals(hotkeyControl.Hotkey))
                {
                    exists = true;
                }
                if (!(i == groupListBox.SelectedIndex && hotkeyControl.Name == "restoreHotkey")
                    && group.Restore.IsSet && group.Restore.Equals(hotkeyControl.Hotkey))
                {
                    exists = true;
                }
                if (!(i == groupListBox.SelectedIndex && hotkeyControl.Name == "minHotkey")
                    && group.Minimize.IsSet && group.Minimize.Equals(hotkeyControl.Hotkey))
                {
                    exists = true;
                }
                if (!(i == groupListBox.SelectedIndex && hotkeyControl.Name == "maxHotkey")
                    && group.Maximize.IsSet && group.Maximize.Equals(hotkeyControl.Hotkey))
                {
                    exists = true;
                }
                if (!(i == groupListBox.SelectedIndex && hotkeyControl.Name == "bottomHotkey")
                    && group.Bottom.IsSet && group.Bottom.Equals(hotkeyControl.Hotkey))
                {
                    exists = true;
                }
                for (int j = 0; j < group.Slots.Count; j++)
                {
                    if (!(i == groupListBox.SelectedIndex && j == slotListBox.SelectedIndex && hotkeyControl.Name == "slotHotkey") &&
                        group.Slots[j].Hotkey.IsSet && group.Slots[j].Hotkey.Equals(hotkeyControl.Hotkey))
                    {
                        exists = true;
                        //break out of both loops
                        i = _config.Groups.Count;
                        break;
                    }
                }
            }
            if (exists)
            {
                toolTip.Show(Resources.hotkeyAlreadyExists, hotkeyControl, TOOL_TIP_OFF_X, TOOL_TIP_OFF_Y, TOOL_TIP_TIME);
                e.Cancel = true;
            }
        }

        private void showHotkey_HotkeyChange()
        {
            WindowGroup wg = getActiveGroup();
            wg.Show = showHotkey.Hotkey;
        }

        private void restoreHotkey_HotkeyChange()
        {
            WindowGroup wg = getActiveGroup();
            wg.Restore = restoreHotkey.Hotkey;
        }

        private void minHotkey_HotkeyChange()
        {
            WindowGroup wg = getActiveGroup();
            wg.Minimize = minHotkey.Hotkey;
        }

        private void maxHotkey_HotkeyChange()
        {
            WindowGroup wg = getActiveGroup();
            wg.Maximize = maxHotkey.Hotkey;
        }

        private void bottomHotkey_HotkeyChange()
        {
            WindowGroup wg = getActiveGroup();
            wg.Bottom = bottomHotkey.Hotkey;
        }

        private TitleList getActiveGroupTitleList()
        {
            return _config.Groups[groupListBox.SelectedIndex].WinTitleRegexList;
        }

        private ClassList getActiveGroupClassList()
        {
            return _config.Groups[groupListBox.SelectedIndex].WinClassRegexList;
        }

        /// <summary>
        /// Show the details GUI
        /// </summary>
        public void showDetailsGrabber()
        {
            if (_detailsGrabber == null)
            {
                _detailsGrabber = new WindowDetailsGUI();
            }
            if (_detailsGrabber.Visible)
            {
                _detailsGrabber.BringToFront();
            }
            else
            {
                _detailsGrabber.Show();
            }
        }

        private void detailsGrabberItem_Click(object sender, EventArgs e)
        {
            showDetailsGrabber();
        }
    }
}
