using System;
using System.Windows.Forms;
using System.ComponentModel;

//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Access
{
    using PlaceMint.Manager;

    public partial class HotkeyControlCheck : UserControl
    {
        private Hotkey _hotkey = new Hotkey();

        public delegate void HotkeyChangeHandler();
        public event HotkeyChangeHandler HotkeyChange;
        protected void OnHotkeyChange()
        {
            if (HotkeyChange != null)
            {
                HotkeyChange();
            }
        }

        public HotkeyControlCheck()
        {
            InitializeComponent();
        }

        public Hotkey Hotkey
        {
            get { return _hotkey.DeepClone(); }
            set
            {
                _hotkey = value.DeepClone();
                refreshDisplay();
            }
        }

        private void resetHotkey()
        {
            _hotkey = new Hotkey();
            clearCheckBoxes();
        }

        private void clearCheckBoxes()
        {
            disableCheckedChange();
            altCheckBox.Checked = false;
            controlCheckBox.Checked = false;
            shiftCheckBox.Checked = false;
            winCheckBox.Checked = false;
            enableCheckedChange();
        }

        private void refreshDisplay()
        {
            keyTextBox.Text = _hotkey.Key.ToString();
            if (_hotkey.IsModified)
            {
                disableCheckedChange();
                altCheckBox.Checked = _hotkey.HasAlt;
                controlCheckBox.Checked = _hotkey.HasControl;
                shiftCheckBox.Checked = _hotkey.HasShift;
                winCheckBox.Checked = _hotkey.HasWin;
                enableCheckedChange();
            }
            else
            {
                clearCheckBoxes();
            }
        }

        private void disableCheckedChange()
        {
            altCheckBox.CheckedChanged -= checkBox_CheckedChanged;
            controlCheckBox.CheckedChanged -= checkBox_CheckedChanged;
            shiftCheckBox.CheckedChanged -= checkBox_CheckedChanged;
            winCheckBox.CheckedChanged -= checkBox_CheckedChanged;
        }

        private void enableCheckedChange()
        {
            altCheckBox.CheckedChanged += checkBox_CheckedChanged;
            controlCheckBox.CheckedChanged += checkBox_CheckedChanged;
            shiftCheckBox.CheckedChanged += checkBox_CheckedChanged;
            winCheckBox.CheckedChanged += checkBox_CheckedChanged;
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            Hotkey tempHotkey = _hotkey.DeepClone();

            ModifyingKeys tempMod = ModifyingKeys.None;
            if (altCheckBox.Checked)
            {
                tempMod = tempMod | ModifyingKeys.Alt;
            }
            if (controlCheckBox.Checked)
            {
                tempMod = tempMod | ModifyingKeys.Control;
            }
            if (shiftCheckBox.Checked)
            {
                tempMod = tempMod | ModifyingKeys.Shift;
            }
            if (winCheckBox.Checked)
            {
                tempMod = tempMod | ModifyingKeys.Win;
            }
            _hotkey.ModKeys = tempMod;
            if (ValidatingPassed())
            {
                OnHotkeyChange();
            }
            else
            {
                //revert back to value before KeyDown
                _hotkey = tempHotkey;
                refreshDisplay();
            }
        }

        /// <summary>
        /// Fires when a key is pushed down. Here, we'll want to update the text in the box
        /// to notify the user what combination is currently pressed.
        /// </summary>
        private void keyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Hotkey tempHotkey = _hotkey.DeepClone();

            // Clear the current hotkey
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                if (e.Control)
                {
                    _hotkey.Key = e.KeyCode;
                }
                else
                {
                    resetHotkey();
                }
                e.Handled = true;
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.ControlKey:
                    case Keys.ShiftKey:
                    case Keys.Alt:
                    case Keys.Menu: //Alt
                    case Keys.RWin:
                    case Keys.LWin:
                        _hotkey.Key = Keys.None;
                        e.Handled = true;
                        break;
                    default:
                        _hotkey.Key = e.KeyCode;
                        break;
                }
            }
            if (ValidatingPassed())
            {
                keyTextBox.Text = _hotkey.Key.ToString();
                OnHotkeyChange();
            }
            else
            {
                //revert back to value before KeyDown
                _hotkey = tempHotkey;
            }
        }

        /// <summary>
        /// Prevents the letter/sumbol entered from showing up in the TextBox
        /// </summary>
        private void keyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private bool ValidatingPassed()
        {
            CancelEventArgs e = new CancelEventArgs();
            OnValidating(e);
            return !e.Cancel;
        }
    }
}
