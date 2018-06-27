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
        public static Dictionary<string, double> BuildWordCountProbability(Stream stream)
        {
            Dictionary<string, int> wordList = WordListReader.GetWordCount(stream);

            return WordListReader.NormalizeToMax(wordList);
        }

        public static Dictionary<string, int> GetWordCount(Stream stream)
        {
            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            using (StreamReader streamReader = new StreamReader(stream))
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
                            if (!wordCount.ContainsKey(word))
                            {
                                wordCount[word] = 0;
                            }

                            ++wordCount[word];
                        }
                    }
                }
            }

            return wordCount;
        }

        public static Dictionary<string, double> NormalizeToMax(Dictionary<string, int> wordList)
        {
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
