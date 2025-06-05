using System.Collections.Generic;
using System.Xml.Serialization;
using System.Diagnostics.CodeAnalysis;

//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Manager
{
    [SuppressMessage("Microsoft.Naming", "CA1710")]
    [XmlRoot("RegExList")]
    public class TitleList : RegexList, IDeepCloneable<TitleList>
    {
        public TitleList() { }
        public TitleList(int capacity)
            : base(capacity) { }
        public TitleList(IEnumerable<TitleRegex> collection)
            :base((IEnumerable<PMRegex>)collection) { }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            TitleList wg = obj as TitleList;
            return Equals(wg);
        }

        public new TitleList DeepClone()
        {
            TitleList titles = new TitleList();
            foreach (TitleRegex regex in this)
            {
                titles.Add(regex.DeepClone<TitleRegex>());
            }
            return titles;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}