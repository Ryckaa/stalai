using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Manager
{
    using PMException;
    using Properties;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Collection of Window Groups.
    /// </summary>
    [XmlRoot("Configuration")]
    public class GroupConfiguration : IDeepCloneable<GroupConfiguration>, ISwap, IFileNotFound
    {
        private List<WindowGroup> _groups = new List<WindowGroup>();

        public GroupConfiguration() { }

        [XmlArray("groups")]
        public List<WindowGroup> Groups
        {
            get { return _groups; }
            set { _groups = value; }
        }

        /// <summary>
        /// Empties any slot currently assigned to a window
        /// </summary>
        public void ClearSlots()
        {
            for (int i = 0; i < _groups.Count; i++)
            {
                _groups[i].IteratorReset();
                while (_groups[i].IteratorHasMore())
                {
                    if (!_groups[i].IteratorCurrent.IsEmpty)
                    {
                        _groups[i].IteratorCurrent = new Slot(_groups[i].IteratorCurrent.Shape, _groups[i].IteratorCurrent.Hotkey,
                            _groups[i].IteratorCurrent.Size);
                    }
                    _groups[i].IteratorNext();
                }
                Logger.Debug("Clearing windows because of ClearSlots.");
                _groups[i].Windows.Clear();
            }
        }

        /// <summary>
        /// Search the configuration for duplicate expression 
        /// </summary>
        /// <param name="remove">True if duplicates should be removed</param>
        /// <returns>List containing names of groups with duplicates</returns>
        public List<String> FindDuplicates(bool remove)
        {
            List<WindowGroupExpression> values = new List<WindowGroupExpression>();
            List<String> duplicates = new List<String>();
            foreach (WindowGroup group in _groups)
            {
                if (group.WinClassRegexList.Count != 0 || group.WinClassRegexList.Count != 0)
                {
                    WindowGroupExpression expression = new WindowGroupExpression(group.WinTitleRegexList, group.WinClassRegexList);
                    if (values.Contains(expression))
                    {
                        duplicates.Add(group.WindowGroupTitle);
                        if (remove)
                        {
                            group.WinTitleRegexList.Clear();
                            group.WinClassRegexList.Clear();
                        }
                    }
                    else
                    {
                        values.Add(expression);
                    }
                }
            }
            return duplicates;
        }

        public bool Equals(GroupConfiguration config)
        {
            if (config == null)
            {
                return false;
            }
            if ((Object)this == (Object)config) //identity compare
            {
                return true;
            }
            if (_groups.Count != config.Groups.Count)
            {
                return false;
            }
            for (int i = 0; i < _groups.Count; i++)
            {
                if (!_groups[i].Equals(config.Groups[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            GroupConfiguration config =  obj as GroupConfiguration;
            return Equals(config);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool RefreshGroups(AppSettings appSetting, IDrawOverlay drawOverlay)
        {
            Logger.Trace("RefreshGroups");
            bool result = false;
            foreach (WindowGroup group in _groups)
            {
                Logger.Trace("Group \"{0}\"", group.WindowGroupTitle);
                if (group.Refresh(appSetting, drawOverlay))
                {
                    Logger.Debug("Window in group changed");
                    result = true;
                }
            }
            return result;
        }

        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public bool WindowExists(IntPtr hwnd)
        {
            Logger.Trace(string.Format("Check if {0} is already handled", hwnd));
            foreach (WindowGroup group in _groups)
            {
                if (group.Windows.ContainsKey(hwnd))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// String representation of GroupConfiguration
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("GroupConfiguration:");
            if (_groups.Count == 0)
            {
                sb.Append("Empty configuration");
            }
            else
            {
                foreach (WindowGroup group in _groups)
                {
                    sb.AppendLine(group.ToString());
                }
                sb.Remove(sb.Length - 2, 2);//remove last new line
            }
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>New object with all of the same values.</returns>
        public GroupConfiguration DeepClone()
        {
            GroupConfiguration config = new GroupConfiguration();
            foreach (WindowGroup group in _groups)
            {
                config.Groups.Add(group.DeepClone());
            }
            return config;
        }
        /// <summary>
        /// Swap two Window Groups
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <exception cref="OutOfRangeException">Thrown if the index is out of range.</exception>
        public void SwapItems(int index1, int index2)
        {
            if (index1 < 0 && index1 >= _groups.Count && index2 < 0 && index2 >= _groups.Count)
            {
                throw new OutOfRangeException(Resources.illegalGroupsIndex);
            }
            WindowGroup wg = _groups[index1];
            _groups[index1] = _groups[index2];
            _groups[index2] = wg;
        }

        public string FileNotFoundMsg()
        {
            return Resources.configFileNotFound;
        }

        public static void Save(GroupConfiguration config, string fileName)
        {
            XmlReadWrite<GroupConfiguration>.Save(config, fileName, RegexList.REGEX_TYPES);
        }

        public static void Load(string fileName, out GroupConfiguration config)
        {
            config = XmlReadWrite<GroupConfiguration>.Load(fileName, RegexList.REGEX_TYPES);
            TitleList titles2titles, classes2titles;
            ClassList classes2classes, titles2classes;
            foreach (WindowGroup group in config.Groups)
            {
                RegexList.RegexSorter(group.WinTitleRegexList, out titles2titles, out titles2classes);
                RegexList.RegexSorter(group.WinClassRegexList, out classes2titles, out classes2classes);
                group.WinTitleRegexList = titles2titles;
                group.WinTitleRegexList.AddRange(classes2titles);
                group.WinClassRegexList = classes2classes;
                group.WinClassRegexList.AddRange(titles2classes);
            }
        }
    }
}