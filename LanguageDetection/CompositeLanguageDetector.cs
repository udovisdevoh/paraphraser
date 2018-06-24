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

        public void AddLanguage(string name, IMarkovMatrix<double> languageMatrix)
        {
            foreach (ILanguageDetector languageDetector in this.languageDetectors)
            {
                languageDetector.AddLanguage(name, languageMatrix);
            }
        }

        public string DetectLanguage(string text)
        {
            return this.GetLanguageProximities(text)[0].Key;
        }

        public void AddLanguageDetector(ILanguageDetector languageDetector)
        {
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
    }
}
