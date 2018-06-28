using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkovMatrices;
using ParaphraserMath;

namespace LanguageDetection
{
    public class CompositeLanguageDetector : LanguageDetector, ICompositeLanguageDetector
    {
        private List<ILanguageDetector> languageDetectors = new List<ILanguageDetector>();

        public void AddLanguageDetector(ILanguageDetector languageDetector)
        {
            if (!languageDetector.GetLanguageList().Any())
            {
                throw new ArgumentException("The component language detector must contain at least one language");
            }

            this.AssertSameLanguages(languageDetector);
            this.languageDetectors.Add(languageDetector);
        }

        public override KeyValuePair<string, double>[] GetLanguageProximities(string text)
        {
            if (this.languageDetectors.Count < 1)
            {
                throw new InvalidOperationException("The composite language detector must contain at least one language detector. Use AddLanguageDetector() first");
            }

            Dictionary<string, double> compositeLanguageProximities = new Dictionary<string, double>();

            foreach (ILanguageDetector currentLanguageDetector in this.languageDetectors)
            {
                KeyValuePair<string, double>[] languageProximities = currentLanguageDetector.GetLanguageProximities(text);

                foreach (KeyValuePair<string, double> languageAndProximity in languageProximities)
                {
                    string languageName = languageAndProximity.Key;
                    double proximity = languageAndProximity.Value / (double)this.languageDetectors.Count;

                    if (!compositeLanguageProximities.ContainsKey(languageName))
                    {
                        compositeLanguageProximities[languageName] = 0.0;
                    }

                    compositeLanguageProximities[languageName] += proximity;
                }
            }

            return ((IEnumerable<KeyValuePair<string, double>>)compositeLanguageProximities).OrderByDescending(keyValuePair => keyValuePair.Value).ToArray();
        }

        public void AssertSameLanguages(ILanguageDetector languageDetector)
        {
            const string missingLanguageFormat = "Missing language '{0}' in component detector '{1}'.";

            IEnumerable<string> languageLists = languageDetector.GetLanguageList();

            foreach (ILanguageDetector otherLanguageDetector in this.languageDetectors)
            {
                IEnumerable<string> languageListsOtherDetector = otherLanguageDetector.GetLanguageList();

                foreach (string language in languageLists)
                {
                    if (!languageListsOtherDetector.Contains(language))
                    {
                        throw new ArgumentException(string.Format(missingLanguageFormat, language, otherLanguageDetector.GetType().Name));
                    }
                }

                foreach (string language in languageListsOtherDetector)
                {
                    if (!languageLists.Contains(language))
                    {
                        throw new ArgumentException(string.Format(missingLanguageFormat, language, languageDetector.GetType().Name));
                    }
                }
            }
        }

        public override IEnumerable<string> GetLanguageList()
        {
            HashSet<string> languages = new HashSet<string>();
            foreach (ILanguageDetector componentLanguageDetector in this.languageDetectors)
            {
                foreach (string language in componentLanguageDetector.GetLanguageList())
                {
                    languages.Add(language);
                }
            }
            return languages;
        }
    }
}
