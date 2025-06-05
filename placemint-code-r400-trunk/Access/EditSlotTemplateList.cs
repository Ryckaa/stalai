using System;
using System.ComponentModel;
using System.Windows.Forms;

using PlaceMint.Manager;
using PlaceMint.Manager.PMException;
namespace PlaceMint.Access
{
    using Properties;
    /// <summary>
    /// 
    /// </summary>
    public partial class EditSlotTemplateList : EditListBase
    {
        private SlotTemplateList _slotTemplateList = new SlotTemplateList();
        private readonly SlotTemplate NEW_SlotTemplate = new SlotTemplate("New Title", 0, 0);
        /// <summary>
        /// 
        /// </summary>
        /// <summary>
        /// 
        /// </summary>
        public EditSlotTemplateList(AppSettings appS, ConfigureGUI parent)
           : base(appS, parent)
        {
            InitializeComponent();
            enableSelectionChange();

            //read in Slot Template list
            try
            {
                _slotTemplateList = XmlReadWrite<SlotTemplateList>.Load(_appSettings.SlotTemplateListFileName);
                _slotTemplateList.Sort();
            }
            catch (PlaceMintException e)
            {
                string msg = null;
                if (e is PMFileNotFoundException || e is PMPathTooLongException)
                {
                    msg = e.Message;
                }
                else if (e is EmptyPathException)
                {
                    msg = string.Format(Resources.emptyPathFormat, "slot template");
                }
                else if (e is InvalidXmlValueException)
                {
                    msg = string.Format(Resources.slotTemplateIllegalValueFormat, e.Message);
                }

                if (msg == null)
                {
                    throw;
                }
                else
                {
                    Program.msgBoxShow(this, msg);
                }

                _slotTemplateList = new SlotTemplateList();
                e.Log("slot template list");
            }

            foreach (SlotTemplate sTemp in _slotTemplateList)
            {
                mainListBox.Items.Add(sTemp.Title);
            }
            mainListBox.Items.Add(ADD_NEW);
            mainListBox.SelectedIndex = 0;
        }
        /// <summary>
        /// Handeling for a selection change on the mainListBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void mainListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.Debug("Remove group at {0}", mainListBox.SelectedIndex);
            if (mainListBox.SelectedItem.ToString() == ADD_NEW)
            {
                Logger.Debug("Add new SlotTemplate string");
                disableSelectionChange();
                int index = getInsertIndex(NEW_SlotTemplate.Title);
                if (!_slotTemplateList.Exists(TitleMatch<SlotTemplate>.Match(NEW_SlotTemplate.Title)))
                {
                    Logger.Debug("NEW_SlotTemplate.Title doesn't exist, create it.");
                    _slotTemplateList.Add(new SlotTemplate(NEW_SlotTemplate.Title, NEW_SlotTemplate.Width, NEW_SlotTemplate.Height));
                    _slotTemplateList.Sort();
                    mainListBox.Items.Insert(index, NEW_SlotTemplate.Title);
                }
                mainListBox.SelectedIndex = index;
                enableSelectionChange();
            }
            disableValidating();
            titleTextBox.Text = _slotTemplateList[mainListBox.SelectedIndex].Title;
            widthNumericUpDown.Value = (decimal)_slotTemplateList[mainListBox.SelectedIndex].Width;
            heightNumericUpDown.Value = (decimal)_slotTemplateList[mainListBox.SelectedIndex].Height;
            enableValidating();
        }

        /// <summary>
        /// Handeling for when the saveButton is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void saveButton_Click(object sender, EventArgs e)
        {
            XmlReadWrite<SlotTemplateList>.Save(_slotTemplateList, _appSettings.SlotTemplateListFileName);
            _parent.setSlotTemplateList();
            this.Close();
        }

        private void EditSlotTemplateList_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parent.setSlotTemplateList();
            base.EditListBase_FormClosed(sender, e);
        }

        /// <summary>
        /// Get index to instert new item at.
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        protected override int getInsertIndex(string title)
        {
            int index = 0;
            foreach (SlotTemplate sTemp in _slotTemplateList)
            {
                if (string.Compare(title, sTemp.Title) <= 0)
                {
                    break;
                }
                index++;
            }
            return index;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void disableValidating()
        {
            titleTextBox.Validating -= titleTextBox_Validating;
            widthNumericUpDown.Validating -= upDown_Validating;
            heightNumericUpDown.Validating -= upDown_Validating;
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void enableValidating()
        {
            titleTextBox.Validating += titleTextBox_Validating;
            widthNumericUpDown.Validating += upDown_Validating;
            heightNumericUpDown.Validating += upDown_Validating;
        }

        private void upDown_Validating(object sender, CancelEventArgs e)
        {
            NumericUpDown upDown = sender as NumericUpDown;
            if (upDown == null)
            {
                throw new IllegalSenderException(String.Format(Resources.illegalSenderFormat,
                    "upDown_Validating", "a NumericUpDown"));
            }
            try
            {
                int value = Convert.ToInt32(upDown.Value);
            }
            catch (OverflowException)
            {
                int boundary = 0;
                string direction = "";
                if (upDown.Value < 0)
                {
                    upDown.Value = (decimal)Int32.MinValue;
                    boundary = Int32.MinValue;
                    direction = "smaller";
                }
                else
                {
                    upDown.Value = (decimal)Int32.MaxValue;
                    boundary = Int32.MaxValue;
                    direction = "larger";
                }
                Program.msgBoxShow(this, string.Format(Resources.intSizeFailFormat,
                    direction, boundary));
                e.Cancel = true;
            }
        }

        private void widthNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _slotTemplateList[mainListBox.SelectedIndex].Width = Convert.ToInt32(widthNumericUpDown.Value);
        }

        private void heightNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _slotTemplateList[mainListBox.SelectedIndex].Height = Convert.ToInt32(heightNumericUpDown.Value);
        }

        /// <summary>
        /// Handeling for titleTextBox validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void titleTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (!titleTextBoxValidation<SlotTemplateList, SlotTemplate>(_slotTemplateList))
            {
                Program.msgBoxShow(this, Resources.slotTemplateTitleInUse);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Remove the item from the list
        /// </summary>
        /// <param name="index"></param>
        protected override void deleteItem(int index)
        {
            _slotTemplateList.RemoveAt(index);
        }
    }
}

