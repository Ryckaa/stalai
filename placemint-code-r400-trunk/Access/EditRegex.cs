using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;

using PlaceMint.Manager;
using PlaceMint.Manager.PMException;
namespace PlaceMint.Access
{
    using Properties;

    /// <summary>
    /// 
    /// </summary>
    public partial class EditRegex : EditListBase
    {
        //TODO: add radio for class vs title
        private RegexList _regexList = new RegexList();
        private readonly TitleRegex NEW_PMRegex = new TitleRegex("New Title", "New Regular Expression", false);
        /// <summary>
        /// 
        /// </summary>
        public EditRegex(AppSettings appS, ConfigureGUI parent)
           : base(appS, parent)
        {
            InitializeComponent();
            enableSelectionChange();

            //read in Regex list
            try
            {
                RegexList.Load(_appSettings.RegexListFileName, out _regexList);
                _regexList.Sort();
            }
            catch (PlaceMintException e)
            {
                string msg = null;
                if (e is PMFileNotFoundException)
                {
                    msg = e.Message;
                }
                else if (e is PMPathTooLongException)
                {
                    msg = Resources.regexListPathTooLong;
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

                if (msg == null)
                {
                    throw;
                }
                else
                {
                    Program.msgBoxShow(this, msg);
                }

                _regexList = new RegexList();
                e.Log("regex list");
            }

            foreach (PMRegex regex in _regexList)
            {
                mainListBox.Items.Add(regex.Title);
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
                Logger.Debug("Add new Regex string");
                disableSelectionChange();
                int index = getInsertIndex(NEW_PMRegex.Title);
                if (!_regexList.Exists(TitleMatch<PMRegex>.Match(NEW_PMRegex.Title)))
                {
                    Logger.Debug("NEW_PMRegex.Title doesn't exist, create it.");
                    _regexList.Add(NEW_PMRegex.DeepClone<TitleRegex>());
                    _regexList.Sort();
                    mainListBox.Items.Insert(index, NEW_PMRegex.Title);
                }
                mainListBox.SelectedIndex = index;
                enableSelectionChange();
            }
            disableValidating();
            disableRegexTypeChange();
            PMRegex regex = _regexList[mainListBox.SelectedIndex];
            titleTextBox.Text = regex.Title;
            regexTextBox.Text = regex.Match;
            classRadio.Checked = (regex is ClassRegex);
            titleRadio.Checked = (regex is TitleRegex);
            caseSensitiveCheckBox.Checked = _regexList[mainListBox.SelectedIndex].CaseSensitive;
            enableRegexTypeChange();
            enableValidating();
        }

        /// <summary>
        /// Handeling for when the saveButton is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void saveButton_Click(object sender, EventArgs e)
        {
            RegexList.Save(_regexList, _appSettings.RegexListFileName);
            _parent.setRegexList();
            _parent.confirmRegexList();
            _parent.checkRegexList();
            this.Close();
        }

        private void EditRegex_FormClosed(object sender, FormClosedEventArgs e)
        {
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
            foreach(PMRegex regex in _regexList)
            {
                if (string.Compare(title, regex.Title) <= 0)
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
            regexTextBox.Validating -= regexTextBox_Validating;
            caseSensitiveCheckBox.CheckedChanged -= caseSensitiveCheckBox_CheckedChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void enableValidating()
        {
            titleTextBox.Validating += titleTextBox_Validating;
            regexTextBox.Validating += regexTextBox_Validating;
            caseSensitiveCheckBox.CheckedChanged += caseSensitiveCheckBox_CheckedChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void disableRegexTypeChange()
        {
            titleRadio.CheckedChanged -= titleRadio_CheckedChanged;
            classRadio.CheckedChanged -= classRadio_CheckedChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void enableRegexTypeChange()
        {
            titleRadio.CheckedChanged += titleRadio_CheckedChanged;
            classRadio.CheckedChanged += classRadio_CheckedChanged;
        }

        /// <summary>
        /// Validation for titleBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void titleTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (!titleTextBoxValidation<RegexList, PMRegex>(_regexList))
            {
                Program.msgBoxShow(this, Resources.regexTitleInUse);
                e.Cancel = true;
            }
        }

        private void regexTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                PMRegex.VerifyRegex(regexTextBox.Text, true);
            }
            catch (Exception ex)
            {
                Program.msgBoxShow(this, string.Format(Resources.invalidRegexFormat, ex.Message));
                e.Cancel = true;
                return;
            }
            _regexList[mainListBox.SelectedIndex].Match = regexTextBox.Text;
        }

        private void caseSensitiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _regexList[mainListBox.SelectedIndex].CaseSensitive = caseSensitiveCheckBox.Checked;
        }

        /// <summary>
        /// Remove the item from the list
        /// </summary>
        /// <param name="index"></param>
        protected override void deleteItem(int index)
        {
            _regexList.RemoveAt(index);
        }

        private void titleRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (titleRadio.Checked)
            {
                PMRegex regex = _regexList[mainListBox.SelectedIndex];
                _regexList[mainListBox.SelectedIndex] = regex.DeepClone<TitleRegex>();
            }
        }

        private void classRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (classRadio.Checked)
            {
                PMRegex regex = _regexList[mainListBox.SelectedIndex];
                _regexList[mainListBox.SelectedIndex] = regex.DeepClone<ClassRegex>();
            }
        }
    }
}
