using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace PlaceMint.Manager
{
    using PMException;
    
    /// <summary>
    /// Holds a named regular expression.
    /// </summary>
    [Serializable]
    public class PMRegex : IComparable<PMRegex>, ITitle, IDeepCloneable<PMRegex>
    {
        private string _title;
        private string _match;
        private bool _caseSensitive;

        /// <summary>
        /// Default constructor
        /// </summary>
        protected PMRegex()
            : this("", "", false) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">Display name</param>
        /// <param name="match">Regex String</param>
        /// <param name="caseSensitive">Regex option</param>
        protected PMRegex(string title, string match, bool caseSensitive)
        {
            _title = title;
            _match = match;
            _caseSensitive = caseSensitive;
        }

        /// <summary>
        /// Get the title
        /// </summary>
        [XmlElement("title")]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// Get the match string
        /// </summary>
        [XmlElement("match")]
        public string Match
        {
            get { return _match; }
            set { _match = value; }
        }

        /// <summary>
        /// Get the regex options
        /// </summary>
        [XmlElement("case-sensitive")]
        public bool CaseSensitive
        {
            get { return _caseSensitive; }
            set { _caseSensitive = value; }
        }

        /// <summary>
        /// Returns a Regex object using PMRegex.Match
        /// </summary>
        public Regex GetRegex()
        {
            return new Regex(Match, (_caseSensitive) ? RegexOptions.None : RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual T DeepClone<T>() where T : PMRegex, new()
        {
            T regex = new T();
            copyTo(regex);
            return regex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual PMRegex DeepClone()
        {
            PMRegex regex = new PMRegex();
            copyTo(regex);
            return regex;
        }

        private void copyTo(PMRegex regex)
        {
            regex._title = this.Title;
            regex._match = this.Match;
            regex._caseSensitive = this.CaseSensitive;
        }


        /// <summary>
        /// Determines if two PMRegex objects are the same
        /// </summary>
        /// <param name="other">object to test</param>
        public bool Equals(PMRegex other)
        {
            if (other == null)
            {
                return false;
            }
            if ((Object)this == (Object)other) //identity compare
            {
                return true;
            }
            return (this.Title.Equals(other.Title) && this.Match.Equals(other.Match));
        }

        /// <summary>
        /// Determines if two objects objects are the same
        /// </summary>
        /// <param name="obj"></param>
        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            PMRegex rgp = obj as PMRegex;
            return Equals(rgp);
        }

        /// <summary>
        /// 
        /// </summary>
        public int CompareTo(PMRegex other)
        {
            if (other.Title == this.Title)
            {
                return 0;
            }
            return String.Compare(this.Title, other.Title);
        }

        /// <summary>
        /// 
        /// </summary>
        public override int GetHashCode()
        {
            return this.Match.GetHashCode() + this.Title.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            String t = this.GetType().ToString();
            sb.Append(t.Substring(t.LastIndexOf(".")+1));
            sb.Append(":\n_title|");
            sb.Append(_title);
            sb.Append("|\n_match|");
            sb.Append(_match);
            sb.Append("|\n_caseSensitive|");
            sb.Append(_caseSensitive);
            return sb.ToString();
        }

        /// <summary>
        /// Checks the pattern for correct regular expression syntax
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="throwEx"></param>
        /// <returns></returns>
        public static bool VerifyRegex(string pattern, bool throwEx)
        {
            bool isValid = false;
            if (pattern != null)
            {
                try
                {
                    Regex.Match("", pattern);
                    isValid = true;
                }
                catch (ArgumentException e)
                {
                    if (throwEx)
                    {
                        throw new InvalidRegexException(e.Message, e);
                    }
                }
            }
            Logger.Trace("Verify Regex \"{0}\"\n  {1}", pattern, (isValid) ? "valid" : "invalid");
            return isValid;
        }
    }
}