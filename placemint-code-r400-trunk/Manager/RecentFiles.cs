using System.Collections.Generic;
using System.Xml.Serialization;

namespace PlaceMint.Manager
{
    /// <summary>
    /// A stack of string that removes items from the bottom when the max has been reached
    /// </summary>
    public class RecentFiles : IDeepCloneable<RecentFiles>
    {
        private const int FILE_MAX = 4;
        [XmlArray]
        private List<string> _files;

        /// <summary>
        /// Constructor
        /// </summary>
        public RecentFiles()
        {
            _files = new List<string>(FILE_MAX);
        }

        /// <summary>
        /// Add a new file. A repeated file is simply moved to the top
        /// </summary>
        /// <param name="file">File to add</param>
        public void Add(string file)
        {
            if (_files.Contains(file))
            {
                _files.Remove(file);
            }
            _files.Insert(0, file);
            if (_files.Count > FILE_MAX)
            {
                _files.RemoveAt(FILE_MAX);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RecentFiles DeepClone()
        {
            RecentFiles copy = new RecentFiles();
            for (int i = _files.Count - 1; i >= 0; i--)
            {
                copy.Add(_files[i]);
            }
            return copy;
        }

        /// <summary>
        /// Number of files stored
        /// </summary>
        public int Count
        {
            get { return _files.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Files
        {
            get { return _files; }
            set { _files = value; }
        }
    }
}
