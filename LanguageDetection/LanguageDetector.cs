﻿using MarkovMatrices;
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
        private Dictionary<string, IMarkovMatrix<double>> languages;

        private IMarkovMatrixLoader<double> languageDetectionMatrixLoader;
        #endregion

        #region Constructors
        public LanguageDetector(IMarkovMatrixLoader<double> languageDetectionMatrixLoader)
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
            this.languages.Add(name, languageMatrix);
        }

        public string DetectLanguage(string text)
        {
            return this.GetLanguageProximities(text)[0].Key;
        }

        public KeyValuePair<string, double>[] GetLanguageProximities(string text)
        {
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
                double proximity = MatrixMathHelper.GetDotProduct(inputMatrix, languageMatrix);// + MatrixMathHelper.GetFromCharOccurrenceSum(inputMatrix, languageMatrix);

                languageProximities.Add(new KeyValuePair<string, double>(languageName, proximity));
            }

            return languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).ToArray();
        }
    }
}
