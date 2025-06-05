namespace PlaceMint.Access
{
    partial class ConfigureGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            PlaceMint.Manager.Hotkey hotkey1 = new PlaceMint.Manager.Hotkey();
            PlaceMint.Manager.Hotkey hotkey2 = new PlaceMint.Manager.Hotkey();
            PlaceMint.Manager.Hotkey hotkey3 = new PlaceMint.Manager.Hotkey();
            PlaceMint.Manager.Hotkey hotkey4 = new PlaceMint.Manager.Hotkey();
            PlaceMint.Manager.Hotkey hotkey5 = new PlaceMint.Manager.Hotkey();
            PlaceMint.Manager.Hotkey hotkey6 = new PlaceMint.Manager.Hotkey();
            this.removeSelectedGroupButton = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.saveItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regexEditItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slotTemplatesEdituItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupTitleLabel = new System.Windows.Forms.Label();
            this.groupTitleTextBox = new System.Windows.Forms.TextBox();
            this.groupTab = new System.Windows.Forms.TabControl();
            this.settingsPage = new System.Windows.Forms.TabPage();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.maxCheckBox = new System.Windows.Forms.CheckBox();
            this.sendF5CheckBox = new System.Windows.Forms.CheckBox();
            this.minCheckBox = new System.Windows.Forms.CheckBox();
            this.sizeCheckBox = new System.Windows.Forms.CheckBox();
            this.moveCheckBox = new System.Windows.Forms.CheckBox();
            this.titlePage = new System.Windows.Forms.TabPage();
            this.titleRegexSelector = new PlaceMint.Access.RegexSelector();
            this.classPage = new System.Windows.Forms.TabPage();
            this.classRegexSelector = new PlaceMint.Access.RegexSelector();
            this.slotsPage = new System.Windows.Forms.TabPage();
            this.maxWinCountTextBox = new System.Windows.Forms.TextBox();
            this.maxWinCountLabel = new System.Windows.Forms.Label();
            this.hotkeyGroupBox = new System.Windows.Forms.GroupBox();
            this.slotHotkey = new PlaceMint.Access.HotkeyControlCheck();
            this.slotListBox = new System.Windows.Forms.ListBox();
            this.showCheckBox = new System.Windows.Forms.CheckBox();
            this.downSlotButton = new System.Windows.Forms.Button();
            this.upSlotButton = new System.Windows.Forms.Button();
            this.locationGroupBox = new System.Windows.Forms.GroupBox();
            this.locationCommaLabel = new System.Windows.Forms.Label();
            this.yTextBox = new System.Windows.Forms.TextBox();
            this.xTextBox = new System.Windows.Forms.TextBox();
            this.sizeGroupBox = new System.Windows.Forms.GroupBox();
            this.sizeByLable = new System.Windows.Forms.Label();
            this.heightTextBox = new System.Windows.Forms.TextBox();
            this.widthTextBox = new System.Windows.Forms.TextBox();
            this.addNewSlotButton = new System.Windows.Forms.Button();
            this.cloneSelectedSlotButton = new System.Windows.Forms.Button();
            this.removeSelectedSlotButton = new System.Windows.Forms.Button();
            this.hotkeyPage = new System.Windows.Forms.TabPage();
            this.bottomGroupBox = new System.Windows.Forms.GroupBox();
            this.bottomHotkey = new PlaceMint.Access.HotkeyControlCheck();
            this.maxGroupBox = new System.Windows.Forms.GroupBox();
            this.maxHotkey = new PlaceMint.Access.HotkeyControlCheck();
            this.minGroupBox = new System.Windows.Forms.GroupBox();
            this.minHotkey = new PlaceMint.Access.HotkeyControlCheck();
            this.restoreGroupBox = new System.Windows.Forms.GroupBox();
            this.restoreHotkey = new PlaceMint.Access.HotkeyControlCheck();
            this.showGroupBox = new System.Windows.Forms.GroupBox();
            this.showHotkey = new PlaceMint.Access.HotkeyControlCheck();
            this.groupHotkeyLabel = new System.Windows.Forms.Label();
            this.addNewGroupButton = new System.Windows.Forms.Button();
            this.cloneSelectedGroupButton = new System.Windows.Forms.Button();
            this.groupListBox = new System.Windows.Forms.ListBox();
            this.upGroupButton = new System.Windows.Forms.Button();
            this.downGroupButton = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.detailsGrabberItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.groupTab.SuspendLayout();
            this.settingsPage.SuspendLayout();
            this.optionsGroupBox.SuspendLayout();
            this.titlePage.SuspendLayout();
            this.classPage.SuspendLayout();
            this.slotsPage.SuspendLayout();
            this.hotkeyGroupBox.SuspendLayout();
            this.locationGroupBox.SuspendLayout();
            this.sizeGroupBox.SuspendLayout();
            this.hotkeyPage.SuspendLayout();
            this.bottomGroupBox.SuspendLayout();
            this.maxGroupBox.SuspendLayout();
            this.minGroupBox.SuspendLayout();
            this.restoreGroupBox.SuspendLayout();
            this.showGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // removeSelectedGroupButton
            // 
            this.removeSelectedGroupButton.Location = new System.Drawing.Point(12, 391);
            this.removeSelectedGroupButton.Name = "removeSelectedGroupButton";
            this.removeSelectedGroupButton.Size = new System.Drawing.Size(150, 22);
            this.removeSelectedGroupButton.TabIndex = 5;
            this.removeSelectedGroupButton.Text = "Remove Selected Group";
            this.removeSelectedGroupButton.UseVisualStyleBackColor = true;
            this.removeSelectedGroupButton.Click += new System.EventHandler(this.removeSelectedGroupButton_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveItem,
            this.saveAsItem,
            this.regexEditItem,
            this.slotTemplatesEdituItem,
            this.detailsGrabberItem,
            this.exitItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(480, 24);
            this.menuStrip.TabIndex = 7;
            this.menuStrip.Text = "menuStrip1";
            // 
            // saveItem
            // 
            this.saveItem.Name = "saveItem";
            this.saveItem.Size = new System.Drawing.Size(43, 20);
            this.saveItem.Text = "&Save";
            this.saveItem.ToolTipText = "Save to the currently loaded configuration file.";
            this.saveItem.Click += new System.EventHandler(this.saveItem_Click);
            // 
            // saveAsItem
            // 
            this.saveAsItem.Name = "saveAsItem";
            this.saveAsItem.Size = new System.Drawing.Size(58, 20);
            this.saveAsItem.Text = "Save &As";
            this.saveAsItem.ToolTipText = "Save configuration to a new file.";
            this.saveAsItem.Click += new System.EventHandler(this.saveAsItem_Click);
            // 
            // regexEditItem
            // 
            this.regexEditItem.Name = "regexEditItem";
            this.regexEditItem.Size = new System.Drawing.Size(90, 20);
            this.regexEditItem.Text = "Edit &Regex List";
            this.regexEditItem.Click += new System.EventHandler(this.regexEditItem_Click);
            // 
            // slotTemplatesEdituItem
            // 
            this.slotTemplatesEdituItem.Name = "slotTemplatesEdituItem";
            this.slotTemplatesEdituItem.Size = new System.Drawing.Size(110, 20);
            this.slotTemplatesEdituItem.Text = "Edit Slot Templates";
            this.slotTemplatesEdituItem.Click += new System.EventHandler(this.slotTemplatesEdituItem_Click);
            // 
            // exitItem
            // 
            this.exitItem.Name = "exitItem";
            this.exitItem.Size = new System.Drawing.Size(37, 20);
            this.exitItem.Text = "E&xit";
            this.exitItem.ToolTipText = "Any changes not saved will be discarded.";
            this.exitItem.Click += new System.EventHandler(this.exitItem_Click);
            // 
            // groupTitleLabel
            // 
            this.groupTitleLabel.AutoSize = true;
            this.groupTitleLabel.Location = new System.Drawing.Point(6, 3);
            this.groupTitleLabel.Name = "groupTitleLabel";
            this.groupTitleLabel.Size = new System.Drawing.Size(59, 13);
            this.groupTitleLabel.TabIndex = 0;
            this.groupTitleLabel.Text = "Group Title";
            // 
            // groupTitleTextBox
            // 
            this.groupTitleTextBox.Location = new System.Drawing.Point(6, 24);
            this.groupTitleTextBox.Name = "groupTitleTextBox";
            this.groupTitleTextBox.Size = new System.Drawing.Size(257, 20);
            this.groupTitleTextBox.TabIndex = 7;
            this.groupTitleTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.groupTitleTextBox_Validating);
            // 
            // groupTab
            // 
            this.groupTab.Controls.Add(this.settingsPage);
            this.groupTab.Controls.Add(this.titlePage);
            this.groupTab.Controls.Add(this.classPage);
            this.groupTab.Controls.Add(this.slotsPage);
            this.groupTab.Controls.Add(this.hotkeyPage);
            this.groupTab.Location = new System.Drawing.Point(168, 27);
            this.groupTab.Name = "groupTab";
            this.groupTab.SelectedIndex = 0;
            this.groupTab.Size = new System.Drawing.Size(300, 399);
            this.groupTab.TabIndex = 6;
            // 
            // settingsPage
            // 
            this.settingsPage.Controls.Add(this.optionsGroupBox);
            this.settingsPage.Controls.Add(this.groupTitleLabel);
            this.settingsPage.Controls.Add(this.groupTitleTextBox);
            this.settingsPage.Location = new System.Drawing.Point(4, 22);
            this.settingsPage.Name = "settingsPage";
            this.settingsPage.Padding = new System.Windows.Forms.Padding(3);
            this.settingsPage.Size = new System.Drawing.Size(292, 373);
            this.settingsPage.TabIndex = 0;
            this.settingsPage.Text = "Settings";
            this.settingsPage.UseVisualStyleBackColor = true;
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.Controls.Add(this.maxCheckBox);
            this.optionsGroupBox.Controls.Add(this.sendF5CheckBox);
            this.optionsGroupBox.Controls.Add(this.minCheckBox);
            this.optionsGroupBox.Controls.Add(this.sizeCheckBox);
            this.optionsGroupBox.Controls.Add(this.moveCheckBox);
            this.optionsGroupBox.Location = new System.Drawing.Point(6, 50);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(257, 88);
            this.optionsGroupBox.TabIndex = 14;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Options";
            // 
            // maxCheckBox
            // 
            this.maxCheckBox.AutoSize = true;
            this.maxCheckBox.Location = new System.Drawing.Point(123, 19);
            this.maxCheckBox.Name = "maxCheckBox";
            this.maxCheckBox.Size = new System.Drawing.Size(69, 17);
            this.maxCheckBox.TabIndex = 13;
            this.maxCheckBox.Text = "Maximize";
            this.maxCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.maxCheckBox.UseVisualStyleBackColor = true;
            this.maxCheckBox.CheckStateChanged += new System.EventHandler(this.groupCheckBox_CheckStateChanged);
            // 
            // sendF5CheckBox
            // 
            this.sendF5CheckBox.AutoSize = true;
            this.sendF5CheckBox.Location = new System.Drawing.Point(34, 65);
            this.sendF5CheckBox.Name = "sendF5CheckBox";
            this.sendF5CheckBox.Size = new System.Drawing.Size(66, 17);
            this.sendF5CheckBox.TabIndex = 13;
            this.sendF5CheckBox.Text = "Send F5";
            this.sendF5CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.sendF5CheckBox.UseVisualStyleBackColor = true;
            this.sendF5CheckBox.CheckStateChanged += new System.EventHandler(this.groupCheckBox_CheckStateChanged);
            // 
            // minCheckBox
            // 
            this.minCheckBox.AutoSize = true;
            this.minCheckBox.Location = new System.Drawing.Point(123, 42);
            this.minCheckBox.Name = "minCheckBox";
            this.minCheckBox.Size = new System.Drawing.Size(66, 17);
            this.minCheckBox.TabIndex = 13;
            this.minCheckBox.Text = "Minimize";
            this.minCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.minCheckBox.UseVisualStyleBackColor = true;
            this.minCheckBox.CheckStateChanged += new System.EventHandler(this.groupCheckBox_CheckStateChanged);
            // 
            // sizeCheckBox
            // 
            this.sizeCheckBox.AutoSize = true;
            this.sizeCheckBox.Location = new System.Drawing.Point(34, 19);
            this.sizeCheckBox.Name = "sizeCheckBox";
            this.sizeCheckBox.Size = new System.Drawing.Size(46, 17);
            this.sizeCheckBox.TabIndex = 11;
            this.sizeCheckBox.Text = "Size";
            this.sizeCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.sizeCheckBox.UseVisualStyleBackColor = true;
            this.sizeCheckBox.CheckStateChanged += new System.EventHandler(this.groupCheckBox_CheckStateChanged);
            // 
            // moveCheckBox
            // 
            this.moveCheckBox.AutoSize = true;
            this.moveCheckBox.Location = new System.Drawing.Point(34, 44);
            this.moveCheckBox.Name = "moveCheckBox";
            this.moveCheckBox.Size = new System.Drawing.Size(53, 17);
            this.moveCheckBox.TabIndex = 12;
            this.moveCheckBox.Text = "Move";
            this.moveCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.moveCheckBox.UseVisualStyleBackColor = true;
            this.moveCheckBox.CheckStateChanged += new System.EventHandler(this.groupCheckBox_CheckStateChanged);
            // 
            // titlePage
            // 
            this.titlePage.Controls.Add(this.titleRegexSelector);
            this.titlePage.Location = new System.Drawing.Point(4, 22);
            this.titlePage.Name = "titlePage";
            this.titlePage.Size = new System.Drawing.Size(292, 373);
            this.titlePage.TabIndex = 3;
            this.titlePage.Text = "Title Regex";
            this.titlePage.UseVisualStyleBackColor = true;
            // 
            // titleRegexSelector
            // 
            this.titleRegexSelector.ActiveGroupRegexList = null;
            this.titleRegexSelector.Location = new System.Drawing.Point(0, 0);
            this.titleRegexSelector.Name = "titleRegexSelector";
            this.titleRegexSelector.Size = new System.Drawing.Size(290, 370);
            this.titleRegexSelector.TabIndex = 0;
            // 
            // classPage
            // 
            this.classPage.Controls.Add(this.classRegexSelector);
            this.classPage.Location = new System.Drawing.Point(4, 22);
            this.classPage.Name = "classPage";
            this.classPage.Size = new System.Drawing.Size(292, 373);
            this.classPage.TabIndex = 4;
            this.classPage.Text = "Class Regex";
            this.classPage.UseVisualStyleBackColor = true;
            // 
            // classRegexSelector
            // 
            this.classRegexSelector.ActiveGroupRegexList = null;
            this.classRegexSelector.Location = new System.Drawing.Point(0, 0);
            this.classRegexSelector.Name = "classRegexSelector";
            this.classRegexSelector.Size = new System.Drawing.Size(290, 370);
            this.classRegexSelector.TabIndex = 0;
            // 
            // slotsPage
            // 
            this.slotsPage.Controls.Add(this.maxWinCountTextBox);
            this.slotsPage.Controls.Add(this.maxWinCountLabel);
            this.slotsPage.Controls.Add(this.hotkeyGroupBox);
            this.slotsPage.Controls.Add(this.slotListBox);
            this.slotsPage.Controls.Add(this.showCheckBox);
            this.slotsPage.Controls.Add(this.downSlotButton);
            this.slotsPage.Controls.Add(this.upSlotButton);
            this.slotsPage.Controls.Add(this.locationGroupBox);
            this.slotsPage.Controls.Add(this.sizeGroupBox);
            this.slotsPage.Controls.Add(this.addNewSlotButton);
            this.slotsPage.Controls.Add(this.cloneSelectedSlotButton);
            this.slotsPage.Controls.Add(this.removeSelectedSlotButton);
            this.slotsPage.Location = new System.Drawing.Point(4, 22);
            this.slotsPage.Name = "slotsPage";
            this.slotsPage.Padding = new System.Windows.Forms.Padding(3);
            this.slotsPage.Size = new System.Drawing.Size(292, 373);
            this.slotsPage.TabIndex = 1;
            this.slotsPage.Text = "Slots";
            this.slotsPage.UseVisualStyleBackColor = true;
            // 
            // maxWinCountTextBox
            // 
            this.maxWinCountTextBox.Location = new System.Drawing.Point(128, 231);
            this.maxWinCountTextBox.Name = "maxWinCountTextBox";
            this.maxWinCountTextBox.Size = new System.Drawing.Size(100, 20);
            this.maxWinCountTextBox.TabIndex = 13;
            this.maxWinCountTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.maxWinCountTextBox_Validating);
            this.maxWinCountTextBox.Validated += new System.EventHandler(this.maxWinCountTextBox_Validated);
            // 
            // maxWinCountLabel
            // 
            this.maxWinCountLabel.AutoSize = true;
            this.maxWinCountLabel.Location = new System.Drawing.Point(113, 214);
            this.maxWinCountLabel.Name = "maxWinCountLabel";
            this.maxWinCountLabel.Size = new System.Drawing.Size(100, 13);
            this.maxWinCountLabel.TabIndex = 12;
            this.maxWinCountLabel.Text = "Max Window Count";
            // 
            // hotkeyGroupBox
            // 
            this.hotkeyGroupBox.Controls.Add(this.slotHotkey);
            this.hotkeyGroupBox.Location = new System.Drawing.Point(112, 110);
            this.hotkeyGroupBox.Name = "hotkeyGroupBox";
            this.hotkeyGroupBox.Size = new System.Drawing.Size(151, 97);
            this.hotkeyGroupBox.TabIndex = 11;
            this.hotkeyGroupBox.TabStop = false;
            this.hotkeyGroupBox.Text = "Hotkey";
            // 
            // slotHotkey
            // 
            hotkey1.Key = System.Windows.Forms.Keys.None;
            hotkey1.ModKeys = PlaceMint.Manager.ModifyingKeys.None;
            this.slotHotkey.Hotkey = hotkey1;
            this.slotHotkey.Location = new System.Drawing.Point(15, 19);
            this.slotHotkey.Name = "slotHotkey";
            this.slotHotkey.Size = new System.Drawing.Size(130, 71);
            this.slotHotkey.TabIndex = 10;
            this.slotHotkey.HotkeyChange += new PlaceMint.Access.HotkeyControlCheck.HotkeyChangeHandler(this.slotHotkey_HotkeyChange);
            this.slotHotkey.Validating += new System.ComponentModel.CancelEventHandler(this.Hotkey_Validating);
            // 
            // slotListBox
            // 
            this.slotListBox.FormattingEnabled = true;
            this.slotListBox.Location = new System.Drawing.Point(6, 6);
            this.slotListBox.Name = "slotListBox";
            this.slotListBox.Size = new System.Drawing.Size(100, 329);
            this.slotListBox.TabIndex = 1;
            this.slotListBox.SelectedIndexChanged += new System.EventHandler(this.slotListBox_SelectedIndexChanged);
            // 
            // showCheckBox
            // 
            this.showCheckBox.AutoSize = true;
            this.showCheckBox.Location = new System.Drawing.Point(128, 263);
            this.showCheckBox.Name = "showCheckBox";
            this.showCheckBox.Size = new System.Drawing.Size(112, 17);
            this.showCheckBox.TabIndex = 4;
            this.showCheckBox.Text = "Show Sample Slot";
            this.showCheckBox.UseVisualStyleBackColor = true;
            this.showCheckBox.Click += new System.EventHandler(this.showCheckBox_Click);
            // 
            // downSlotButton
            // 
            this.downSlotButton.Location = new System.Drawing.Point(59, 342);
            this.downSlotButton.Name = "downSlotButton";
            this.downSlotButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.downSlotButton.Size = new System.Drawing.Size(47, 22);
            this.downSlotButton.TabIndex = 7;
            this.downSlotButton.Text = "Down";
            this.downSlotButton.UseVisualStyleBackColor = true;
            this.downSlotButton.Click += new System.EventHandler(this.slotUpDownButton_Click);
            // 
            // upSlotButton
            // 
            this.upSlotButton.Location = new System.Drawing.Point(6, 342);
            this.upSlotButton.Name = "upSlotButton";
            this.upSlotButton.Size = new System.Drawing.Size(47, 22);
            this.upSlotButton.TabIndex = 6;
            this.upSlotButton.Text = "Up";
            this.upSlotButton.UseVisualStyleBackColor = true;
            this.upSlotButton.Click += new System.EventHandler(this.slotUpDownButton_Click);
            // 
            // locationGroupBox
            // 
            this.locationGroupBox.Controls.Add(this.locationCommaLabel);
            this.locationGroupBox.Controls.Add(this.yTextBox);
            this.locationGroupBox.Controls.Add(this.xTextBox);
            this.locationGroupBox.Location = new System.Drawing.Point(112, 58);
            this.locationGroupBox.Name = "locationGroupBox";
            this.locationGroupBox.Size = new System.Drawing.Size(151, 46);
            this.locationGroupBox.TabIndex = 3;
            this.locationGroupBox.TabStop = false;
            this.locationGroupBox.Text = "Location";
            // 
            // locationCommaLabel
            // 
            this.locationCommaLabel.AutoSize = true;
            this.locationCommaLabel.Location = new System.Drawing.Point(77, 22);
            this.locationCommaLabel.Name = "locationCommaLabel";
            this.locationCommaLabel.Size = new System.Drawing.Size(10, 13);
            this.locationCommaLabel.TabIndex = 2;
            this.locationCommaLabel.Text = ",";
            // 
            // yTextBox
            // 
            this.yTextBox.Location = new System.Drawing.Point(95, 19);
            this.yTextBox.Name = "yTextBox";
            this.yTextBox.Size = new System.Drawing.Size(50, 20);
            this.yTextBox.TabIndex = 1;
            this.yTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.locationTextBox_Validating);
            this.yTextBox.Validated += new System.EventHandler(this.shapeTextBox_Validated);
            // 
            // xTextBox
            // 
            this.xTextBox.Location = new System.Drawing.Point(16, 19);
            this.xTextBox.Name = "xTextBox";
            this.xTextBox.Size = new System.Drawing.Size(50, 20);
            this.xTextBox.TabIndex = 0;
            this.xTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.locationTextBox_Validating);
            this.xTextBox.Validated += new System.EventHandler(this.shapeTextBox_Validated);
            // 
            // sizeGroupBox
            // 
            this.sizeGroupBox.Controls.Add(this.sizeByLable);
            this.sizeGroupBox.Controls.Add(this.heightTextBox);
            this.sizeGroupBox.Controls.Add(this.widthTextBox);
            this.sizeGroupBox.Location = new System.Drawing.Point(112, 6);
            this.sizeGroupBox.Name = "sizeGroupBox";
            this.sizeGroupBox.Size = new System.Drawing.Size(151, 46);
            this.sizeGroupBox.TabIndex = 2;
            this.sizeGroupBox.TabStop = false;
            this.sizeGroupBox.Text = "Size";
            // 
            // sizeByLable
            // 
            this.sizeByLable.AutoSize = true;
            this.sizeByLable.Location = new System.Drawing.Point(77, 22);
            this.sizeByLable.Name = "sizeByLable";
            this.sizeByLable.Size = new System.Drawing.Size(12, 13);
            this.sizeByLable.TabIndex = 2;
            this.sizeByLable.Text = "x";
            // 
            // heightTextBox
            // 
            this.heightTextBox.Location = new System.Drawing.Point(95, 20);
            this.heightTextBox.Name = "heightTextBox";
            this.heightTextBox.Size = new System.Drawing.Size(50, 20);
            this.heightTextBox.TabIndex = 1;
            this.heightTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.shapeTextBox_Validating);
            this.heightTextBox.Validated += new System.EventHandler(this.shapeTextBox_Validated);
            // 
            // widthTextBox
            // 
            this.widthTextBox.Location = new System.Drawing.Point(16, 19);
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.Size = new System.Drawing.Size(50, 20);
            this.widthTextBox.TabIndex = 0;
            this.widthTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.shapeTextBox_Validating);
            this.widthTextBox.Validated += new System.EventHandler(this.shapeTextBox_Validated);
            // 
            // addNewSlotButton
            // 
            this.addNewSlotButton.Location = new System.Drawing.Point(112, 286);
            this.addNewSlotButton.Name = "addNewSlotButton";
            this.addNewSlotButton.Size = new System.Drawing.Size(154, 22);
            this.addNewSlotButton.TabIndex = 8;
            this.addNewSlotButton.Text = "Add New Slot";
            this.addNewSlotButton.UseVisualStyleBackColor = true;
            this.addNewSlotButton.Click += new System.EventHandler(this.addNewSlotButton_Click);
            // 
            // cloneSelectedSlotButton
            // 
            this.cloneSelectedSlotButton.Location = new System.Drawing.Point(112, 314);
            this.cloneSelectedSlotButton.Name = "cloneSelectedSlotButton";
            this.cloneSelectedSlotButton.Size = new System.Drawing.Size(154, 22);
            this.cloneSelectedSlotButton.TabIndex = 9;
            this.cloneSelectedSlotButton.Text = "Clone Selected Slot";
            this.cloneSelectedSlotButton.UseVisualStyleBackColor = true;
            this.cloneSelectedSlotButton.Click += new System.EventHandler(this.cloneSelectedSlotButton_Click);
            // 
            // removeSelectedSlotButton
            // 
            this.removeSelectedSlotButton.Location = new System.Drawing.Point(112, 342);
            this.removeSelectedSlotButton.Name = "removeSelectedSlotButton";
            this.removeSelectedSlotButton.Size = new System.Drawing.Size(154, 22);
            this.removeSelectedSlotButton.TabIndex = 0;
            this.removeSelectedSlotButton.Text = "Remove Selected Slot";
            this.removeSelectedSlotButton.UseVisualStyleBackColor = true;
            this.removeSelectedSlotButton.Click += new System.EventHandler(this.removeSelectedSlotButton_Click);
            // 
            // hotkeyPage
            // 
            this.hotkeyPage.Controls.Add(this.bottomGroupBox);
            this.hotkeyPage.Controls.Add(this.maxGroupBox);
            this.hotkeyPage.Controls.Add(this.minGroupBox);
            this.hotkeyPage.Controls.Add(this.restoreGroupBox);
            this.hotkeyPage.Controls.Add(this.showGroupBox);
            this.hotkeyPage.Controls.Add(this.groupHotkeyLabel);
            this.hotkeyPage.Location = new System.Drawing.Point(4, 22);
            this.hotkeyPage.Name = "hotkeyPage";
            this.hotkeyPage.Size = new System.Drawing.Size(292, 373);
            this.hotkeyPage.TabIndex = 2;
            this.hotkeyPage.Text = "Hotkeys";
            this.hotkeyPage.UseVisualStyleBackColor = true;
            // 
            // bottomGroupBox
            // 
            this.bottomGroupBox.Controls.Add(this.bottomHotkey);
            this.bottomGroupBox.Location = new System.Drawing.Point(11, 223);
            this.bottomGroupBox.Name = "bottomGroupBox";
            this.bottomGroupBox.Size = new System.Drawing.Size(109, 94);
            this.bottomGroupBox.TabIndex = 9;
            this.bottomGroupBox.TabStop = false;
            this.bottomGroupBox.Text = "To Bottom";
            // 
            // bottomHotkey
            // 
            hotkey2.Key = System.Windows.Forms.Keys.None;
            hotkey2.ModKeys = PlaceMint.Manager.ModifyingKeys.None;
            this.bottomHotkey.Hotkey = hotkey2;
            this.bottomHotkey.Location = new System.Drawing.Point(6, 17);
            this.bottomHotkey.Name = "bottomHotkey";
            this.bottomHotkey.Size = new System.Drawing.Size(95, 71);
            this.bottomHotkey.TabIndex = 3;
            this.bottomHotkey.HotkeyChange += new PlaceMint.Access.HotkeyControlCheck.HotkeyChangeHandler(this.bottomHotkey_HotkeyChange);
            this.bottomHotkey.Validating += new System.ComponentModel.CancelEventHandler(this.Hotkey_Validating);
            // 
            // maxGroupBox
            // 
            this.maxGroupBox.Controls.Add(this.maxHotkey);
            this.maxGroupBox.Location = new System.Drawing.Point(126, 123);
            this.maxGroupBox.Name = "maxGroupBox";
            this.maxGroupBox.Size = new System.Drawing.Size(109, 94);
            this.maxGroupBox.TabIndex = 8;
            this.maxGroupBox.TabStop = false;
            this.maxGroupBox.Text = "Maximize";
            // 
            // maxHotkey
            // 
            hotkey3.Key = System.Windows.Forms.Keys.None;
            hotkey3.ModKeys = PlaceMint.Manager.ModifyingKeys.None;
            this.maxHotkey.Hotkey = hotkey3;
            this.maxHotkey.Location = new System.Drawing.Point(6, 17);
            this.maxHotkey.Name = "maxHotkey";
            this.maxHotkey.Size = new System.Drawing.Size(95, 71);
            this.maxHotkey.TabIndex = 2;
            this.maxHotkey.Validating += new System.ComponentModel.CancelEventHandler(this.Hotkey_Validating);
            // 
            // minGroupBox
            // 
            this.minGroupBox.Controls.Add(this.minHotkey);
            this.minGroupBox.Location = new System.Drawing.Point(11, 123);
            this.minGroupBox.Name = "minGroupBox";
            this.minGroupBox.Size = new System.Drawing.Size(109, 94);
            this.minGroupBox.TabIndex = 8;
            this.minGroupBox.TabStop = false;
            this.minGroupBox.Text = "Minmimize";
            // 
            // minHotkey
            // 
            hotkey4.Key = System.Windows.Forms.Keys.None;
            hotkey4.ModKeys = PlaceMint.Manager.ModifyingKeys.None;
            this.minHotkey.Hotkey = hotkey4;
            this.minHotkey.Location = new System.Drawing.Point(6, 17);
            this.minHotkey.Name = "minHotkey";
            this.minHotkey.Size = new System.Drawing.Size(95, 71);
            this.minHotkey.TabIndex = 3;
            this.minHotkey.Validating += new System.ComponentModel.CancelEventHandler(this.Hotkey_Validating);
            // 
            // restoreGroupBox
            // 
            this.restoreGroupBox.Controls.Add(this.restoreHotkey);
            this.restoreGroupBox.Location = new System.Drawing.Point(126, 14);
            this.restoreGroupBox.Name = "restoreGroupBox";
            this.restoreGroupBox.Size = new System.Drawing.Size(109, 94);
            this.restoreGroupBox.TabIndex = 7;
            this.restoreGroupBox.TabStop = false;
            this.restoreGroupBox.Text = "Restore";
            // 
            // restoreHotkey
            // 
            hotkey5.Key = System.Windows.Forms.Keys.None;
            hotkey5.ModKeys = PlaceMint.Manager.ModifyingKeys.None;
            this.restoreHotkey.Hotkey = hotkey5;
            this.restoreHotkey.Location = new System.Drawing.Point(6, 17);
            this.restoreHotkey.Name = "restoreHotkey";
            this.restoreHotkey.Size = new System.Drawing.Size(95, 71);
            this.restoreHotkey.TabIndex = 1;
            this.restoreHotkey.Validating += new System.ComponentModel.CancelEventHandler(this.Hotkey_Validating);
            // 
            // showGroupBox
            // 
            this.showGroupBox.Controls.Add(this.showHotkey);
            this.showGroupBox.Location = new System.Drawing.Point(11, 14);
            this.showGroupBox.Name = "showGroupBox";
            this.showGroupBox.Size = new System.Drawing.Size(109, 94);
            this.showGroupBox.TabIndex = 6;
            this.showGroupBox.TabStop = false;
            this.showGroupBox.Text = "Show";
            // 
            // showHotkey
            // 
            hotkey6.Key = System.Windows.Forms.Keys.None;
            hotkey6.ModKeys = PlaceMint.Manager.ModifyingKeys.None;
            this.showHotkey.Hotkey = hotkey6;
            this.showHotkey.Location = new System.Drawing.Point(6, 19);
            this.showHotkey.Name = "showHotkey";
            this.showHotkey.Size = new System.Drawing.Size(95, 71);
            this.showHotkey.TabIndex = 0;
            this.showHotkey.HotkeyChange += new PlaceMint.Access.HotkeyControlCheck.HotkeyChangeHandler(this.showHotkey_HotkeyChange);
            this.showHotkey.Validating += new System.ComponentModel.CancelEventHandler(this.Hotkey_Validating);
            // 
            // groupHotkeyLabel
            // 
            this.groupHotkeyLabel.AutoSize = true;
            this.groupHotkeyLabel.Location = new System.Drawing.Point(129, 228);
            this.groupHotkeyLabel.Name = "groupHotkeyLabel";
            this.groupHotkeyLabel.Size = new System.Drawing.Size(132, 78);
            this.groupHotkeyLabel.TabIndex = 5;
            this.groupHotkeyLabel.Text = "These hotkeys will perform\r\nthe specified action on all\r\nthe slots in the group,\r" +
                "\nstarting with the the \r\nfirst slot and ending with\r\nthe last slot.";
            // 
            // addNewGroupButton
            // 
            this.addNewGroupButton.Location = new System.Drawing.Point(12, 335);
            this.addNewGroupButton.Name = "addNewGroupButton";
            this.addNewGroupButton.Size = new System.Drawing.Size(150, 22);
            this.addNewGroupButton.TabIndex = 3;
            this.addNewGroupButton.Text = "Add New Group";
            this.addNewGroupButton.UseVisualStyleBackColor = true;
            this.addNewGroupButton.Click += new System.EventHandler(this.addNewGroupButton_Click);
            // 
            // cloneSelectedGroupButton
            // 
            this.cloneSelectedGroupButton.Location = new System.Drawing.Point(12, 363);
            this.cloneSelectedGroupButton.Name = "cloneSelectedGroupButton";
            this.cloneSelectedGroupButton.Size = new System.Drawing.Size(150, 22);
            this.cloneSelectedGroupButton.TabIndex = 4;
            this.cloneSelectedGroupButton.Text = "Clone Selected Group";
            this.cloneSelectedGroupButton.UseVisualStyleBackColor = true;
            this.cloneSelectedGroupButton.Click += new System.EventHandler(this.cloneSelectedGroupButton_Click);
            // 
            // groupListBox
            // 
            this.groupListBox.FormattingEnabled = true;
            this.groupListBox.Location = new System.Drawing.Point(12, 42);
            this.groupListBox.Name = "groupListBox";
            this.groupListBox.Size = new System.Drawing.Size(150, 251);
            this.groupListBox.TabIndex = 0;
            this.groupListBox.SelectedIndexChanged += new System.EventHandler(this.groupListBox_SelectedIndexChanged);
            // 
            // upGroupButton
            // 
            this.upGroupButton.Location = new System.Drawing.Point(12, 307);
            this.upGroupButton.Name = "upGroupButton";
            this.upGroupButton.Size = new System.Drawing.Size(72, 22);
            this.upGroupButton.TabIndex = 1;
            this.upGroupButton.Text = "Up";
            this.upGroupButton.UseVisualStyleBackColor = true;
            this.upGroupButton.Click += new System.EventHandler(this.groupUpDownButton_Click);
            // 
            // downGroupButton
            // 
            this.downGroupButton.Location = new System.Drawing.Point(90, 307);
            this.downGroupButton.Name = "downGroupButton";
            this.downGroupButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.downGroupButton.Size = new System.Drawing.Size(72, 22);
            this.downGroupButton.TabIndex = 2;
            this.downGroupButton.Text = "Down";
            this.downGroupButton.UseVisualStyleBackColor = true;
            this.downGroupButton.Click += new System.EventHandler(this.groupUpDownButton_Click);
            // 
            // detailsGrabberItem
            // 
            this.detailsGrabberItem.Name = "detailsGrabberItem";
            this.detailsGrabberItem.Size = new System.Drawing.Size(93, 20);
            this.detailsGrabberItem.Text = "Details Grabber";
            this.detailsGrabberItem.Click += new System.EventHandler(this.detailsGrabberItem_Click);
            // 
            // ConfigureGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 425);
            this.Controls.Add(this.groupListBox);
            this.Controls.Add(this.groupTab);
            this.Controls.Add(this.downGroupButton);
            this.Controls.Add(this.upGroupButton);
            this.Controls.Add(this.addNewGroupButton);
            this.Controls.Add(this.cloneSelectedGroupButton);
            this.Controls.Add(this.removeSelectedGroupButton);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(999, 999);
            this.MinimumSize = new System.Drawing.Size(403, 307);
            this.Name = "ConfigureGUI";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure Window Groups";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigureGUI_FormClosed);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.groupTab.ResumeLayout(false);
            this.settingsPage.ResumeLayout(false);
            this.settingsPage.PerformLayout();
            this.optionsGroupBox.ResumeLayout(false);
            this.optionsGroupBox.PerformLayout();
            this.titlePage.ResumeLayout(false);
            this.classPage.ResumeLayout(false);
            this.slotsPage.ResumeLayout(false);
            this.slotsPage.PerformLayout();
            this.hotkeyGroupBox.ResumeLayout(false);
            this.locationGroupBox.ResumeLayout(false);
            this.locationGroupBox.PerformLayout();
            this.sizeGroupBox.ResumeLayout(false);
            this.sizeGroupBox.PerformLayout();
            this.hotkeyPage.ResumeLayout(false);
            this.hotkeyPage.PerformLayout();
            this.bottomGroupBox.ResumeLayout(false);
            this.maxGroupBox.ResumeLayout(false);
            this.minGroupBox.ResumeLayout(false);
            this.restoreGroupBox.ResumeLayout(false);
            this.showGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button removeSelectedGroupButton;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem saveItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsItem;
        private System.Windows.Forms.ToolStripMenuItem exitItem;
        private System.Windows.Forms.Label groupTitleLabel;
        private System.Windows.Forms.TextBox groupTitleTextBox;
        private System.Windows.Forms.TabControl groupTab;
        private System.Windows.Forms.TabPage settingsPage;
        private System.Windows.Forms.TabPage slotsPage;
        private System.Windows.Forms.ToolStripMenuItem regexEditItem;
        private System.Windows.Forms.CheckBox minCheckBox;
        private System.Windows.Forms.CheckBox sizeCheckBox;
        private System.Windows.Forms.CheckBox moveCheckBox;
        private System.Windows.Forms.Button addNewGroupButton;
        private System.Windows.Forms.Button cloneSelectedGroupButton;
        private System.Windows.Forms.Button addNewSlotButton;
        private System.Windows.Forms.Button cloneSelectedSlotButton;
        private System.Windows.Forms.Button removeSelectedSlotButton;
        private System.Windows.Forms.GroupBox locationGroupBox;
        private System.Windows.Forms.Label locationCommaLabel;
        private System.Windows.Forms.TextBox heightTextBox;
        private System.Windows.Forms.TextBox widthTextBox;
        private System.Windows.Forms.GroupBox sizeGroupBox;
        private System.Windows.Forms.Label sizeByLable;
        private System.Windows.Forms.TextBox yTextBox;
        private System.Windows.Forms.TextBox xTextBox;
        private System.Windows.Forms.CheckBox showCheckBox;
        private System.Windows.Forms.ListBox slotListBox;
        private System.Windows.Forms.ListBox groupListBox;
        private System.Windows.Forms.Button upGroupButton;
        private System.Windows.Forms.Button downGroupButton;
        private System.Windows.Forms.Button downSlotButton;
        private System.Windows.Forms.Button upSlotButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem slotTemplatesEdituItem;
        private HotkeyControlCheck slotHotkey;
        private System.Windows.Forms.GroupBox optionsGroupBox;
        private System.Windows.Forms.GroupBox hotkeyGroupBox;
        private System.Windows.Forms.CheckBox maxCheckBox;
        private System.Windows.Forms.TabPage hotkeyPage;
        private HotkeyControlCheck minHotkey;
        private HotkeyControlCheck maxHotkey;
        private HotkeyControlCheck restoreHotkey;
        private HotkeyControlCheck showHotkey;
        private System.Windows.Forms.Label groupHotkeyLabel;
        private System.Windows.Forms.GroupBox restoreGroupBox;
        private System.Windows.Forms.GroupBox showGroupBox;
        private System.Windows.Forms.GroupBox maxGroupBox;
        private System.Windows.Forms.GroupBox minGroupBox;
        private System.Windows.Forms.CheckBox sendF5CheckBox;
        private System.Windows.Forms.Label maxWinCountLabel;
        private System.Windows.Forms.TextBox maxWinCountTextBox;
        private System.Windows.Forms.TabPage titlePage;
        private System.Windows.Forms.TabPage classPage;
        private RegexSelector titleRegexSelector;
        private RegexSelector classRegexSelector;
        private System.Windows.Forms.GroupBox bottomGroupBox;
        private HotkeyControlCheck bottomHotkey;
        private System.Windows.Forms.ToolStripMenuItem detailsGrabberItem;
    }
}
