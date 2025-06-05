using System;

//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Manager
{
    // Search predicate returns true if title matches matchTo.
    public static class TitleMatch<T> where T : class, ITitle
    {
        private static string _matchTo;

        public static Predicate<T> Match(string matchTo)
        {
            _matchTo = matchTo;
            return IsMatch;
        }

        private static bool IsMatch(T r)
        {
            return (_matchTo == r.Title);
        }
    }
}
