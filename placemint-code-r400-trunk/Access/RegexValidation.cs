using System.Text.RegularExpressions;

//cs1591: Missing XML comment
#pragma warning disable 1591

namespace PlaceMint.Access
{
    public static class RegexValidation
    {
        public static Regex isNum = new Regex(@"^-?\d+$");
        public static Regex isNewGroup = new Regex(@"^New\sGroup\s(\d+)$");
    }
}
