using MarkovMatrices;
using MarkovMatrices.TestHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetection.TestHelpers
{
    public static class LanguageDetectorTestHelper
    {
        public static IMarkovMatrix<float> BuildLanguageMatrix(string text)
        {
            #warning Doit plutôt retourner un mock. Enlever référence vers MarkovMatrix enlever using

            IMarkovMatrixLoader<ulong> markovMatrixLoader = new TextMarkovMatrixLoader<ulong>();
            MemoryStream memoryStream = StreamBuilder.BuildTextStream(text);
            IMarkovMatrix<ulong> markovMatrix = markovMatrixLoader.LoadMatrix(memoryStream);
            IMarkovMatrixNormalizer markovMatrixNormalizer = new MarkovMatrixNormalizer();
            IMarkovMatrix<float> normalizedMatrix = markovMatrixNormalizer.Normalize(markovMatrix);

            return normalizedMatrix;
        }
    }
}
