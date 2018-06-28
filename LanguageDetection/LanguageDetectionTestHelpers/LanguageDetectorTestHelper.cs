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
        public static IMarkovMatrix<double> BuildEnglishLanguageMatrixMock()
        {
            return LanguageDetectorTestHelper.BuildLanguageMatrixMock(1.0, 0.0);
        }

        public static IMarkovMatrix<double> BuildFrenchLanguageMatrixMock()
        {
            return LanguageDetectorTestHelper.BuildLanguageMatrixMock(0.0, 1.0);
        }

        private static IMarkovMatrix<double> BuildLanguageMatrixMock(double english, double french)
        {
            Mock<IMarkovMatrix<double>> markovMatrixMockFactory = new Mock<IMarkovMatrix<double>>();
            List<KeyValuePair<Tuple<char, char>, double>> values = new List<KeyValuePair<Tuple<char, char>, double>>();

            LanguageDetectorTestHelper.AddMatrixValue(markovMatrixMockFactory, values, 't', 'h', english);
            LanguageDetectorTestHelper.AddMatrixValue(markovMatrixMockFactory, values, 'i', 's', english);
            LanguageDetectorTestHelper.AddMatrixValue(markovMatrixMockFactory, values, 'c', 'e', french);
            LanguageDetectorTestHelper.AddMatrixValue(markovMatrixMockFactory, values, 'c', '\'', french);
            LanguageDetectorTestHelper.AddMatrixValue(markovMatrixMockFactory, values, 'h', 'e', 0.5f);

            markovMatrixMockFactory.Setup(matrix => matrix.GetEnumerator()).Returns(() => values.GetEnumerator());

            return markovMatrixMockFactory.Object;
        }

        public static IMarkovMatrixLoader<double> BuildNormalizedTextMarkovMatrixLoader(IMarkovMatrix<double> outputMatrix)
        {
            Mock<IMarkovMatrixLoader<double>> matrixLoaderMockFactory = new Mock<IMarkovMatrixLoader<double>>();
            matrixLoaderMockFactory.Setup(matrixLoader => matrixLoader.LoadMatrix(It.IsAny<Stream>())).Returns(outputMatrix);
            matrixLoaderMockFactory.Setup(matrixLoader => matrixLoader.LoadMatrix(It.IsAny<string>())).Returns(outputMatrix);
            return matrixLoaderMockFactory.Object;
        }

        private static void AddMatrixValue(Mock<IMarkovMatrix<double>> markovMatrixMockFactory,
            List<KeyValuePair<Tuple<char, char>, double>> iEnumerable,
            char fromChar,
            char toChar,
            double value)
        {
            markovMatrixMockFactory.Setup(matrix => matrix.GetOccurrence(fromChar, toChar)).Returns(value);
            Tuple<char, char> characters = new Tuple<char, char>(fromChar, toChar);
            iEnumerable.Add(new KeyValuePair<Tuple<char, char>, double>(characters, value));
        }
    }
}
