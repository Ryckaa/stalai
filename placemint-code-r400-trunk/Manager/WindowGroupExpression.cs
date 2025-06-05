
//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Manager
{
    /// <summary>
    /// Container for storing a groups regular expression combination
    /// </summary>
    public class WindowGroupExpression
    {
        private TitleList _titles;
        private ClassList _classes;

        /// <summary>
        /// WindowGroupExpression constructor
        /// </summary>
        public WindowGroupExpression(TitleList titles, ClassList classes)
        {
            _titles = titles;
            _classes = classes;
        }

        public TitleList Titles
        { 
            get { return _titles; }
        }

        public ClassList Classes
        {
            get { return _classes; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj == this)
                return true;
            WindowGroupExpression other = obj as WindowGroupExpression;
            if (other == null)
                return false;

            return
                (((_titles == null) && (other.Titles == null))
                    || ((_titles != null) && _titles.Equals(other.Titles)))
                  &&
                (((_classes == null) && (other.Classes == null))
                    || ((_classes != null) && _classes.Equals(other.Classes)));
        }

        public override int GetHashCode()
        {
            int hashcode = 0;
            if (_titles != null)
                hashcode += _titles.GetHashCode();
            if (_classes != null)
                hashcode += _classes.GetHashCode();

            return hashcode;
        }
    }
}
