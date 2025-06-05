namespace PlaceMint.Access
{
    partial class OptionsGUI
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
            this.loggingLevelLabel = new System.Windows.Forms.Label();
            this.updateFrequencyTextBox = new System.Windows.Forms.TextBox();
            this.loggingComboBox = new System.Windows.Forms.ComboBox();
            this.minToTrayCheckBox = new System.Windows.Forms.CheckBox();
            this.dragDrowSwapCheckBox = new System.Windows.Forms.CheckBox();
            this.updateFrequencyLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.regexListFileTextBox = new System.Windows.Forms.TextBox();
            this.regexListFileLabel = new System.Windows.Forms.Label();
            this.slotTemplateFileTextBox = new System.Windows.Forms.TextBox();
            this.slotTemplateFileLabel = new System.Windows.Forms.Label();
            this.regexListBrowseButton = new System.Windows.Forms.Button();
            this.slotTemplateBrowseButton = new System.Windows.Forms.Button();
            this.swapOnNewFindCheckBox = new System.Windows.Forms.CheckBox();
            this.optionsTabs = new System.Windows.Forms.TabControl();
            this.filesPage = new System.Windows.Forms.TabPage();
            this.configLabel = new System.Windows.Forms.Label();
            this.filesLabel = new System.Windows.Forms.Label();
            this.swapPage = new System.Windows.Forms.TabPage();
            this.overlayClosestCheckBox = new System.Windows.Forms.CheckBox();
            this.hotkeySwapCheckBox = new System.Windows.Forms.CheckBox();
            this.mouseLabel = new System.Windows.Forms.Label();
            this.newFindLabel = new System.Windows.Forms.Label();
            this.swapLabel = new System.Windows.Forms.Label();
            this.PositionPage = new System.Windows.Forms.TabPage();
            this.rippleHotkey = new PlaceMint.Access.HotkeyControlCheck();
            this.rippleLabel = new System.Windows.Forms.Label();
            this.PositionLabel = new System.Windows.Forms.Label();
            this.rippleForward = new System.Windows.Forms.CheckBox();
            this.otherPage = new System.Windows.Forms.TabPage();
            this.orTitleMatchingLabel = new System.Windows.Forms.Label();
            this.orTitleMatching = new System.Windows.Forms.CheckBox();
            this.pauseGroup = new System.Windows.Forms.GroupBox();
            this.pauseHotkey = new PlaceMint.Access.HotkeyControlCheck();
            this.otherLabel = new System.Windows.Forms.Label();
            this.groupHotkeysCheckBox = new System.Windows.Forms.CheckBox();
            this.breadthFirstFill = new System.Windows.Forms.CheckBox();
            this.breadthFirstLabel = new System.Windows.Forms.Label();
            this.optionsTabs.SuspendLayout();
            this.filesPage.SuspendLayout();
            this.swapPage.SuspendLayout();
            this.PositionPage.SuspendLayout();
            this.otherPage.SuspendLayout();
            this.pauseGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // loggingLevelLabel
            // 
            this.loggingLevelLabel.AutoSize = true;
            this.loggingLevelLabel.Location = new System.Drawing.Point(8, 77);
            this.loggingLevelLabel.Name = "loggingLevelLabel";
            this.loggingLevelLabel.Size = new System.Drawing.Size(45, 13);
            this.loggingLevelLabel.TabIndex = 0;
            this.loggingLevelLabel.Text = "Logging";
            // 
            // updateFrequencyTextBox
            // 
            this.updateFrequencyTextBox.Location = new System.Drawing.Point(108, 47);
            this.updateFrequencyTextBox.Name = "updateFrequencyTextBox";
            this.updateFrequencyTextBox.Size = new System.Drawing.Size(68, 20);
            this.updateFrequencyTextBox.TabIndex = 0;
            this.updateFrequencyTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.updateFrequencyTextBox_Validating);
            // 
            // loggingComboBox
            // 
            this.loggingComboBox.FormattingEnabled = true;
            this.loggingComboBox.Location = new System.Drawing.Point(108, 74);
            this.loggingComboBox.Name = "loggingComboBox";
            this.loggingComboBox.Size = new System.Drawing.Size(68, 21);
            this.loggingComboBox.TabIndex = 3;
            // 
            // minToTrayCheckBox
            // 
            this.minToTrayCheckBox.AutoSize = true;
            this.minToTrayCheckBox.Location = new System.Drawing.Point(11, 101);
            this.minToTrayCheckBox.Name = "minToTrayCheckBox";
            this.minToTrayCheckBox.Size = new System.Drawing.Size(102, 17);
            this.minToTrayCheckBox.TabIndex = 5;
            this.minToTrayCheckBox.Text = "Minimize to Tray";
            this.minToTrayCheckBox.UseVisualStyleBackColor = true;
            // 
            // dragDrowSwapCheckBox
            // 
            this.dragDrowSwapCheckBox.AutoSize = true;
            this.dragDrowSwapCheckBox.Location = new System.Drawing.Point(11, 31);
            this.dragDrowSwapCheckBox.Name = "dragDrowSwapCheckBox";
            this.dragDrowSwapCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dragDrowSwapCheckBox.Size = new System.Drawing.Size(128, 17);
            this.dragDrowSwapCheckBox.TabIndex = 4;
            this.dragDrowSwapCheckBox.Text = "Drag && Drop Swaping";
            this.dragDrowSwapCheckBox.UseVisualStyleBackColor = true;
            this.dragDrowSwapCheckBox.CheckedChanged += new System.EventHandler(this.dragDrowSwapCheckBox_CheckedChanged);
            // 
            // updateFrequencyLabel
            // 
            this.updateFrequencyLabel.AutoSize = true;
            this.updateFrequencyLabel.Location = new System.Drawing.Point(8, 50);
            this.updateFrequencyLabel.Name = "updateFrequencyLabel";
            this.updateFrequencyLabel.Size = new System.Drawing.Size(94, 13);
            this.updateFrequencyLabel.TabIndex = 0;
            this.updateFrequencyLabel.Text = "Refresh delay (ms)";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(213, 251);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(108, 251);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // regexListFileTextBox
            // 
            this.regexListFileTextBox.Location = new System.Drawing.Point(147, 40);
            this.regexListFileTextBox.Name = "regexListFileTextBox";
            this.regexListFileTextBox.Size = new System.Drawing.Size(145, 20);
            this.regexListFileTextBox.TabIndex = 1;
            // 
            // regexListFileLabel
            // 
            this.regexListFileLabel.AutoSize = true;
            this.regexListFileLabel.Location = new System.Drawing.Point(8, 43);
            this.regexListFileLabel.Name = "regexListFileLabel";
            this.regexListFileLabel.Size = new System.Drawing.Size(117, 13);
            this.regexListFileLabel.TabIndex = 0;
            this.regexListFileLabel.Text = "Regular Expression List";
            // 
            // slotTemplateFileTextBox
            // 
            this.slotTemplateFileTextBox.Location = new System.Drawing.Point(147, 71);
            this.slotTemplateFileTextBox.Name = "slotTemplateFileTextBox";
            this.slotTemplateFileTextBox.Size = new System.Drawing.Size(145, 20);
            this.slotTemplateFileTextBox.TabIndex = 1;
            // 
            // slotTemplateFileLabel
            // 
            this.slotTemplateFileLabel.AutoSize = true;
            this.slotTemplateFileLabel.Location = new System.Drawing.Point(8, 74);
            this.slotTemplateFileLabel.Name = "slotTemplateFileLabel";
            this.slotTemplateFileLabel.Size = new System.Drawing.Size(91, 13);
            this.slotTemplateFileLabel.TabIndex = 0;
            this.slotTemplateFileLabel.Text = "Slot Template List";
            // 
            // regexListBrowseButton
            // 
            this.regexListBrowseButton.Location = new System.Drawing.Point(298, 38);
            this.regexListBrowseButton.Name = "regexListBrowseButton";
            this.regexListBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.regexListBrowseButton.TabIndex = 2;
            this.regexListBrowseButton.Text = "Browse";
            this.regexListBrowseButton.UseVisualStyleBackColor = true;
            this.regexListBrowseButton.Click += new System.EventHandler(this.regexListBrowseButton_Click);
            // 
            // slotTemplateBrowseButton
            // 
            this.slotTemplateBrowseButton.Location = new System.Drawing.Point(298, 69);
            this.slotTemplateBrowseButton.Name = "slotTemplateBrowseButton";
            this.slotTemplateBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.slotTemplateBrowseButton.TabIndex = 2;
            this.slotTemplateBrowseButton.Text = "Browse";
            this.slotTemplateBrowseButton.UseVisualStyleBackColor = true;
            this.slotTemplateBrowseButton.Click += new System.EventHandler(this.slotTemplateBrowseButton_Click);
            // 
            // swapOnNewFindCheckBox
            // 
            this.swapOnNewFindCheckBox.AutoSize = true;
            this.swapOnNewFindCheckBox.Location = new System.Drawing.Point(33, 54);
            this.swapOnNewFindCheckBox.Name = "swapOnNewFindCheckBox";
            this.swapOnNewFindCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.swapOnNewFindCheckBox.Size = new System.Drawing.Size(118, 17);
            this.swapOnNewFindCheckBox.TabIndex = 4;
            this.swapOnNewFindCheckBox.Text = "Swap On New Find";
            this.swapOnNewFindCheckBox.UseVisualStyleBackColor = true;
            // 
            // optionsTabs
            // 
            this.optionsTabs.Controls.Add(this.filesPage);
            this.optionsTabs.Controls.Add(this.swapPage);
            this.optionsTabs.Controls.Add(this.PositionPage);
            this.optionsTabs.Controls.Add(this.otherPage);
            this.optionsTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.optionsTabs.Location = new System.Drawing.Point(0, 0);
            this.optionsTabs.Name = "optionsTabs";
            this.optionsTabs.SelectedIndex = 0;
            this.optionsTabs.Size = new System.Drawing.Size(390, 245);
            this.optionsTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.optionsTabs.TabIndex = 8;
            // 
            // filesPage
            // 
            this.filesPage.Controls.Add(this.configLabel);
            this.filesPage.Controls.Add(this.filesLabel);
            this.filesPage.Controls.Add(this.regexListFileTextBox);
            this.filesPage.Controls.Add(this.slotTemplateFileTextBox);
            this.filesPage.Controls.Add(this.regexListBrowseButton);
            this.filesPage.Controls.Add(this.slotTemplateFileLabel);
            this.filesPage.Controls.Add(this.slotTemplateBrowseButton);
            this.filesPage.Controls.Add(this.regexListFileLabel);
            this.filesPage.Location = new System.Drawing.Point(4, 22);
            this.filesPage.Name = "filesPage";
            this.filesPage.Padding = new System.Windows.Forms.Padding(3);
            this.filesPage.Size = new System.Drawing.Size(382, 219);
            this.filesPage.TabIndex = 0;
            this.filesPage.Text = "Files";
            this.filesPage.UseVisualStyleBackColor = true;
            // 
            // configLabel
            // 
            this.configLabel.AutoSize = true;
            this.configLabel.Location = new System.Drawing.Point(8, 112);
            this.configLabel.Name = "configLabel";
            this.configLabel.Size = new System.Drawing.Size(326, 13);
            this.configLabel.TabIndex = 3;
            this.configLabel.Text = "Use File > Load in the main window to change the configuration file.";
            // 
            // filesLabel
            // 
            this.filesLabel.AutoSize = true;
            this.filesLabel.Location = new System.Drawing.Point(8, 12);
            this.filesLabel.Name = "filesLabel";
            this.filesLabel.Size = new System.Drawing.Size(259, 13);
            this.filesLabel.TabIndex = 3;
            this.filesLabel.Text = "Select the files to use when seting up a configuration.";
            // 
            // swapPage
            // 
            this.swapPage.Controls.Add(this.overlayClosestCheckBox);
            this.swapPage.Controls.Add(this.hotkeySwapCheckBox);
            this.swapPage.Controls.Add(this.mouseLabel);
            this.swapPage.Controls.Add(this.newFindLabel);
            this.swapPage.Controls.Add(this.swapLabel);
            this.swapPage.Controls.Add(this.dragDrowSwapCheckBox);
            this.swapPage.Controls.Add(this.swapOnNewFindCheckBox);
            this.swapPage.Location = new System.Drawing.Point(4, 22);
            this.swapPage.Name = "swapPage";
            this.swapPage.Padding = new System.Windows.Forms.Padding(3);
            this.swapPage.Size = new System.Drawing.Size(382, 219);
            this.swapPage.TabIndex = 1;
            this.swapPage.Text = "Swapping";
            this.swapPage.UseVisualStyleBackColor = true;
            // 
            // overlayClosestCheckBox
            // 
            this.overlayClosestCheckBox.AutoSize = true;
            this.overlayClosestCheckBox.Location = new System.Drawing.Point(33, 116);
            this.overlayClosestCheckBox.Name = "overlayClosestCheckBox";
            this.overlayClosestCheckBox.Size = new System.Drawing.Size(117, 17);
            this.overlayClosestCheckBox.TabIndex = 7;
            this.overlayClosestCheckBox.Text = "Outline Closest Slot";
            this.overlayClosestCheckBox.UseVisualStyleBackColor = true;
            // 
            // hotkeySwapCheckBox
            // 
            this.hotkeySwapCheckBox.AutoSize = true;
            this.hotkeySwapCheckBox.Location = new System.Drawing.Point(11, 139);
            this.hotkeySwapCheckBox.Name = "hotkeySwapCheckBox";
            this.hotkeySwapCheckBox.Size = new System.Drawing.Size(110, 17);
            this.hotkeySwapCheckBox.TabIndex = 6;
            this.hotkeySwapCheckBox.Text = "Hotkey Swapping";
            this.hotkeySwapCheckBox.UseVisualStyleBackColor = true;
            // 
            // mouseLabel
            // 
            this.mouseLabel.AutoSize = true;
            this.mouseLabel.Location = new System.Drawing.Point(48, 159);
            this.mouseLabel.Name = "mouseLabel";
            this.mouseLabel.Size = new System.Drawing.Size(253, 26);
            this.mouseLabel.TabIndex = 5;
            this.mouseLabel.Text = "Moves the active window to the slot associated with\r\nthe pressed hotkey.";
            // 
            // newFindLabel
            // 
            this.newFindLabel.AutoSize = true;
            this.newFindLabel.Location = new System.Drawing.Point(52, 74);
            this.newFindLabel.Name = "newFindLabel";
            this.newFindLabel.Size = new System.Drawing.Size(300, 39);
            this.newFindLabel.TabIndex = 5;
            this.newFindLabel.Text = "When checked, a new window will be placed in the slot it was\r\nclosest to when it " +
                "was created. If this closest slot is occupied,\r\nthe old window will be moved to " +
                "the first available slot.\r\n";
            // 
            // swapLabel
            // 
            this.swapLabel.AutoSize = true;
            this.swapLabel.Location = new System.Drawing.Point(8, 12);
            this.swapLabel.Name = "swapLabel";
            this.swapLabel.Size = new System.Drawing.Size(350, 13);
            this.swapLabel.TabIndex = 5;
            this.swapLabel.Text = "Set options on how you want to be able to move windows between slots.";
            // 
            // PositionPage
            // 
            this.PositionPage.Controls.Add(this.breadthFirstLabel);
            this.PositionPage.Controls.Add(this.breadthFirstFill);
            this.PositionPage.Controls.Add(this.rippleHotkey);
            this.PositionPage.Controls.Add(this.rippleLabel);
            this.PositionPage.Controls.Add(this.PositionLabel);
            this.PositionPage.Controls.Add(this.rippleForward);
            this.PositionPage.Location = new System.Drawing.Point(4, 22);
            this.PositionPage.Name = "PositionPage";
            this.PositionPage.Padding = new System.Windows.Forms.Padding(3);
            this.PositionPage.Size = new System.Drawing.Size(382, 219);
            this.PositionPage.TabIndex = 3;
            this.PositionPage.Text = "Positioning";
            this.PositionPage.UseVisualStyleBackColor = true;
            // 
            // rippleHotkey
            // 
            hotkey1.Key = System.Windows.Forms.Keys.None;
            hotkey1.ModKeys = PlaceMint.Manager.ModifyingKeys.None;
            this.rippleHotkey.Hotkey = hotkey1;
            this.rippleHotkey.Location = new System.Drawing.Point(245, 29);
            this.rippleHotkey.Name = "rippleHotkey";
            this.rippleHotkey.Size = new System.Drawing.Size(95, 71);
            this.rippleHotkey.TabIndex = 9;
            // 
            // rippleLabel
            // 
            this.rippleLabel.AutoSize = true;
            this.rippleLabel.Location = new System.Drawing.Point(32, 48);
            this.rippleLabel.Name = "rippleLabel";
            this.rippleLabel.Size = new System.Drawing.Size(207, 52);
            this.rippleLabel.TabIndex = 8;
            this.rippleLabel.Text = "Windows will be moved forward \r\nin the slot list if a slot is made available.\r\nTh" +
                "e hotkey can be used when Ripple\r\nForward is disabled to do a one time ripple.";
            // 
            // PositionLabel
            // 
            this.PositionLabel.AutoSize = true;
            this.PositionLabel.Location = new System.Drawing.Point(8, 12);
            this.PositionLabel.Name = "PositionLabel";
            this.PositionLabel.Size = new System.Drawing.Size(315, 13);
            this.PositionLabel.TabIndex = 7;
            this.PositionLabel.Text = "Set options on how you want windows to be positioned in groups.";
            // 
            // rippleForward
            // 
            this.rippleForward.AutoSize = true;
            this.rippleForward.Location = new System.Drawing.Point(6, 28);
            this.rippleForward.Name = "rippleForward";
            this.rippleForward.Size = new System.Drawing.Size(97, 17);
            this.rippleForward.TabIndex = 6;
            this.rippleForward.Text = "Ripple Forward";
            this.rippleForward.UseVisualStyleBackColor = true;
            this.rippleForward.CheckedChanged += new System.EventHandler(this.rippleForward_CheckedChanged);
            // 
            // otherPage
            // 
            this.otherPage.Controls.Add(this.orTitleMatchingLabel);
            this.otherPage.Controls.Add(this.orTitleMatching);
            this.otherPage.Controls.Add(this.pauseGroup);
            this.otherPage.Controls.Add(this.otherLabel);
            this.otherPage.Controls.Add(this.updateFrequencyLabel);
            this.otherPage.Controls.Add(this.loggingComboBox);
            this.otherPage.Controls.Add(this.groupHotkeysCheckBox);
            this.otherPage.Controls.Add(this.minToTrayCheckBox);
            this.otherPage.Controls.Add(this.loggingLevelLabel);
            this.otherPage.Controls.Add(this.updateFrequencyTextBox);
            this.otherPage.Location = new System.Drawing.Point(4, 22);
            this.otherPage.Name = "otherPage";
            this.otherPage.Size = new System.Drawing.Size(382, 219);
            this.otherPage.TabIndex = 2;
            this.otherPage.Text = "Other";
            this.otherPage.UseVisualStyleBackColor = true;
            // 
            // orTitleMatchingLabel
            // 
            this.orTitleMatchingLabel.AutoSize = true;
            this.orTitleMatchingLabel.Location = new System.Drawing.Point(25, 149);
            this.orTitleMatchingLabel.Name = "orTitleMatchingLabel";
            this.orTitleMatchingLabel.Size = new System.Drawing.Size(161, 26);
            this.orTitleMatchingLabel.TabIndex = 12;
            this.orTitleMatchingLabel.Text = "Match a window if it matches\r\nthe window class or window title.";
            // 
            // orTitleMatching
            // 
            this.orTitleMatching.AutoSize = true;
            this.orTitleMatching.Location = new System.Drawing.Point(11, 125);
            this.orTitleMatching.Name = "orTitleMatching";
            this.orTitleMatching.Size = new System.Drawing.Size(107, 17);
            this.orTitleMatching.TabIndex = 11;
            this.orTitleMatching.Text = "Or Title Matching";
            this.orTitleMatching.UseVisualStyleBackColor = true;
            // 
            // pauseGroup
            // 
            this.pauseGroup.Controls.Add(this.pauseHotkey);
            this.pauseGroup.Location = new System.Drawing.Point(209, 12);
            this.pauseGroup.Name = "pauseGroup";
            this.pauseGroup.Size = new System.Drawing.Size(111, 94);
            this.pauseGroup.TabIndex = 10;
            this.pauseGroup.TabStop = false;
            this.pauseGroup.Text = "Pause Hotkey";
            // 
            // pauseHotkey
            // 
            hotkey2.Key = System.Windows.Forms.Keys.None;
            hotkey2.ModKeys = PlaceMint.Manager.ModifyingKeys.None;
            this.pauseHotkey.Hotkey = hotkey2;
            this.pauseHotkey.Location = new System.Drawing.Point(6, 19);
            this.pauseHotkey.Name = "pauseHotkey";
            this.pauseHotkey.Size = new System.Drawing.Size(95, 71);
            this.pauseHotkey.TabIndex = 8;
            // 
            // otherLabel
            // 
            this.otherLabel.AutoSize = true;
            this.otherLabel.Location = new System.Drawing.Point(8, 12);
            this.otherLabel.Name = "otherLabel";
            this.otherLabel.Size = new System.Drawing.Size(145, 13);
            this.otherLabel.TabIndex = 6;
            this.otherLabel.Text = "Set the various other options.";
            // 
            // groupHotkeysCheckBox
            // 
            this.groupHotkeysCheckBox.AutoSize = true;
            this.groupHotkeysCheckBox.Location = new System.Drawing.Point(181, 124);
            this.groupHotkeysCheckBox.Name = "groupHotkeysCheckBox";
            this.groupHotkeysCheckBox.Size = new System.Drawing.Size(139, 17);
            this.groupHotkeysCheckBox.TabIndex = 5;
            this.groupHotkeysCheckBox.Text = "Window Group Hotkeys";
            this.groupHotkeysCheckBox.UseVisualStyleBackColor = true;
            // 
            // breadthFirstFill
            // 
            this.breadthFirstFill.AutoSize = true;
            this.breadthFirstFill.Location = new System.Drawing.Point(6, 107);
            this.breadthFirstFill.Name = "breadthFirstFill";
            this.breadthFirstFill.Size = new System.Drawing.Size(100, 17);
            this.breadthFirstFill.TabIndex = 10;
            this.breadthFirstFill.Text = "Breadth First Fill";
            this.breadthFirstFill.UseVisualStyleBackColor = true;
            // 
            // breadthFirstLabel
            // 
            this.breadthFirstLabel.AutoSize = true;
            this.breadthFirstLabel.Location = new System.Drawing.Point(35, 131);
            this.breadthFirstLabel.Name = "breadthFirstLabel";
            this.breadthFirstLabel.Size = new System.Drawing.Size(264, 13);
            this.breadthFirstLabel.TabIndex = 11;
            this.breadthFirstLabel.Text = "Fill all slots before placing a second window in a stack.";
            // 
            // OptionsGUI
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(390, 278);
            this.Controls.Add(this.optionsTabs);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsGUI";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OptionsGUI_FormClosed);
            this.optionsTabs.ResumeLayout(false);
            this.filesPage.ResumeLayout(false);
            this.filesPage.PerformLayout();
            this.swapPage.ResumeLayout(false);
            this.swapPage.PerformLayout();
            this.PositionPage.ResumeLayout(false);
            this.PositionPage.PerformLayout();
            this.otherPage.ResumeLayout(false);
            this.otherPage.PerformLayout();
            this.pauseGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label loggingLevelLabel;
        private System.Windows.Forms.TextBox updateFrequencyTextBox;
        private System.Windows.Forms.ComboBox loggingComboBox;
        private System.Windows.Forms.CheckBox minToTrayCheckBox;
        private System.Windows.Forms.CheckBox dragDrowSwapCheckBox;
        private System.Windows.Forms.Label updateFrequencyLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox regexListFileTextBox;
        private System.Windows.Forms.Label regexListFileLabel;
        private System.Windows.Forms.TextBox slotTemplateFileTextBox;
        private System.Windows.Forms.Label slotTemplateFileLabel;
        private System.Windows.Forms.Button regexListBrowseButton;
        private System.Windows.Forms.Button slotTemplateBrowseButton;
        private System.Windows.Forms.CheckBox swapOnNewFindCheckBox;
        private System.Windows.Forms.TabControl optionsTabs;
        private System.Windows.Forms.TabPage filesPage;
        private System.Windows.Forms.TabPage swapPage;
        private System.Windows.Forms.TabPage otherPage;
        private System.Windows.Forms.Label filesLabel;
        private System.Windows.Forms.Label swapLabel;
        private System.Windows.Forms.Label newFindLabel;
        private System.Windows.Forms.Label otherLabel;
        private System.Windows.Forms.Label configLabel;
        private System.Windows.Forms.CheckBox hotkeySwapCheckBox;
        private System.Windows.Forms.Label mouseLabel;
        private System.Windows.Forms.CheckBox groupHotkeysCheckBox;
        private HotkeyControlCheck pauseHotkey;
        private System.Windows.Forms.GroupBox pauseGroup;
        private System.Windows.Forms.CheckBox overlayClosestCheckBox;
        private System.Windows.Forms.TabPage PositionPage;
        private System.Windows.Forms.Label PositionLabel;
        private System.Windows.Forms.CheckBox rippleForward;
        private HotkeyControlCheck rippleHotkey;
        private System.Windows.Forms.Label rippleLabel;
        private System.Windows.Forms.Label orTitleMatchingLabel;
        private System.Windows.Forms.CheckBox orTitleMatching;
        private System.Windows.Forms.Label breadthFirstLabel;
        private System.Windows.Forms.CheckBox breadthFirstFill;

    }
}