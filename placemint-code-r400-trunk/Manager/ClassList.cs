using System.Collections.Generic;
using System.Xml.Serialization;
using System.Diagnostics.CodeAnalysis;

//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Manager
{
    [SuppressMessage("Microsoft.Naming", "CA1710")]
    [XmlRoot("RegExList")]
    public class ClassList : RegexList, IDeepCloneable<ClassList>
    {
        public ClassList() { }
        public ClassList(int capacity)
            : base(capacity) { }
        public ClassList(IEnumerable<ClassRegex> collection)
            : base((IEnumerable<PMRegex>)collection) { }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            ClassList wg = obj as ClassList;
            return Equals(wg);
        }

        public new ClassList DeepClone()
        {
            ClassList titles = new ClassList();
            foreach (ClassRegex regex in this)
            {
                titles.Add(regex.DeepClone<ClassRegex>());
            }
            return titles;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}