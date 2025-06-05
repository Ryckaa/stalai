using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics.CodeAnalysis;

//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Manager
{
    [SuppressMessage("Microsoft.Naming", "CA1710")]
    [XmlRoot("RegExList")]
    public class RegexList : List<PMRegex>, IFileNotFound, IDeepCloneable<RegexList>
    {
        [SuppressMessage("Microsoft.Naming", "CA1707")]
        [SuppressMessage("Microsoft.Naming", "CA1709")]
        public static readonly Type[] REGEX_TYPES = new Type[] { typeof(TitleRegex), typeof(ClassRegex) };

        public RegexList() { }
        public RegexList(int capacity)
            : base(capacity) { }
        public RegexList(IEnumerable<PMRegex> collection)
            :base(collection) { }

        public string FileNotFoundMsg()
        {
            return Properties.Resources.regexListFileNotFound;
        }

        /// <summary>
        /// Checks each expression for for a match to the given string
        /// </summary>
        /// <param name="input">String to match against</param>
        /// <param name="useBlank">True if blank expressions should be tested</param>
        /// <returns>True if an expression matches the input</returns>
        public bool IsMatch(string input, bool useBlank)
        {
            foreach (PMRegex regex in this)
            {
                if (!useBlank && regex.Match == "")
                {
                    //skip if the match string is blank
                    continue;
                }
                if (regex.GetRegex().IsMatch(input))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsMatch(string input)
        {
            return IsMatch(input, false);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Count == 0)
            {
                sb.Append("RegexList:Empty");
            }
            else
            {
                foreach (PMRegex regex in this)
                {
                    sb.AppendLine(regex.ToString());
                }
                sb.Remove(sb.Length - 2, 2);//remove last new line
            }
            return sb.ToString();
        }

        public bool Equals(RegexList obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (this.Count != obj.Count)
            {
                return false;
            }
            for (int i = 0; i < this.Count; i++)
            {
                if (!this[i].Equals(obj[i]))
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
            RegexList wg = obj as RegexList;
            return Equals(wg);
        }

        public RegexList DeepClone()
        {
            RegexList titles = new RegexList();
            foreach (PMRegex regex in this)
            {
                titles.Add(regex.DeepClone());
            }
            return titles;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static void Save(RegexList regexList, string fileName)
        {
            XmlReadWrite<RegexList>.Save(regexList, fileName, REGEX_TYPES);
        }

        public static void Save(TitleList titles, ClassList classes, string fileName)
        {
            RegexList combined = new RegexList(titles.Count + classes.Count);
            foreach (PMRegex regex in titles)
            {
                combined.Add(regex);
            }
            foreach (PMRegex regex in classes)
            {
                combined.Add(regex);
            }
            combined.Sort();
            Save(combined, fileName);
        }

        public static void Load(string fileName, out RegexList regexList)
        {
            regexList = XmlReadWrite<RegexList>.Load(fileName, REGEX_TYPES);
        }

        public static void Load(string fileName, out TitleList titles, out ClassList classes)
        {
            RegexList combined = XmlReadWrite<RegexList>.Load(fileName, REGEX_TYPES);
            RegexSorter(combined, out titles, out classes);
        }

        public static void RegexSorter(RegexList init, out TitleList titles, out ClassList classes)
        {
            titles = new TitleList();
            classes = new ClassList();
            foreach (PMRegex regex in init)
            {
                if (regex is ClassRegex)
                {
                    classes.Add((ClassRegex)regex);
                }
                else if (regex is TitleRegex)
                {
                    titles.Add((TitleRegex)regex);
                }
                else
                {
                    //Backwards campatibility: default to TitleRegex
                    titles.Add(new TitleRegex(regex.Title, regex.Match, regex.CaseSensitive));
                }
            }
        }
    }
}