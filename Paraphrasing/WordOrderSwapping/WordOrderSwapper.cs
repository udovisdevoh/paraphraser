using MarkovMatrices;
using ParaphraserMath;
using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paraphrasing
{
    public class WordOrderSwapper : IWordOrderSwapper
    {
        private IMarkovMatrix<string, double> languageWordMatrix;

        private IMarkovMatrixLoader<string, double> textMatrixLoader;

        public WordOrderSwapper(IMarkovMatrix<string, double> languageWordMatrix, IMarkovMatrixLoader<string, double> textMatrixLoader)
        {
            this.languageWordMatrix = languageWordMatrix;
            this.textMatrixLoader = textMatrixLoader;
        }

        public string SwapWordOrder(string text, HashSet<string> wordsToSwap, int offset)
        {
            Dictionary<string, double> matrixDistances = new Dictionary<string, double>();
            string previousSwappedText = null;
            string currentSwappedText = text;

            while (true)
            {
                previousSwappedText = currentSwappedText;
                currentSwappedText = StringFormatter.SwapWordOrder(previousSwappedText, wordsToSwap, offset, 1);
                if (currentSwappedText == previousSwappedText)
                {
                    break;
                }
                double matrixDistance = this.GetMatrixDistance(currentSwappedText);
                matrixDistances.Add(currentSwappedText, matrixDistance);
            }

            return matrixDistances.OrderBy(keyValuePair => keyValuePair.Value).First().Key;
        }

        private double GetMatrixDistance(string text)
        {
            IMarkovMatrix<string, double> currentTextMatrix = textMatrixLoader.LoadMatrix(text);
            return MatrixMathHelper.GetDistance(currentTextMatrix, languageWordMatrix);
        }
    }
}
