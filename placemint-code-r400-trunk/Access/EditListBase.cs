using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

//cs1591: Missing XML comment
#pragma warning disable 1591

using PlaceMint.Manager;
using PlaceMint.Manager.PMException;
namespace PlaceMint.Access
{
    using Properties;

    /// <summary>
    /// 
    /// </summary>
    public partial class EditListBase : Form
    {
        protected AppSettings _appSettings;
        protected ConfigureGUI _parent;
        protected const string ADD_NEW = "<Add New>";
        /// <summary>
        /// 
        /// </summary>
        public EditListBase()
        {
            InitializeComponent();
            this.Icon = Resources.icon;
        }
        public EditListBase(AppSettings appS, ConfigureGUI parent)
        {
            _appSettings = appS;
            _parent = parent;
            _parent.Enabled = false;
            this.Owner = _parent;
            InitializeComponent();
            this.Icon = Resources.icon;
        }

        protected virtual void mainListBox_SelectedIndexChanged(object sender, EventArgs e) { }
        protected virtual void saveButton_Click(object sender, EventArgs e) { }
        protected virtual int getInsertIndex(string title) { return -1; }
        protected virtual void disableValidating() { }
        protected virtual void enableValidating() { }
        protected virtual void titleTextBox_Validating(object sender, CancelEventArgs e) { }
        protected virtual void deleteItem(int index) { }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            int index = mainListBox.SelectedIndex;
            if (index != -1)
            {
                disableSelectionChange();
                mainListBox.Items.RemoveAt(index);
                deleteItem(index);
                enableSelectionChange();
                //if more than ADD_NEW
                if (mainListBox.Items.Count > 1)
                {
                    if (index > 0)
                    {
                        index--;
                    }
                    mainListBox.SelectedIndex = index;
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected void EditListBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parent.Enabled = true;
            _parent.Focus();
        }

        protected void disableSelectionChange()
        {
            mainListBox.SelectedIndexChanged -= mainListBox_SelectedIndexChanged;
        }
        protected void enableSelectionChange()
        {
            mainListBox.SelectedIndexChanged += mainListBox_SelectedIndexChanged;
        }

        /// <summary>
        /// Checks to see if the title is already taken in the list. Sets new name if not already used.
        /// </summary>
        /// <typeparam name="T">List Type</typeparam>
        /// <typeparam name="U">Base object of the list</typeparam>
        /// <param name="list"></param>
        /// <returns>False if name is already used.</returns>
        protected bool titleTextBoxValidation<T, U>(T list)
            where T : List<U>
            where U : class, ITitle
        {
            if (titleTextBox.Text != mainListBox.SelectedItem.ToString())
            {
                if (list.Exists(TitleMatch<U>.Match(titleTextBox.Text)))
                {
                    return false;
                }
                disableSelectionChange();
                list[mainListBox.SelectedIndex].Title = titleTextBox.Text;
                list.Sort();
                mainListBox.Items.RemoveAt(mainListBox.SelectedIndex);
                int index = getInsertIndex(titleTextBox.Text);
                mainListBox.Items.Insert(index, titleTextBox.Text);
                enableSelectionChange();
                mainListBox.SelectedIndex = index;
            }
            return true;
        }

        private void detailsGrabberButton_Click(object sender, EventArgs e)
        {
            _parent.showDetailsGrabber();
        }
    }
}