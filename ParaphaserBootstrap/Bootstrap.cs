using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageDetection;
using MarkovMatrices;
using Unity;
using Unity.Resolution;

namespace ParaphaserBootstrap
{
    public class Bootstrap
    {
        public ILanguageDetector BuildLanguageDetector()
        {
            IMarkovMatrixLoader<ulong> languageDetectionMatrixLoader = new TextMarkovMatrixLoader();
            IMarkovMatrixNormalizer markovMatrixNormalizer = new MarkovMatrixNormalizer();

            IMarkovMatrixLoader<double> normalizedLanguageDetectionMatrixLoader = new NormalizedTextMarkovMatrixLoader(languageDetectionMatrixLoader, markovMatrixNormalizer);

            LanguageDetector languageDetector = new LanguageDetector(normalizedLanguageDetectionMatrixLoader);

            return languageDetector;
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
