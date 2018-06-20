using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringManipulation
{
    public static class StringFormatter
    {
        public static string FormatLanguageName(string languageName)
        {
            languageName = languageName.Replace('\r', ' ');
            languageName = languageName.Replace('\t', ' ');
            languageName = languageName.Replace('\n', ' ');

            while (languageName.Contains("  "))
            {
                languageName = languageName.Replace("  ", " ");
            }

            languageName = languageName.Trim();

            if (languageName.Length > 0)
            {
                languageName = languageName.Substring(0, 1).ToUpperInvariant() + languageName.Substring(1).ToLowerInvariant();
            }

            return languageName;
        }
    }
}
