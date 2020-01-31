using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageDetection;
using MarkovMatrices;
using Paraphrasing;
using StringManipulation;

namespace ParaphaserBootstrap
{
    public class Bootstrap
    {
        public LanguageDetectorByMarkovMatrix BuildLanguageDetectorByMarkovMatrix()
        {
            IMarkovMatrixLoader<char, ulong> languageDetectionMatrixLoader = new TextMarkovMatrixLoader();
            IMarkovMatrixNormalizer<char> markovMatrixNormalizer = new MarkovMatrixNormalizer();

            IMarkovMatrixLoader<char, double> normalizedLanguageDetectionMatrixLoader = new NormalizedTextMarkovMatrixLoader(languageDetectionMatrixLoader, markovMatrixNormalizer);

            LanguageDetectorByMarkovMatrix languageDetectorByMarkovMatrix = new LanguageDetectorByMarkovMatrix(normalizedLanguageDetectionMatrixLoader);

            return languageDetectorByMarkovMatrix;
        }

        public ISentenceTypeDetector BuildSentenceTypeDetector()
        {
            return new EnglishSentenceTypeDetector();
        }

        public IEnglishInterrogativeToAffirmative BuildEnglishInterrogativeToAffirmative()
        {
            return new EnglishInterrogativeToAffirmative(new WordOrderSwapper());
        }

        public ILanguageDetector BuildLanguageDetectorByMarkovMatrixBasedOnTextFiles(string textDirectory)
        {
            LanguageDetectorByMarkovMatrix languageDetectorByMarkovMatrix = BuildLanguageDetectorByMarkovMatrix();

            IMarkovMatrixLoader<char, ulong> markovMatrixLoader = new TextMarkovMatrixLoader();
            IMarkovMatrixNormalizer<char> markovMatrixConverter = new MarkovMatrixNormalizer();

            string[] textFiles = Directory.EnumerateFiles(textDirectory, "*.txt").Select(file => Path.GetFileName(file)).ToArray();

            foreach (string textFile in textFiles)
            {
                string languageName = StringFormatter.FormatLanguageName(textFile.Substring(0, textFile.LastIndexOf('.')));

                IMarkovMatrix<char, ulong> matrix;
                using (FileStream fileStream = File.Open(textDirectory + textFile, FileMode.Open))
                {
                    matrix = markovMatrixLoader.LoadMatrix(fileStream);
                }

                languageDetectorByMarkovMatrix.AddLanguage(languageName, markovMatrixConverter.Convert(matrix));
            }

            return languageDetectorByMarkovMatrix;
        }

        public IMarkovMatrixLoader<string, double> BuildStringMarkovMatrixLoaderFromText()
        {
            return new StringMarkovMatrixLoaderFromText();
        }

        public IFirstSecondPersonInverter BuildFirstSecondPersonInverter()
        {
            return new FirstSecondPersonInverter();
        }

        public ILanguageMatrixBuilder BuildLanguageMatrixBuilder()
        {
            IMarkovMatrixLoader<char, ulong> languageDetectionMatrixLoader = new TextMarkovMatrixLoader();
            IMarkovMatrixNormalizer<char> markovMatrixNormalizer = new MarkovMatrixNormalizer();

            IMarkovMatrixLoader<char, double> normalizedLanguageDetectionMatrixLoader = new NormalizedTextMarkovMatrixLoader(languageDetectionMatrixLoader, markovMatrixNormalizer);
            IMarkovMatrixSaver<char, double> binaryMarkovMatrixSaver = new BinaryMarkovMatrixSaver();

            ILanguageMatrixBuilder languageMatrixBuilder = new LanguageMatrixBuilder(normalizedLanguageDetectionMatrixLoader, binaryMarkovMatrixSaver);
            return languageMatrixBuilder;
        }
    }
}
