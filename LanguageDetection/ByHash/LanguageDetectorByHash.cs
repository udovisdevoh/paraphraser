using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetection
{
    public class LanguageDetectorByHash : LanguageDetector
    {
        private bool isAborting = false;

        private Dictionary<string, Dictionary<string, double>> languageWordLists = new Dictionary<string, Dictionary<string, double>>();

        public override void Abort()
        {
            this.isAborting = true;
        }

        public void AddLanguage(string languageName, Dictionary<string, double> wordList)
        {
            this.languageWordLists.Add(StringFormatter.FormatLanguageName(languageName), wordList);
        }

        public override IEnumerable<string> GetLanguageList()
        {
            return this.languageWordLists.Keys;
        }

        public override KeyValuePair<string, double>[] GetLanguageProximities(string text)
        {
            this.isAborting = false;

            string[] words = WordExtractor.GetLowerInvariantWords(text);

            List<KeyValuePair<string, double>> languageProximities = new List<KeyValuePair<string, double>>();

            foreach (KeyValuePair<string, Dictionary<string, double>> languageNameAndWordList in this.languageWordLists)
            {
                string languageName = languageNameAndWordList.Key;
                Dictionary<string, double> wordList = languageNameAndWordList.Value;

                double existingWords = WordExtractor.GetExistingWordsSumOfProbabilities(wordList, words);

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

                if (this.isAborting)
                {
                    break;
                }
            }

            return languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).ToArray();
        }
    }
}
