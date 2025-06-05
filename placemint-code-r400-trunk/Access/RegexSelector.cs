using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using PlaceMint.Manager;
namespace PlaceMint.Access
{
    using Properties;

    /// <summary>
    /// Control that lets a user select expressions
    /// </summary>
    public partial class RegexSelector : UserControl
    {
        /// <summary>
        /// A delegate that gets the corresponding RegexList for the activeGroup
        /// </summary>
        /// <returns></returns>
        public delegate RegexList ActiveGroupRegexListDelegate();
        
        private RegexList _regexes;
        private ActiveGroupRegexListDelegate _activeGroupRegexList;
        private int _prevRegexCheckedListIndex = -1;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public RegexSelector()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public ActiveGroupRegexListDelegate ActiveGroupRegexList
        {
            get { return _activeGroupRegexList; }
            set { _activeGroupRegexList = value; }
        }
        
        private void regexCheckedList_SelectedIndexChanged(object sender, EventArgs e)
        {
            regexTextBox.Text = _regexes[regexCheckedList.SelectedIndex].Match;
        }

        private void regexCheckedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            RegexList groupList = _activeGroupRegexList();
            if (e.NewValue == CheckState.Checked)
            {
                groupList.Add(_regexes[e.Index]);
            }
            else
            {
                groupList.RemoveAt(groupList.FindIndex(TitleMatch<PMRegex>.Match(_regexes[e.Index].Title)));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void setRegexList(RegexList regexes)
        {
            _regexes = regexes;
            //regexCheckedList.BeginUpdate();
            regexCheckedList.Items.Clear();
            RegexList validRegexes = new RegexList();
            List<string> validTitles = new List<string>();
            foreach (PMRegex regex in regexes)
            {
                if (PMRegex.VerifyRegex(regex.Match, false))
                {
                    validTitles.Add(regex.Title);
                    validRegexes.Add(regex);
                }
            }
            regexCheckedList.Items.AddRange(validTitles.ToArray());
            if (validRegexes.Count != _regexes.Count)
            {
                int dif = regexes.Count - _regexes.Count;
                Program.msgBoxShow(this, string.Format(Resources.configInvalidRegexFormat, (dif == 1) ? "One" : "Some",
                    (dif == 1) ? "It was" : "They were"));
                _regexes = validRegexes;
            }
        }

        /// <summary>
        /// Sets which items should be checked in the regex list box
        /// </summary>
        public void checkRegexList()
        {
            checkRegexList(false);
        }

        /// <summary>
        /// Sets which items should be checked in the regex list box
        /// </summary>
        public void checkRegexList(bool selectFirst)
        {
            RegexList groupList = _activeGroupRegexList();
            disableRegexCheckedListItemCheck();
            regexCheckedList.BeginUpdate();
            for (int i = 0; i < regexCheckedList.Items.Count; i++)
            {
                regexCheckedList.SetItemChecked(i, groupList.Exists(TitleMatch<PMRegex>.Match((string)regexCheckedList.Items[i])));
            }
            regexCheckedList.EndUpdate();
            enableRegexCheckedListItemCheck();

            if (regexCheckedList.Items.Count == 0)
            {
                return;
            }
            if (selectFirst)
            {
                regexCheckedList.SelectedIndex = 0;
            }
            else if (_prevRegexCheckedListIndex != -1)
            {
                //revert to previous selection after regex list update
                if (_prevRegexCheckedListIndex < regexCheckedList.Items.Count)
                {
                    regexCheckedList.SelectedIndex = _prevRegexCheckedListIndex;
                }
                else
                {
                    regexCheckedList.SelectedIndex = regexCheckedList.Items.Count - 1;
                }
                _prevRegexCheckedListIndex = -1;
            }
        }

        /// <summary>
        /// Confirm each Regex has a valid title. If not add to the list.
        /// </summary>
        public bool confirmRegex(PMRegex regex)
        {
            PMRegex listed = _regexes.Find(TitleMatch<PMRegex>.Match(regex.Title));
            if (listed == null)
            {
                _regexes.Add(regex);
                return true;
            }
            else if (listed.Match != regex.Match)
            {
                regex.Match = listed.Match;
            }
            return false;
        }

        /// <summary>
        /// Confirm each Regex has a valid title. If not add to the list.
        /// </summary>
        public RegexList getSortedList()
        {
            _regexes.Sort();
            return _regexes;
        }

        /// <summary>
        /// Locally stores the selected index so it can be used later
        /// </summary>
        public void storeRegexCheckedListIndex()
        {
            _prevRegexCheckedListIndex = regexCheckedList.SelectedIndex;
        }

        private void disableRegexCheckedListItemCheck()
        {
            regexCheckedList.ItemCheck -= regexCheckedList_ItemCheck;
        }
        private void enableRegexCheckedListItemCheck()
        {
            regexCheckedList.ItemCheck += regexCheckedList_ItemCheck;
        }
    }
}
