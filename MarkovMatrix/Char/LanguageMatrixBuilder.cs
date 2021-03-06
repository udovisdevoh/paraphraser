﻿using MarkovMatrices;
using StringManipulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class LanguageMatrixBuilder : ILanguageMatrixBuilder
    {
        #region Members
        private IMarkovMatrixLoader<char, double> normalizedTextMarkovMatrixLoader;

        private IMarkovMatrixSaver<char, double> binaryMarkovMatrixSaver;
        #endregion

        #region Constructors
        public LanguageMatrixBuilder(IMarkovMatrixLoader<char, double> normalizedTextMarkovMatrixLoader,
            IMarkovMatrixSaver<char, double> binaryMarkovMatrixSaver)
        {
            this.normalizedTextMarkovMatrixLoader = normalizedTextMarkovMatrixLoader;
            this.binaryMarkovMatrixSaver = binaryMarkovMatrixSaver;
        }
        #endregion

        public void BuildMissingLanguageMatrices(string inputDirectory, string outputDirectory)
        {
            string[] textFileNames = Directory.EnumerateFiles(inputDirectory, "*.txt").Select(file => Path.GetFileName(file)).ToArray();

            foreach (string languageSampleTextFile in textFileNames)
            {
                string inputFile = inputDirectory + languageSampleTextFile;
                string outputFile = outputDirectory + languageSampleTextFile.Substring(0, languageSampleTextFile.LastIndexOf(".txt")) + ".bin";

                if (!File.Exists(outputFile))
                {
                    IMarkovMatrix<char, double> languageMatrix = this.BuildLanguageMatrix(inputFile);
                    this.SaveMatrix(languageMatrix, outputFile);
                }
            }
        }

        private void SaveMatrix(IMarkovMatrix<char, double> languageMatrix, string outputFile)
        {
            using (FileStream fileStream = File.Create(outputFile))
            {
                this.binaryMarkovMatrixSaver.SaveMatrix(languageMatrix, fileStream);
            }
        }

        private IMarkovMatrix<char, double> BuildLanguageMatrix(string inputFile)
        {
            string text = File.ReadAllText(inputFile);

            text = text.ToLowerInvariant();
            text = StringFormatter.RemoveDoubleTabsSpacesAndEnters(text);
            text = StringFormatter.RemoveLigatures(text);

            IMarkovMatrix<char, double> matrix;
            matrix = this.normalizedTextMarkovMatrixLoader.LoadMatrix(text);
            return matrix;
        }
    }
}
