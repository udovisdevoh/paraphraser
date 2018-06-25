using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkovMatrices;
using ParaphraserMath;

namespace LanguageDetection
{
    public class CompositeLanguageDetector : ILanguageDetector
    {
        private List<ILanguageDetector> languageDetectors = new List<ILanguageDetector>();

        public string DetectLanguage(string text)
        {
            return this.GetLanguageProximities(text)[0].Key;
        }

        public void AddLanguageDetector(ILanguageDetector languageDetector)
        {
            this.AssertSameLanguages(languageDetector);
            this.languageDetectors.Add(languageDetector);
        }

        public KeyValuePair<string, double>[] GetLanguageProximities(string text)
        {
            if (this.languageDetectors.Count < 1)
            {
                throw new InvalidOperationException("The composite language detector must contain at least one language detector. Use AddLanguageDetector() first");
            }

            double maximumStandardDeviation = double.MinValue;
            KeyValuePair<string, double>[] largestStandardDeviationProximities = null;

            foreach (ILanguageDetector currentLanguageDetector in this.languageDetectors)
            {
                KeyValuePair<string, double>[] languageProximities = currentLanguageDetector.GetLanguageProximities(text);

                double currentStandardDeviation = MatrixMathHelper.GetStandardDeviation(languageProximities.Select(keyValuePair => keyValuePair.Value).ToArray());
                //double currentStandardDeviation = languageProximities[0].Value;

                if (currentStandardDeviation > maximumStandardDeviation || largestStandardDeviationProximities == null)
                {
                    largestStandardDeviationProximities = languageProximities;
                    maximumStandardDeviation = currentStandardDeviation;
                }
            }

            return largestStandardDeviationProximities;
        }

        public void AssertSameLanguages(ILanguageDetector languageDetector)
        {
            #warning Add unit tests

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

        public IEnumerable<string> GetLanguageList()
        {
            #warning Add unit tests

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
