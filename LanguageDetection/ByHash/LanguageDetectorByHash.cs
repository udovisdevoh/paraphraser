using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetection
{
    public class LanguageDetectorByHash : ILanguageDetector
    {
        private Dictionary<string, Dictionary<string, double>> languageWordLists = new Dictionary<string, Dictionary<string, double>>();

        public void AddLanguage(string languageName, Dictionary<string, double> wordList)
        {
            #warning Add unit tests

            this.languageWordLists.Add(StringFormatter.FormatLanguageName(languageName), wordList);
        }

        public IEnumerable<string> GetLanguageList()
        {
            #warning Add unit tests

            return this.languageWordLists.Keys;
        }

        public string DetectLanguage(string text)
        {
            #warning Add unit tests

            return this.GetLanguageProximities(text)[0].Key;
        }

        public KeyValuePair<string, double>[] GetLanguageProximities(string text)
        {
            #warning Add unit tests

            string[] words = WordExtractor.GetLowerInvariantWords(text);

            List<KeyValuePair<string, double>> languageProximities = new List<KeyValuePair<string, double>>();

            foreach (KeyValuePair<string, Dictionary<string, double>> languageNameAndWordList in this.languageWordLists)
            {
                string languageName = languageNameAndWordList.Key;
                Dictionary<string, double> wordList = languageNameAndWordList.Value;

                double existingWords = this.GetExistingWordsSumOfProbabilities(wordList, words);

                double proximity;

                if (words.Length == 0)
                {
                    proximity = 0.0;
                }
                else
                {
                    proximity = existingWords / (double)(words.Length);
                }

                languageProximities.Add(new KeyValuePair<string, double>(languageName, proximity));
            }

            return languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).ToArray();
        }

        public double GetExistingWordsSumOfProbabilities(Dictionary<string, double> languageWordProbability, string[] wordsToMatch)
        {
            #warning Add unit tests

            double sumOfProbabilities = 0.0;
            foreach (string word in wordsToMatch)
            {
                string cleanWord = StringFormatter.RemoveLigatures(word.ToLowerInvariant().Trim());

                double probability = 0.0;
                if (languageWordProbability.TryGetValue(cleanWord, out probability))
                {
                    sumOfProbabilities += probability;
                }
            }
            return sumOfProbabilities;
        }
    }
}
