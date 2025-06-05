using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics.CodeAnalysis;

//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Manager
{
    [SuppressMessage("Microsoft.Naming", "CA1710")]
    [XmlRoot("SlotTemplateList")]
    public class SlotTemplateList : List<SlotTemplate>, IFileNotFound
    {
        public SlotTemplateList() { }
        public SlotTemplateList(int capacity)
            : base(capacity) { }
        public SlotTemplateList(IEnumerable<SlotTemplate> collection)
            : base(collection) { }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Count == 0)
            {
                sb.Append("SlotTemplate:Empty");
            }
            else
            {
                foreach (SlotTemplate slotTemplate in this)
                {
                    sb.AppendLine(slotTemplate.ToString());
                }
                sb.Remove(sb.Length - 2, 2);//remove last new line
            }
            return sb.ToString();
        }

        public string FileNotFoundMsg()
        {
            return Properties.Resources.slotTemplateDileNotFound;
        }
    }
}