namespace PlaceMint.Manager
{
    /// <summary>
    /// Holds a named regular expression that matches a window title.
    /// </summary>
    public class TitleRegex : PMRegex
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TitleRegex()
            : base()
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">Display name</param>
        /// <param name="match">Regex String</param>
        /// <param name="caseSensitive">Regex option</param>
        public TitleRegex(string title, string match, bool caseSensitive)
            :base(title, match, caseSensitive)
        {
        }
    }
}
