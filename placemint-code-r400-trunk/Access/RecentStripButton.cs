using System;
using System.Windows.Forms;

namespace PlaceMint.Access
{
    class RecentStripButton : ToolStripButton
    {
        private string _file;
        public RecentStripButton(string file, EventHandler handle)
        {
            _file = file;
            this.Text = file.Substring(_file.LastIndexOf("\\") + 1);
            this.Click += handle;
        }

        public string File
        {
            get { return _file; }
        }
    }
}
