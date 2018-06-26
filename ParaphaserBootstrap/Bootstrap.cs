using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageDetection;
using MarkovMatrices;
using SpellChecking;
using StringManipulation;

namespace ParaphaserBootstrap
{
    public class Bootstrap
    {
        public LanguageDetector BuildLanguageDetector()
        {
            IMarkovMatrixLoader<ulong> languageDetectionMatrixLoader = new TextMarkovMatrixLoader();
            IMarkovMatrixNormalizer markovMatrixNormalizer = new MarkovMatrixNormalizer();

            IMarkovMatrixLoader<double> normalizedLanguageDetectionMatrixLoader = new NormalizedTextMarkovMatrixLoader(languageDetectionMatrixLoader, markovMatrixNormalizer);

            LanguageDetector languageDetector = new LanguageDetector(normalizedLanguageDetectionMatrixLoader);

            return languageDetector;
        }

        public ICompositeLanguageDetector BuildCompositeLanguageDetector()
        {
            CompositeLanguageDetector compositeLanguageDetector = new CompositeLanguageDetector();

            return compositeLanguageDetector;
        }

        public ILanguageDetector BuildLanguageDetectorNoDiacritics()
        {
            IMarkovMatrixLoader<ulong> languageDetectionMatrixLoader = new TextMarkovMatrixLoader();
            IMarkovMatrixNormalizer markovMatrixNormalizer = new MarkovMatrixNormalizer();

            IMarkovMatrixLoader<double> normalizedLanguageDetectionMatrixLoader = new NormalizedTextMarkovMatrixLoader(languageDetectionMatrixLoader, markovMatrixNormalizer);
            IMarkovMatrixTransformer markovMatrixCharacterCombiner = new MarkovMatrixCharacterCombiner(letter => StringFormatter.RemoveDiacritics(letter));

            LanguageDetector languageDetector = new LanguageDetectorNoDiacritics(normalizedLanguageDetectionMatrixLoader, markovMatrixCharacterCombiner);

            return languageDetector;
        }

        public ISpellChecker BuildSpellChecker(string dictionary, string languageName)
        {
            ISpellChecker spellChecker = new SpellChecker(dictionary, languageName);
            return spellChecker;
        }

        public IMarkovMatrixLoader<double> BuildBinaryMarkovMatrixLoader()
        {
            IMarkovMatrixLoader<double> binaryMarkovMatrixLoader = new BinaryMarkovMatrixLoader();
            return binaryMarkovMatrixLoader;
        }

        public ILanguageMatrixBuilder BuildLanguageMatrixBuilder()
        {
            IMarkovMatrixLoader<ulong> languageDetectionMatrixLoader = new TextMarkovMatrixLoader();
            IMarkovMatrixNormalizer markovMatrixNormalizer = new MarkovMatrixNormalizer();

            IMarkovMatrixLoader<double> normalizedLanguageDetectionMatrixLoader = new NormalizedTextMarkovMatrixLoader(languageDetectionMatrixLoader, markovMatrixNormalizer);
            IMarkovMatrixSaver<double> binaryMarkovMatrixSaver = new BinaryMarkovMatrixSaver();

            ILanguageMatrixBuilder languageMatrixBuilder = new LanguageMatrixBuilder(normalizedLanguageDetectionMatrixLoader, binaryMarkovMatrixSaver);
            return languageMatrixBuilder;
        }

        public ILanguageDetector BuildLanguageDetectorByDictionary(string spellCheckDictionaries)
        {
            LanguageDetectorByDictionary languageDetectorByDictionary = new LanguageDetectorByDictionary();

            string[] dictionaryFiles = Directory.EnumerateFiles(spellCheckDictionaries, "*.dic").Select(file => Path.GetFileName(file)).ToArray();

            foreach (string dictionaryFile in dictionaryFiles)
            {
                string languageName = dictionaryFile.Substring(0, dictionaryFile.LastIndexOf("."));
                ISpellChecker spellChecker = this.BuildSpellChecker(spellCheckDictionaries, languageName);

                languageDetectorByDictionary.AddLanguage(languageName, spellChecker);
            }

            return languageDetectorByDictionary;
        }

        public ILanguageDetector BuildLanguageDetectorByMarkovMatrix(string matricesDirectory)
        {
            LanguageDetector languageDetectorByMarkovMatrix = this.BuildLanguageDetector();

            IMarkovMatrixLoader<double> binaryMarkovMatrixLoader = this.BuildBinaryMarkovMatrixLoader();

            string[] matrixFiles = Directory.EnumerateFiles(matricesDirectory, "*.bin").Select(file => Path.GetFileName(file)).ToArray();

            foreach (string matrixFile in matrixFiles)
            {
                string languageName = StringFormatter.FormatLanguageName(matrixFile.Substring(0, matrixFile.LastIndexOf('.')));

                IMarkovMatrix<double> matrix;
                using (FileStream fileStream = File.Open(matricesDirectory + matrixFile, FileMode.Open))
                {
                    matrix = binaryMarkovMatrixLoader.LoadMatrix(fileStream);
                }

                languageDetectorByMarkovMatrix.AddLanguage(languageName, matrix);
            }

            return languageDetectorByMarkovMatrix;
        }
    }
}
