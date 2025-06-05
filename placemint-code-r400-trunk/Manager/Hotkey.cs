using System;
using System.Windows.Forms;
using System.Xml.Serialization;

//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Manager
{
    public class Hotkey : IEquatable<Hotkey>, IDeepCloneable<Hotkey>
    {
        private ModifyingKeys _modKeys;
        private Keys _key;

        public Hotkey()
            : this(ModifyingKeys.None, Keys.None) { }

        public Hotkey(ModifyingKeys modKeys, Keys key)
        {
            _modKeys = modKeys;
            _key = key;
        }

        [XmlAttribute]
        public ModifyingKeys ModKeys
        {
            get { return _modKeys; }
            set { _modKeys = value; }
        }

        [XmlAttribute]
        public Keys Key
        {
            get { return _key; }
            set { _key = value; }
        }

        [XmlIgnore]
        public bool IsSet
        {
            get { return (_key != Keys.None); }
        }

        [XmlIgnore]
        public bool IsModified
        {
            get { return ((_modKeys & ModifyingKeys.None) == ModifyingKeys.None); }
        }

        [XmlIgnore]
        public bool HasAlt
        {
            get { return ((_modKeys & ModifyingKeys.Alt) == ModifyingKeys.Alt); }
        }

        [XmlIgnore]
        public bool HasControl
        {
            get { return ((_modKeys & ModifyingKeys.Control) == ModifyingKeys.Control); }
        }

        [XmlIgnore]
        public bool HasShift
        {
            get { return ((_modKeys & ModifyingKeys.Shift) == ModifyingKeys.Shift); }
        }

        [XmlIgnore]
        public bool HasWin
        {
            get { return ((_modKeys & ModifyingKeys.Win) == ModifyingKeys.Win); }
        }

        public bool Equals(Hotkey other)
        {
            return ((System.Object)other != null &&
                 this.Key.Equals(other.Key) && this.ModKeys.Equals(other.ModKeys));
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Hotkey hotkey = obj as Hotkey;

            return Equals(hotkey);
        }

        public override int GetHashCode()
        {
            return _key.GetHashCode() + _modKeys.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Key|{0}|ModKeys|{1}|", _key, _modKeys);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>New object with all of the same values.</returns>
        public Hotkey DeepClone()
        {
            return new Hotkey(this.ModKeys, this.Key);
        }
    }

    /// <summary>
    /// The enumeration of possible modifiers.
    /// </summary>
    [Flags]
    public enum ModifyingKeys : uint
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Win = 8
    }
}