//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Manager
{
    /// <summary>
    /// Object to hold the title and class of a window.
    /// </summary>
    public class WindowInfo: IDeepCloneable<WindowInfo>
    {
        private string _class;
        private string _title;

        public WindowInfo()
            : this("","") { }

        public WindowInfo(string theClass, string theTitle)
        {
            _class = theClass;
            _title = theTitle;
        }

        public string TheClass
        {
            get { return _class; }
            set { _class = value; }
        }

        public string TheTitle
        {
            get { return _title; }
            set { _title = value; }
        }

        public override string ToString()
        {
            return string.Format("Title|{0}|Class|{1}|", _title, _class);
        }

        public WindowInfo DeepClone()
        {
            WindowInfo wi = new WindowInfo();
            wi.TheClass = this.TheClass;
            wi.TheTitle = this.TheTitle;
            return wi;
        }
    }
}