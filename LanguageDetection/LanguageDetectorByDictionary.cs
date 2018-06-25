using SpellChecking;
using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetection
{
    public class LanguageDetectorByDictionary : ILanguageDetector
    {
        private Dictionary<string, ISpellChecker> spellCheckers = new Dictionary<string, ISpellChecker>();

        public void AddLanguage(string languageName, ISpellChecker spellChecker)
        {
            this.spellCheckers.Add(languageName, spellChecker);
        }

        public IEnumerable<string> GetLanguageList()
        {
            #warning Add unit tests
            return this.spellCheckers.Keys;
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

            foreach (KeyValuePair<string, ISpellChecker> languageNameAndSpellChecker in this.spellCheckers)
            {
                string languageName = languageNameAndSpellChecker.Key;
                ISpellChecker spellChecker = languageNameAndSpellChecker.Value;

                double proximity = (double)this.CountExistingWords(spellChecker, words) / (double)words.Length;

                languageProximities.Add(new KeyValuePair<string, double>(languageName, proximity));
            }

            return languageProximities.ToArray();
        }

        private int CountExistingWords(ISpellChecker spellChecker, string[] words)
        {
            #warning Add unit tests

            int existingWords = 0;
            foreach (string word in words)
            {
                if (spellChecker.ContainsWord(word))
                {
                    ++existingWords;
                }
            }

            return existingWords;
        }
    }
}
