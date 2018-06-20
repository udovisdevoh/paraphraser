using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringManipulation
{
    public static class StringFormatter
    {
        #region Members
        private const string punctuationCharsString = "[](){}⟨⟩:;,،、‒–—―….⋯᠁ฯ!.‹›«»‐-?‘’“”\"/⧸⁄\\¿*+-@#÷×^";

        private static char[] punctuationChars;
        #endregion

        static StringFormatter()
        {
            StringFormatter.punctuationChars = punctuationCharsString.ToCharArray();
        }

        public static string FormatLanguageName(string languageName)
        {
            languageName = StringFormatter.FixApostrophe(languageName);
            languageName = StringFormatter.RemoveDoubleTabsSpacesAndEnters(languageName);
            languageName = StringFormatter.UcFirst(languageName);

            return languageName;
        }

        public static string FormatInputText(string text)
        {
            text = StringFormatter.FixApostrophe(text);
            text = StringFormatter.RemovePunctuation(text);
            text = StringFormatter.RemoveDoubleTabsSpacesAndEnters(text);
            text = StringFormatter.UcFirst(text);

            return text;
        }

        public static string RemovePunctuation(string text)
        {
            foreach (char character in StringFormatter.punctuationChars)
            {
                text = text.Replace(character, ' ');
            }

            return text;
        }

        public static string RemoveDiacritics(string text)
        {
            text = text.Replace('Ș', 'S');
            text = text.Replace('ș', 's');
            text = text.Replace('Ț', 'T');
            text = text.Replace('ț', 't');
            text = text.Replace('ẞ', 'S');
            text = text.Replace('ß', 's');            

            byte[] tempBytes;
            tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(text);
            string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);

            return asciiStr;
        }

        public static string FixApostrophe(string text)
        {
            return text.Replace('’', '\'');
        }

        public static string RemoveDoubleTabsSpacesAndEnters(string text)
        {
            text = text.Replace('\r', ' ');
            text = text.Replace('\t', ' ');
            text = text.Replace('\n', ' ');

            while (text.Contains("  "))
            {
                text = text.Replace("  ", " ");
            }

            text = text.Trim();

            return text;
        }

        public static string UcFirst(string text)
        {
            if (text.Length > 0)
            {
                text = text.Substring(0, 1).ToUpperInvariant() + text.Substring(1).ToLowerInvariant();
            }

            return text;
        }
    }
}
