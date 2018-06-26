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
            this.spellCheckers.Add(StringFormatter.FormatLanguageName(languageName), spellChecker);
        }

        public IEnumerable<string> GetLanguageList()
        {
            return this.spellCheckers.Keys;
        }

        public string DetectLanguage(string text)
        {
            return this.GetLanguageProximities(text)[0].Key;
        }

        public KeyValuePair<string, double>[] GetLanguageProximities(string text)
        {
            string[] words = WordExtractor.GetLowerInvariantWords(text);

            List<KeyValuePair<string, double>> languageProximities = new List<KeyValuePair<string, double>>();

            foreach (KeyValuePair<string, ISpellChecker> languageNameAndSpellChecker in this.spellCheckers)
            {
                string languageName = languageNameAndSpellChecker.Key;
                ISpellChecker spellChecker = languageNameAndSpellChecker.Value;

                double proximity = (double)spellChecker.CountExistingWords(words) / (double)words.Length;

                languageProximities.Add(new KeyValuePair<string, double>(languageName, proximity));
            }

            return languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).ToArray();
        }
    }
}
