namespace PlaceMint.Manager
{
    /// <summary>
    /// Holds a named regular expression that matches a window class.
    /// </summary>
    public class ClassRegex : PMRegex
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ClassRegex()
            : base()
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">Display name</param>
        /// <param name="match">Regex String</param>
        /// <param name="caseSensitive">Regex option</param>
        public ClassRegex(string title, string match, bool caseSensitive)
            :base(title, match, caseSensitive)
        {
        }
    }
}
