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
        private Dictionary<string, HashSet<string>> languageWordLists = new Dictionary<string, HashSet<string>>();

        public void AddLanguage(string languageName, HashSet<string> wordList)
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

            foreach (KeyValuePair<string, HashSet<string>> languageNameAndSpellChecker in this.languageWordLists)
            {
                string languageName = languageNameAndSpellChecker.Key;
                HashSet<string> wordList = languageNameAndSpellChecker.Value;

                int existingWords = this.CountExistingWords(wordList, words, false);
                int existingWordsNoDiacritics = this.CountExistingWords(wordList, words, true);

                double proximity = (double)(existingWords + existingWordsNoDiacritics) / (double)(words.Length * 2);

                languageProximities.Add(new KeyValuePair<string, double>(languageName, proximity));
            }

            return languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).ToArray();
        }

        public int CountExistingWords(HashSet<string> wordHash, string[] wordsToMatch, bool isRemoveDiacritics)
        {
            #warning Add unit tests

            int count = 0;
            foreach (string word in wordsToMatch)
            {
                string cleanWord = StringFormatter.RemoveLigatures(word.ToLowerInvariant().Trim());

                if (isRemoveDiacritics)
                {
                    cleanWord = StringFormatter.RemoveDiacritics(word);
                }

                if (wordHash.Contains(cleanWord))
                {
                    ++count;
                }
            }
            return count;
        }
    }
}
