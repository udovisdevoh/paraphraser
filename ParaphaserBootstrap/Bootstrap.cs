using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageDetection;
using MarkovMatrices;
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

        public ILanguageDetector BuildCompositeLanguageDetector()
        {
            CompositeLanguageDetector compositeLanguageDetector = new CompositeLanguageDetector();

            compositeLanguageDetector.AddLanguageDetector(this.BuildLanguageDetector());
            compositeLanguageDetector.AddLanguageDetector(this.BuildLanguageDetectorNoDiacritics());

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
    }
}
