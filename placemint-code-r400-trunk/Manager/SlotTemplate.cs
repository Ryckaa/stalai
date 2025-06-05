using System;
using System.Xml.Serialization;

//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Manager
{
    [XmlRoot("SlotTemplate")]
    public class SlotTemplate : IComparable<SlotTemplate>, ITitle
    {
        private string _title;
        private int _width;
        private int _height;

        public SlotTemplate() { }
        public SlotTemplate(string title, int width, int height)
        {
            _title = title;
            _width = width;
            _height = height;
        }

        [XmlAttribute("title")]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        [XmlAttribute("width")]
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        [XmlAttribute("height")]
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public override string ToString()
        {
            return string.Format("SlotTemplate: title|{0}|width|{1}|height|{2}|", _title, _width, _height);
        }

        public override int GetHashCode()
        {
            return _title.GetHashCode() + _height.GetHashCode() + _width.GetHashCode();
        }

        public bool Equals(SlotTemplate other)
        {
            if (other == null)
            {
                return false;
            }
            if ((Object)this == (Object)other) //identity compare
            {
                return true;
            }
            return (this._title.Equals(other.Title) && this._width.Equals(other.Width)
                && this._height.Equals(other.Height));
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            SlotTemplate sTemp = obj as SlotTemplate;

            return Equals(sTemp);
        }

        /// <summary>
        /// 
        /// </summary>
        public int CompareTo(SlotTemplate other)
        {
            if (other.Title == this.Title)
            {
                return 0;
            }
            return String.Compare(this.Title, other.Title);
        }
    }
}