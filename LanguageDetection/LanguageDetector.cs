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

        private IMarkovMatrixLoader<float> languageDetectionMatrixLoader;
        #endregion

        #region Constructors
        public LanguageDetector(IMarkovMatrixLoader<float> languageDetectionMatrixLoader)
        {
            this.languages = new Dictionary<string, IMarkovMatrix<float>>();
            this.languageDetectionMatrixLoader = languageDetectionMatrixLoader;
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
                throw new InvalidOperationException("The language detector must know at least one language. Use AddLanguage() first");
            }

            MemoryStream memoryStream = MemoryStreamBuilder.BuildMemoryStreamFromText(text);
            IMarkovMatrix<float> inputMatrix = this.languageDetectionMatrixLoader.LoadMatrix(memoryStream);

            float bestDotProduct = float.MinValue;
            string bestLanguage = null;
            foreach (KeyValuePair<string, IMarkovMatrix<float>> nameAndLanguageMatrix in this.languages)
            {
                string languageName = nameAndLanguageMatrix.Key;
                IMarkovMatrix<float> languageMatrix = nameAndLanguageMatrix.Value;
                float dotProduct = MatrixMathHelper.GetDotProduct(inputMatrix, languageMatrix);

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
