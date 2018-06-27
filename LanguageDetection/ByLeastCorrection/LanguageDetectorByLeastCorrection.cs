using SpellChecking;
using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetection
{
    public class LanguageDetectorByLeastCorrection : LanguageDetector
    {
        private Dictionary<string, ISpellChecker> spellCheckers = new Dictionary<string, ISpellChecker>();

        public void AddLanguage(string languageName, ISpellChecker spellChecker)
        {
            #warning Add unit tests

            this.spellCheckers.Add(StringFormatter.FormatLanguageName(languageName), spellChecker);
        }

        public override IEnumerable<string> GetLanguageList()
        {
            #warning Add unit tests

            return this.spellCheckers.Keys;
        }

        public override KeyValuePair<string, double>[] GetLanguageProximities(string text)
        {
            #warning Add unit tests
            #warning Replace implementation with "least correction" implementation

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
