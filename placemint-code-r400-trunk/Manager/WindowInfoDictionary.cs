using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Manager
{
    public class WindowInfoDictionary : Dictionary<IntPtr, WindowInfo>, IDeepCloneable<WindowInfoDictionary>
    {
        public WindowInfoDictionary() { }
        public WindowInfoDictionary(IDictionary<IntPtr, WindowInfo> dictionary)
            : base(dictionary) { }
        public WindowInfoDictionary(IEqualityComparer<IntPtr> comparer)
            : base(comparer) { }
        public WindowInfoDictionary(int capacity)
            : base(capacity) { }
        public WindowInfoDictionary(IDictionary<IntPtr, WindowInfo> dictionary, IEqualityComparer<IntPtr> comparer)
            : base(dictionary, comparer) { }
        public WindowInfoDictionary(int capacity, IEqualityComparer<IntPtr> comparer)
            : base(capacity, comparer) { }
        public WindowInfoDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Count == 0)
            {
                sb.Append("WindowInfoDictionary:Empty");
            }
            else
            {
                foreach (KeyValuePair<IntPtr, WindowInfo> info in this)
                {
                    sb.AppendFormat("Key|{0}|{1}\n", info.Key.ToString(), info.Value.ToString());
                }
                sb.Remove(sb.Length - 1, 1);//remove last new line
            }
            return sb.ToString();
        }

        public WindowInfoDictionary DeepClone()
        {
            WindowInfoDictionary wid = new WindowInfoDictionary();
            foreach (KeyValuePair<IntPtr, WindowInfo> info in this)
            {
                wid.Add(info.Key, info.Value.DeepClone());
            }
            return wid;
        }
    }
}