using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;

//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Manager
{
    [SuppressMessage("Microsoft.Naming", "CA1710")]
    public class SlotList : List<Slot>
    {
        public SlotList() { }
        public SlotList(int capacity)
            : base(capacity) { }
        public SlotList(IEnumerable<Slot> collection)
            : base(collection) { }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Count == 0)
            {
                sb.Append("SlotList:Empty");
            }
            else
            {
                foreach (Slot slot in this)
                {
                    sb.AppendLine(slot.ToString());
                }
                sb.Remove(sb.Length - 2, 2);//remove last new line
            }
            return sb.ToString();
        }
    }
}