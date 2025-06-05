using System;
using System.Drawing;
using System.Xml.Serialization;
using System.Diagnostics.CodeAnalysis;

//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Manager
{
    /// <summary>
    /// Wrapper class to keep the XML shorter
    /// </summary>
    [Serializable]
    public class RectangleWrap : IDeepCloneable<RectangleWrap>
    {
        private Rectangle _rect;

        public RectangleWrap()
        {
            _rect = new Rectangle();
        }
        public RectangleWrap(Point location, Size size)
        {
            _rect = new Rectangle(location, size);
        }
        [SuppressMessage("Microsoft.Naming", "CA1704")]
        public RectangleWrap(int x, int y, int w, int h)
        {
            _rect = new Rectangle(x, y, w, h);
        }
        public RectangleWrap(WindowsApi.RECT rect)
        {
            _rect = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
        }
        public RectangleWrap(RectangleWrap rect)
        {
            _rect = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }

        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [XmlAttribute]
        public int X
        {
            get { return _rect.X; }
            set { _rect.X = value; }
        }

        [SuppressMessage("Microsoft.Naming", "CA1704")]
        [XmlAttribute]
        public int Y
        {
            get { return _rect.Y; }
            set { _rect.Y = value; }
        }

        [XmlAttribute]
        public int Height
        {
            get { return _rect.Height; }
            set { _rect.Height = value; }
        }

        [XmlAttribute]
        public int Width
        {
            get { return _rect.Width; }
            set { _rect.Width = value; }
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            RectangleWrap rw = obj as RectangleWrap;

            return (this._rect.Equals(rw._rect));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public RectangleWrap DeepClone()
        {
            return new RectangleWrap(this.X, this.Y, this.Width, this.Height);
        }

        public override string ToString()
        {
            return "RectangleWrap:\n  _rect|" + _rect.ToString();
        }
    }
}