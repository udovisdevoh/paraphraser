﻿using SpellChecking;
using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetection
{
    public class LanguageDetectorByDictionary : LanguageDetector
    {
        private Dictionary<string, ISpellChecker> spellCheckers = new Dictionary<string, ISpellChecker>();

        public void AddLanguage(string languageName, ISpellChecker spellChecker)
        {
            this.spellCheckers.Add(StringFormatter.FormatLanguageName(languageName), spellChecker);
        }

        public override IEnumerable<string> GetLanguageList()
        {
            return this.spellCheckers.Keys;
        }

        public override KeyValuePair<string, double>[] GetLanguageProximities(string text)
        {
            string[] words = WordExtractor.GetLowerInvariantWords(text);

            List<KeyValuePair<string, double>> languageProximities = new List<KeyValuePair<string, double>>();

            Parallel.ForEach(this.spellCheckers, (languageNameAndSpellChecker, state) =>
            {
                string languageName = languageNameAndSpellChecker.Key;
                ISpellChecker spellChecker = languageNameAndSpellChecker.Value;

                double proximity = (double)spellChecker.CountExistingWords(words) / (double)words.Length;

                lock (languageProximities)
                {
                    languageProximities.Add(new KeyValuePair<string, double>(languageName, proximity));
                }
            });

            return languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).ToArray();
        }
    }
}
