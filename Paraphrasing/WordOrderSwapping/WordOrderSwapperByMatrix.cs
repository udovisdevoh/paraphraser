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
    public class WordOrderSwapperByMatrix : IWordOrderSwapper
    {
        private IMarkovMatrix<string, double> languageWordMatrix;

        private IMarkovMatrixLoader<string, double> textMatrixLoader;

        public WordOrderSwapperByMatrix(IMarkovMatrix<string, double> languageWordMatrix, IMarkovMatrixLoader<string, double> textMatrixLoader)
        {
            this.languageWordMatrix = languageWordMatrix;
            this.textMatrixLoader = textMatrixLoader;
        }

        public string SwapWordOrder(string text, HashSet<string> wordsToSwap, HashSet<string> wordsToSkip, int offset)
        {
            Dictionary<string, double> matrixDistances = new Dictionary<string, double>();
            string previousSwappedText = null;
            string currentSwappedText = text;

            string[] words = WordExtractor.GetWordsAndPunctuationTokens(text, '\'');
            int wordCount = words.Length;
            int swapCount = 0;

            while (true)
            {
                previousSwappedText = currentSwappedText;
                currentSwappedText = StringFormatter.SwapWordOrder(previousSwappedText, wordsToSwap, wordsToSkip, offset, 1);
                if (currentSwappedText == previousSwappedText)
                {
                    break;
                }
                double matrixDistance = this.GetMatrixDistanceFromLanguage(currentSwappedText);
                matrixDistances[currentSwappedText] = matrixDistance;
                ++swapCount;

                if (swapCount > wordCount)
                {
                    break;
                }
            }

            return matrixDistances.OrderBy(keyValuePair => keyValuePair.Value).First().Key;
        }

        private double GetMatrixDistanceFromLanguage(string text)
        {
            IMarkovMatrix<string, double> currentTextMatrix = textMatrixLoader.LoadMatrix(text + " ");
            return MatrixMathHelper.GetDistance(currentTextMatrix, languageWordMatrix);
        }
    }
}
