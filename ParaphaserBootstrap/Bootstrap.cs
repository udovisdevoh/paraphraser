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
        public LanguageDetectorByMarkovMatrix BuildLanguageDetectorByMarkovMatrix()
        {
            IMarkovMatrixLoader<char, ulong> languageDetectionMatrixLoader = new TextMarkovMatrixLoader();
            IMarkovMatrixNormalizer<char> markovMatrixNormalizer = new MarkovMatrixNormalizer();

            IMarkovMatrixLoader<char, double> normalizedLanguageDetectionMatrixLoader = new NormalizedTextMarkovMatrixLoader(languageDetectionMatrixLoader, markovMatrixNormalizer);

            LanguageDetectorByMarkovMatrix languageDetectorByMarkovMatrix = new LanguageDetectorByMarkovMatrix(normalizedLanguageDetectionMatrixLoader);

            return languageDetectorByMarkovMatrix;
        }

        public ICompositeLanguageDetector BuildCompositeLanguageDetector()
        {
            CompositeLanguageDetector compositeLanguageDetector = new CompositeLanguageDetector();

            return compositeLanguageDetector;
        }

        public ILanguageDetector BuildLanguageDetectorNoDiacritics()
        {
            IMarkovMatrixLoader<char, ulong> languageDetectionMatrixLoader = new TextMarkovMatrixLoader();
            IMarkovMatrixNormalizer<char> markovMatrixNormalizer = new MarkovMatrixNormalizer();

            IMarkovMatrixLoader<char, double> normalizedLanguageDetectionMatrixLoader = new NormalizedTextMarkovMatrixLoader(languageDetectionMatrixLoader, markovMatrixNormalizer);
            IMarkovMatrixTransformer<char> markovMatrixCharacterCombiner = new MarkovMatrixCharacterCombiner(letter => StringFormatter.RemoveDiacritics(letter));

            LanguageDetectorByMarkovMatrix languageDetectorByMarkovMatrix = new LanguageDetectorNoDiacritics(normalizedLanguageDetectionMatrixLoader, markovMatrixCharacterCombiner);

            return languageDetectorByMarkovMatrix;
        }

        public ILanguageDetector BuildLanguageDetectorByLeastCorrection(string spellCheckDictionaries)
        {
            IMarkovMatrixLoader<char, ulong> languageDetectionMatrixLoader = new TextMarkovMatrixLoader();
            IMarkovMatrixNormalizer<char> markovMatrixNormalizer = new MarkovMatrixNormalizer();
            IMarkovMatrixLoader<char, double> normalizedLanguageDetectionMatrixLoader = new NormalizedTextMarkovMatrixLoader(languageDetectionMatrixLoader, markovMatrixNormalizer);

            LanguageDetectorByLeastCorrection languageDetectorByLeastCorrection = new LanguageDetectorByLeastCorrection(normalizedLanguageDetectionMatrixLoader);

            string[] dictionaryFiles = Directory.EnumerateFiles(spellCheckDictionaries, "*.dic").Select(file => Path.GetFileName(file)).ToArray();

            Parallel.ForEach(dictionaryFiles, (dictionaryFile) =>
            {
                string languageName = dictionaryFile.Substring(0, dictionaryFile.LastIndexOf("."));
                ISpellChecker spellChecker = this.BuildSpellChecker(spellCheckDictionaries, languageName);

                languageDetectorByLeastCorrection.AddLanguage(languageName, spellChecker);
            });

            return languageDetectorByLeastCorrection;
        }

        public ILanguageDetector BuildLanguageDetectorByHash(string wordListsFolder)
        {
            LanguageDetectorByHash languageDetectorByHash = new LanguageDetectorByHash();

            string[] wordListFiles = Directory.EnumerateFiles(wordListsFolder, "*.txt").Select(file => Path.GetFileName(file)).ToArray();

            Parallel.ForEach(wordListFiles, (wordListFile) =>
            {
            /*foreach (string wordListFile in wordListFiles)
            {*/
                string languageName = wordListFile.Substring(0, wordListFile.LastIndexOf("."));
                Dictionary<string, double> wordList;
                using (FileStream stream = File.Open(wordListsFolder + wordListFile, FileMode.Open))
                {
                    wordList = WordListReader.BuildWordCountProbability(stream);
                }

                lock (languageDetectorByHash)
                {
                    languageDetectorByHash.AddLanguage(languageName, wordList);
                }
            //}
            });

            return languageDetectorByHash;
        }

        public ISpellChecker BuildSpellChecker(string dictionary, string languageName)
        {
            ISpellChecker spellChecker = new SpellChecker(dictionary, languageName);
            return spellChecker;
        }

        public IMarkovMatrixLoader<char, double> BuildBinaryMarkovMatrixLoader()
        {
            IMarkovMatrixLoader<char, double> binaryMarkovMatrixLoader = new BinaryMarkovMatrixLoader();
            return binaryMarkovMatrixLoader;
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

        public ILanguageDetector BuildLanguageDetectorByDictionary(string spellCheckDictionaries)
        {
            LanguageDetectorByDictionary languageDetectorByDictionary = new LanguageDetectorByDictionary();

            string[] dictionaryFiles = Directory.EnumerateFiles(spellCheckDictionaries, "*.dic").Select(file => Path.GetFileName(file)).ToArray();

            Parallel.ForEach(dictionaryFiles, (dictionaryFile) =>
            {
                string languageName = dictionaryFile.Substring(0, dictionaryFile.LastIndexOf("."));
                ISpellChecker spellChecker = this.BuildSpellChecker(spellCheckDictionaries, languageName);

                languageDetectorByDictionary.AddLanguage(languageName, spellChecker);
            });

            return languageDetectorByDictionary;
        }

        public ILanguageDetector BuildLanguageDetectorByMarkovMatrix(string matricesDirectory)
        {
            LanguageDetectorByMarkovMatrix languageDetectorByMarkovMatrix = this.BuildLanguageDetectorByMarkovMatrix();

            IMarkovMatrixLoader<char, double> binaryMarkovMatrixLoader = this.BuildBinaryMarkovMatrixLoader();

            string[] matrixFiles = Directory.EnumerateFiles(matricesDirectory, "*.bin").Select(file => Path.GetFileName(file)).ToArray();

            foreach (string matrixFile in matrixFiles)
            {
                string languageName = StringFormatter.FormatLanguageName(matrixFile.Substring(0, matrixFile.LastIndexOf('.')));

                IMarkovMatrix<char, double> matrix;
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
