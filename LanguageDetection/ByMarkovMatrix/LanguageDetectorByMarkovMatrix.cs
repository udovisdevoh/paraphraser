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
        private Dictionary<string, IMarkovMatrix<char, double>> languages;

        private IMarkovMatrixLoader<char, double> languageDetectionMatrixLoader;

        private object listLock = new object();
        #endregion

        #region Constructors
        public LanguageDetectorByMarkovMatrix(IMarkovMatrixLoader<char, double> languageDetectionMatrixLoader)
        {
            this.languages = new Dictionary<string, IMarkovMatrix<char, double>>();
            this.languageDetectionMatrixLoader = languageDetectionMatrixLoader;
        }
        #endregion

        #region Properties
        public int Count
        {
            get { return this.languages.Count; }
        }
        #endregion

        public void AddLanguage(string name, IMarkovMatrix<char, double> languageMatrix)
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
            if (languages.Count < 1)
            {
                throw new InvalidOperationException("The language detector must know at least one language. Use AddLanguage() first");
            }

            MemoryStream memoryStream = MemoryStreamBuilder.BuildMemoryStreamFromText(text);
            IMarkovMatrix<char, double> inputMatrix = this.languageDetectionMatrixLoader.LoadMatrix(memoryStream);

            List<KeyValuePair<string, double>> languageProximities = new List<KeyValuePair<string, double>>();

            //foreach (KeyValuePair<string, IMarkovMatrix<char, double>> nameAndLanguageMatrix in this.languages)
            Parallel.ForEach(this.languages, nameAndLanguageMatrix =>
            {
                string languageName = nameAndLanguageMatrix.Key;
                IMarkovMatrix<char, double> languageMatrix = nameAndLanguageMatrix.Value;
                double proximity = MatrixMathHelper.GetDotProduct(inputMatrix, languageMatrix);

                lock (this.listLock)
                {
                    languageProximities.Add(new KeyValuePair<string, double>(languageName, proximity));
                }
            });

            return languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).ToArray();
        }

        public virtual string TransformInput(string text)
        {
            return text;
        }

        public virtual IMarkovMatrix<char, double> TransformMatrix(IMarkovMatrix<char, double> matrix)
        {
            return matrix;
        }

        public override IEnumerable<string> GetLanguageList()
        {
            return this.languages.Keys;
        }

        public IMarkovMatrix<char, double> GetLanguageMatrix(string languageName)
        {
            return this.languages[languageName];
        }
    }
}
