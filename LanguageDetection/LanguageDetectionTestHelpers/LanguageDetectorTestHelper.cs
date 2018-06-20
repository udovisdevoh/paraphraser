using MarkovMatrices;
using Moq;
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
        public static IMarkovMatrix<float> BuildEnglishLanguageMatrixMock()
        {
            return LanguageDetectorTestHelper.BuildLanguageMatrixMock(1f, 0f);
        }

        public static IMarkovMatrix<float> BuildFrenchLanguageMatrixMock()
        {
            return LanguageDetectorTestHelper.BuildLanguageMatrixMock(0f, 1f);
        }

        private static IMarkovMatrix<float> BuildLanguageMatrixMock(float english, float french)
        {
            Mock<IMarkovMatrix<float>> markovMatrixMockFactory = new Mock<IMarkovMatrix<float>>();
            List<KeyValuePair<Tuple<char, char>, float>> values = new List<KeyValuePair<Tuple<char, char>, float>>();

            LanguageDetectorTestHelper.AddMatrixValue(markovMatrixMockFactory, values, 't', 'h', english);
            LanguageDetectorTestHelper.AddMatrixValue(markovMatrixMockFactory, values, 'i', 's', english);
            LanguageDetectorTestHelper.AddMatrixValue(markovMatrixMockFactory, values, 'c', 'e', french);
            LanguageDetectorTestHelper.AddMatrixValue(markovMatrixMockFactory, values, 'c', '\'', french);
            LanguageDetectorTestHelper.AddMatrixValue(markovMatrixMockFactory, values, 'h', 'e', 0.5f);

            markovMatrixMockFactory.Setup(matrix => matrix.GetEnumerator()).Returns(() => values.GetEnumerator());

            return markovMatrixMockFactory.Object;
        }

        public static IMarkovMatrixLoader<float> BuildNormalizedTextMarkovMatrixLoader(IMarkovMatrix<float> outputMatrix)
        {
            Mock<IMarkovMatrixLoader<float>> matrixLoaderMockFactory = new Mock<IMarkovMatrixLoader<float>>();
            matrixLoaderMockFactory.Setup(matrixLoader => matrixLoader.LoadMatrix(It.IsAny<Stream>())).Returns(outputMatrix);
            return matrixLoaderMockFactory.Object;
        }

        private static void AddMatrixValue(Mock<IMarkovMatrix<float>> markovMatrixMockFactory,
            List<KeyValuePair<Tuple<char, char>, float>> iEnumerable,
            char fromChar,
            char toChar,
            float value)
        {
            markovMatrixMockFactory.Setup(matrix => matrix.GetOccurrence(fromChar, toChar)).Returns(value);
            Tuple<char, char> characters = new Tuple<char, char>(fromChar, toChar);
            iEnumerable.Add(new KeyValuePair<Tuple<char, char>, float>(characters, value));
        }
    }
}
