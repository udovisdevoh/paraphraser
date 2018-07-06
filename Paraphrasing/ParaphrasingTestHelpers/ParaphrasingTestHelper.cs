using MarkovMatrices;
using Moq;
using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paraphrasing.Tests
{
    public static class ParaphrasingTestHelper
    {
        public static IWordOrderSwapper BuildWordSwapperMock()
        {
            Mock<IWordOrderSwapper> wordOrderSwapperMockFactory = new Mock<IWordOrderSwapper>();
            wordOrderSwapperMockFactory.Setup(swapper => swapper.SwapWordOrder(It.IsAny<string>(), It.IsAny<HashSet<string>>(), It.IsAny<int>())).Returns<string, HashSet<string>, int>((text, wordsToSwap, offset) => StringFormatter.SwapWordOrder(text, wordsToSwap, offset, 1));

            IWordOrderSwapper wordOrderSwapper = wordOrderSwapperMockFactory.Object;
            return wordOrderSwapper;
        }

        public static IMarkovMatrix<string, double> BuildLanguageWordMatrixMock(string fromWord1,
            string toWord1,
            double occurrence1,
            string fromWord2,
            string toWord2,
            double occurrence2,
            string fromWord3,
            string toWord3,
            double occurrence3,
            string fromWord4,
            string toWord4,
            double occurrence4)
        {
            Mock<IMarkovMatrix<string, double>> matrixMockFactory = new Mock<IMarkovMatrix<string, double>>();
            matrixMockFactory.Setup(matrix => matrix.GetOccurrence(fromWord1, toWord1)).Returns(occurrence1);
            matrixMockFactory.Setup(matrix => matrix.GetOccurrence(fromWord2, toWord2)).Returns(occurrence2);
            matrixMockFactory.Setup(matrix => matrix.GetOccurrence(fromWord3, toWord3)).Returns(occurrence3);
            matrixMockFactory.Setup(matrix => matrix.GetOccurrence(fromWord4, toWord4)).Returns(occurrence4);

            return matrixMockFactory.Object;
        }

        public static IMarkovMatrixLoader<string, double> BuildStringMarkovMatrixLoader()
        {
            IMarkovMatrixLoader<string, double> markovMatrixLoader = new StringMarkovMatrixLoaderFromText();
            return markovMatrixLoader;
        }
    }
}
