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
    public class LanguageDetectorByMarkovMatrix : LanguageDetector
    {
        #region Members
        private bool isAborting = false;

        private Dictionary<string, IMarkovMatrix<double>> languages;

        private IMarkovMatrixLoader<double> languageDetectionMatrixLoader;
        #endregion

        #region Constructors
        public LanguageDetectorByMarkovMatrix(IMarkovMatrixLoader<double> languageDetectionMatrixLoader)
        {
            this.languages = new Dictionary<string, IMarkovMatrix<double>>();
            this.languageDetectionMatrixLoader = languageDetectionMatrixLoader;
        }
        #endregion

        #region Properties
        public int Count
        {
            get { return this.languages.Count; }
        }
        #endregion

        public void AddLanguage(string name, IMarkovMatrix<double> languageMatrix)
        {
            name = StringFormatter.FormatLanguageName(name);
            this.languages.Add(name, this.TransformMatrix(languageMatrix));
        }

        public override string DetectLanguage(string text)
        {
            text = this.TransformInput(text);
            return this.GetLanguageProximities(text)[0].Key;
        }

        public override KeyValuePair<string, double>[] GetLanguageProximities(string text)
        {
            this.isAborting = false;

            if (languages.Count < 1)
            {
                throw new InvalidOperationException("The language detector must know at least one language. Use AddLanguage() first");
            }

            MemoryStream memoryStream = MemoryStreamBuilder.BuildMemoryStreamFromText(text);
            IMarkovMatrix<double> inputMatrix = this.languageDetectionMatrixLoader.LoadMatrix(memoryStream);

            List<KeyValuePair<string, double>> languageProximities = new List<KeyValuePair<string, double>>();

            foreach (KeyValuePair<string, IMarkovMatrix<double>> nameAndLanguageMatrix in this.languages)
            {
                string languageName = nameAndLanguageMatrix.Key;
                IMarkovMatrix<double> languageMatrix = nameAndLanguageMatrix.Value;
                //double proximity = MatrixMathHelper.GetDotProduct(inputMatrix, languageMatrix);
                double proximity = 1.0 - MatrixMathHelper.GetDistance(inputMatrix, languageMatrix);

                languageProximities.Add(new KeyValuePair<string, double>(languageName, proximity));

                if (this.isAborting)
                {
                    break;
                }
            }

            return languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).ToArray();
        }

        public virtual string TransformInput(string text)
        {
            return text;
        }

        public virtual IMarkovMatrix<double> TransformMatrix(IMarkovMatrix<double> matrix)
        {
            return matrix;
        }

        public override IEnumerable<string> GetLanguageList()
        {
            return this.languages.Keys;
        }

        public override void Abort()
        {
            #warning Add unit tests
            this.isAborting = true;
        }
    }
}
