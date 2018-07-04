using MarkovMatrices;
using ParaphraserMath;
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

        private IMarkovMatrixLoader<char, double> comparisonMatrixLoader;

        public LanguageDetectorByLeastCorrection(IMarkovMatrixLoader<char, double> resultComparisonMatrixLoader)
        {
            this.comparisonMatrixLoader = resultComparisonMatrixLoader;
        }

        public void AddLanguage(string languageName, ISpellChecker spellChecker)
        {
            this.spellCheckers.Add(StringFormatter.FormatLanguageName(languageName), spellChecker);
        }

        public override IEnumerable<string> GetLanguageList()
        {
            return this.spellCheckers.Keys;
        }

        public override KeyValuePair<string, double>[] GetLanguageProximities(string sourceText)
        {
            sourceText = this.FormatText(sourceText);

            string[] words = WordExtractor.GetLowerInvariantWords(sourceText);

            List<KeyValuePair<string, double>> languageProximities = new List<KeyValuePair<string, double>>();

            Parallel.ForEach(this.spellCheckers, (languageNameAndSpellChecker, state) =>
            {
            /*foreach (KeyValuePair<string, ISpellChecker> languageNameAndSpellChecker in this.spellCheckers)
            {*/
                string languageName = languageNameAndSpellChecker.Key;
                ISpellChecker spellChecker = languageNameAndSpellChecker.Value;

                string correctedText = spellChecker.GetCorrectedText(sourceText, "*******");

                correctedText = this.FormatText(correctedText);

                double proximity = this.GetMarkovMatrixProximity(sourceText, correctedText);
                //double proximity = StringAnalysis.GetLevenshteinDistance(sourceText, correctedText) * -1.0;

                lock (languageProximities)
                {
                    languageProximities.Add(new KeyValuePair<string, double>(languageName, proximity));
                }
            /*}*/
            });

            return languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).ToArray();
        }

        private double GetMarkovMatrixProximity(string fromText, string toText)
        {
            IMarkovMatrix<char, double> fromMatrix = comparisonMatrixLoader.LoadMatrix(fromText);
            IMarkovMatrix<char, double> toMatrix = comparisonMatrixLoader.LoadMatrix(toText);

            //double proximity = MatrixMathHelper.GetDotProduct(fromMatrix, toMatrix);
            double proximity = 1.0 - MatrixMathHelper.GetDistance(fromMatrix, toMatrix);

            return proximity;
        }

        private string FormatText(string text)
        {
            return StringFormatter.FormatInputText(text).ToLowerInvariant();
        }
    }
}
