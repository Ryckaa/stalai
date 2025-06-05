using System;
using System.ComponentModel;
using System.Windows.Forms;

using PlaceMint.Manager;
using PlaceMint.Manager.PMException;
namespace PlaceMint.Access
{
    using Properties;

    /// <summary>
    /// Gui to alter AppSettings
    /// </summary>
    public partial class OptionsGUI : Form
    {
        private const int TOOL_TIP_TIME = 1500; //ms

        private AppSettings _appSettings;
        private MainGUI _parent;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appS">Settings to alter</param>
        /// <param name="parent">Parent Gui</param>
        public OptionsGUI(AppSettings appS, MainGUI parent)
        {
            _appSettings = appS.DeepClone();
            _parent = parent;
            this.Owner = _parent;
            _parent.Enabled = false;
            InitializeComponent();
            this.Icon = Resources.icon;
            loggingComboBox.DataSource = Enum.GetValues(typeof(LoggingLevel));

            slotTemplateFileTextBox.Text = _appSettings.SlotTemplateListFileName;
            regexListFileTextBox.Text = _appSettings.RegexListFileName;
            dragDrowSwapCheckBox.Checked = _appSettings.DragDropSwap;
            swapOnNewFindCheckBox.Checked = _appSettings.DragDropSwap && _appSettings.SwapOnNewFind;
            swapOnNewFindCheckBox.Enabled = _appSettings.DragDropSwap;
            overlayClosestCheckBox.Checked = _appSettings.DragDropSwap && _appSettings.OverlayClosest;
            overlayClosestCheckBox.Enabled = _appSettings.DragDropSwap;
            hotkeySwapCheckBox.Checked = _appSettings.HotkeySwap;
            updateFrequencyTextBox.Text = _appSettings.UpdateFrequency.ToString();
            loggingComboBox.SelectedItem = _appSettings.LogLevel;
            minToTrayCheckBox.Checked = _appSettings.MinToTray;
            groupHotkeysCheckBox.Checked = _appSettings.GroupHotkeys;
            rippleForward.Checked = _appSettings.RippleForward;
            rippleHotkey.Hotkey = _appSettings.RippleHotkey;
            rippleHotkey.Enabled = !rippleForward.Checked;
            pauseHotkey.Hotkey = _appSettings.PauseHotkey;
            orTitleMatching.Checked = _appSettings.OrTitleMatching;
            breadthFirstFill.Checked = _appSettings.BreadthFirstFreeSlot;
        }

        private void updateFrequencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (!RegexValidation.isNum.IsMatch(updateFrequencyTextBox.Text))
            {
                toolTip.Show(Resources.intValidationFail, updateFrequencyTextBox, TOOL_TIP_TIME);
                e.Cancel = true;
            }
        }

        private void regexListBrowseButton_Click(object sender, EventArgs e)
        {
            browseButton(regexListFileTextBox, Resources.regexListLoadDialogTitle);
        }

        private void slotTemplateBrowseButton_Click(object sender, EventArgs e)
        {
            browseButton(slotTemplateFileTextBox, Resources.slotTemplateListLoadDialogTitle);
        }

        private void browseButton(TextBox tBox, string title)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.DefaultExt = Resources.FileDefaultExt;
            openDialog.Filter = Resources.FileFilter;
            int lastSlashPos = tBox.Text.LastIndexOf(@"\");
            if (lastSlashPos == -1)
            {
                //value is just a file nane
                openDialog.InitialDirectory = Environment.CurrentDirectory;
                openDialog.FileName = tBox.Text;
            }
            else
            {
                openDialog.InitialDirectory = tBox.Text.Substring(0, lastSlashPos);
                openDialog.FileName = tBox.Text.Substring(lastSlashPos + 1);
            }
            openDialog.Title = title;

            DialogResult result = openDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                tBox.Text = openDialog.FileName;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (regexListFileTextBox.Text.Equals(""))
            {
                Program.msgBoxShow(this, "The regex list path is empty. Browse to regex list you wish to use.");
                return;
            }
            if (slotTemplateFileTextBox.Text.Equals(""))
            {
                Program.msgBoxShow(this, "The slot template list path is empty. Browse to slot template list you wish to use.");
                return;
            }
            _appSettings.RegexListFileName = regexListFileTextBox.Text;
            _appSettings.SlotTemplateListFileName = slotTemplateFileTextBox.Text;
            _appSettings.DragDropSwap = dragDrowSwapCheckBox.Checked;
            _appSettings.SwapOnNewFind = dragDrowSwapCheckBox.Checked && swapOnNewFindCheckBox.Checked;
            _appSettings.OverlayClosest = dragDrowSwapCheckBox.Checked && overlayClosestCheckBox.Checked;
            _appSettings.HotkeySwap = hotkeySwapCheckBox.Checked;
            _appSettings.UpdateFrequency = Convert.ToInt32(updateFrequencyTextBox.Text);
            _appSettings.LogLevel = (LoggingLevel)loggingComboBox.SelectedItem;
            _appSettings.MinToTray = minToTrayCheckBox.Checked;
            _appSettings.GroupHotkeys = groupHotkeysCheckBox.Checked;
            _appSettings.RippleForward = rippleForward.Checked;
            _appSettings.PauseHotkey = pauseHotkey.Hotkey;
            _appSettings.RippleHotkey = rippleHotkey.Hotkey;
            _appSettings.OrTitleMatching = orTitleMatching.Checked;
            _appSettings.BreadthFirstFreeSlot = breadthFirstFill.Checked;
            if (!_parent.validateHotkeys(_parent.Config, _appSettings, false, true))
            {
                Program.msgBoxShow(this, "Either the pause or ripple forward hotkey is used by another item. Choose a different key combination for the pause feature.");
                return;
            }
            Logger.Debug("Set new appSettings: {0}", _appSettings.ToString());
            _parent.Settings = _appSettings;
            _parent.setLoggingLevel();
            _parent.configureForPausedState();
            _parent.clearAndInitializeGeneralHotkeys();
            _parent.setRefreshTimer();
            this.Close();
        }

        private void OptionsGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parent.Enabled = true;
            _parent.Focus();
        }

        private void dragDrowSwapCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            swapOnNewFindCheckBox.Enabled = dragDrowSwapCheckBox.Checked;
            overlayClosestCheckBox.Enabled = dragDrowSwapCheckBox.Checked;
            swapOnNewFindCheckBox.Checked = swapOnNewFindCheckBox.Checked && dragDrowSwapCheckBox.Checked;
            overlayClosestCheckBox.Checked = overlayClosestCheckBox.Checked && dragDrowSwapCheckBox.Checked;
        }

        private void rippleForward_CheckedChanged(object sender, EventArgs e)
        {
            rippleHotkey.Enabled = !rippleForward.Checked;
        }
    }
}