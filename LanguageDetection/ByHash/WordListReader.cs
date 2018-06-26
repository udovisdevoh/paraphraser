using StringManipulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetection
{
    public static class WordListReader
    {
        public static HashSet<string> BuildWordList(string fileName)
        {
            #warning Add unit tests
            #warning Improve performance

            HashSet<string> wordList = new HashSet<string>();

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    line = StringFormatter.RemoveDoubleTabsSpacesAndEnters(line).ToLowerInvariant();
                    if (!string.IsNullOrEmpty(line))
                    {
                        line = StringFormatter.RemoveLigatures(line);
                        wordList.Add(line);
                        line = StringFormatter.RemoveDiacritics(line);
                        wordList.Add(line);
                    }
                }
            }

            return wordList;
        }
    }
}
