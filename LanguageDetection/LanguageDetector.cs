using MarkovMatrices;
using ParaphraserMath;
using StringManipulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetection
{
    public class LanguageDetector : ILanguageDetector
    {
        #region Members
        private Dictionary<string, IMarkovMatrix<float>> languages;

        private IMarkovMatrixLoader<ulong> languageDetectionMatrixLoader;

        private IMarkovMatrixNormalizer markovMatrixNormalizer;
        #endregion

        #region Constructors
        public LanguageDetector(IMarkovMatrixLoader<ulong> languageDetectionMatrixLoader, IMarkovMatrixNormalizer markovMatrixNormalizer)
        {
            this.languages = new Dictionary<string, IMarkovMatrix<float>>();
            this.languageDetectionMatrixLoader = languageDetectionMatrixLoader;
            this.markovMatrixNormalizer = markovMatrixNormalizer;
        }
        #endregion

        #region Properties
        public int Count
        {
            get { return this.languages.Count; }
        }
        #endregion

        public void AddLanguage(string name, IMarkovMatrix<float> languageMatrix)
        {
            name = StringFormatter.FormatLanguageName(name);
            this.languages.Add(name, languageMatrix);
        }

        public string DetectLanguage(string text)
        {
            if (languages.Count < 1)
            {
                #warning Add unit tests for throw
                throw new InvalidOperationException("The language detector must know at least one language. Use AddLanguage() first");
            }

            #warning Add unit tests

            MemoryStream memoryStream = MemoryStreamBuilder.BuildMemoryStreamFromText(text);
            IMarkovMatrix<ulong> inputMatrix = this.languageDetectionMatrixLoader.LoadMatrix(memoryStream);
            IMarkovMatrix<float> normalizedInputMatrix = this.markovMatrixNormalizer.Normalize(inputMatrix);

            float bestDotProduct = float.MinValue;
            string bestLanguage = null;
            foreach (KeyValuePair<string, IMarkovMatrix<float>> nameAndLanguageMatrix in this.languages)
            {
                string languageName = nameAndLanguageMatrix.Key;
                IMarkovMatrix<float> languageMatrix = nameAndLanguageMatrix.Value;
                float dotProduct = MatrixMathHelper.GetDotProduct(normalizedInputMatrix, languageMatrix);

                if (dotProduct > bestDotProduct)
                {
                    bestDotProduct = dotProduct;
                    bestLanguage = languageName;
                }
            }

            return bestLanguage;
        }
    }
}
