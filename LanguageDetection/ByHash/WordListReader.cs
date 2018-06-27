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
        public static Dictionary<string, double> BuildWordList(string fileName)
        {
            #warning Add unit tests
            #warning Improve performance

            Dictionary<string, int> wordList = new Dictionary<string, int>();

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    line = StringFormatter.RemoveDoubleTabsSpacesAndEnters(line).ToLowerInvariant();
                    if (!string.IsNullOrEmpty(line))
                    {
                        line = StringFormatter.RemoveLigatures(line);
                        string[] words = WordExtractor.GetLowerInvariantWords(line);

                        foreach (string word in words)
                        {
                            if (!wordList.ContainsKey(word))
                            {
                                wordList[word] = 0;
                            }

                            ++wordList[word];
                        }
                    }
                }
            }

            return WordListReader.NormalizeToMax(wordList);
        }

        public static Dictionary<string, double> NormalizeToMax(Dictionary<string, int> wordList)
        {
            #warning Add unit tests
            #warning Improve performance

            int maxValue = wordList.Values.Max();

            if (maxValue == 0)
            {
                maxValue = 1;
            }

            Dictionary<string, double> normalizedValues = new Dictionary<string, double>();

            foreach (KeyValuePair<string, int> wordAndCount in wordList)
            {
                double normalizedValue = (double)wordAndCount.Value / (double)maxValue;
                normalizedValues.Add(wordAndCount.Key, normalizedValue);
            }

            return normalizedValues;
        }
    }
}
