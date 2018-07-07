using MarkovMatrices;
using Moq;
using StringManipulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paraphrasing.Tests
{
    public static class ParaphrasingTestHelper
    {
        private static IWordOrderSwapper wordOrderSwapperByMatrix = null;

        public static IWordOrderSwapper BuildWordOrderSwapper()
        {
            return new WordOrderSwapper();
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

        public static IWordOrderSwapper BuildWordOrderSwapperByMatrix(string matrixFileName)
        {
            if (ParaphrasingTestHelper.wordOrderSwapperByMatrix == null)
            {
                IMarkovMatrix<string, double> languageWordMatrix = ParaphrasingTestHelper.BuildLanguageWordMatrix(matrixFileName);
                IMarkovMatrixLoader<string, double> textMatrixLoader = ParaphrasingTestHelper.BuildStringMarkovMatrixLoader();
                ParaphrasingTestHelper.wordOrderSwapperByMatrix = new WordOrderSwapperByMatrix(languageWordMatrix, textMatrixLoader);
            }

            return ParaphrasingTestHelper.wordOrderSwapperByMatrix;
        }

        private static IMarkovMatrix<string, double> BuildLanguageWordMatrix(string matrixFileName)
        {
            IMarkovMatrixLoader<string, double> binaryMatrixLoader = new BinaryStringMarkovMatrixLoader();
            using (FileStream stream = new FileStream(matrixFileName, FileMode.Open))
            {
                return binaryMatrixLoader.LoadMatrix(stream);
            }
        }
    }
}
