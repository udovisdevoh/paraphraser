using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class MarkovMatrixCharacterCombiner : IMarkovMatrixTransformer
    {
        public delegate char TransformCharacterDelegate(char character);

        private TransformCharacterDelegate transformCharacterDelegate;

        public MarkovMatrixCharacterCombiner(TransformCharacterDelegate transformCharacterDelegate)
        {
            this.transformCharacterDelegate = transformCharacterDelegate;
        }

        public IMarkovMatrix<double> Transform(IMarkovMatrix<double> sourceMatrix)
        {
            MarkovMatrix<double> newMatrix = new MarkovMatrix<double>();

            foreach (KeyValuePair<Tuple<char, char>, double> twoCharsAndCount in sourceMatrix)
            {
                Tuple<char, char> twoChars = twoCharsAndCount.Key;

                char fromChar = twoChars.Item1;
                char toChar = twoChars.Item2;

                double sourceOccurrence = twoCharsAndCount.Value;

                char fromCharNew = this.transformCharacterDelegate(fromChar);
                char toCharNew = this.transformCharacterDelegate(toChar);

                if (sourceOccurrence != 0)
                {
                    newMatrix.IncrementOccurrence(fromCharNew, toCharNew, sourceOccurrence);
                }
            }

            IMarkovMatrix<double> normalizedNewMatrix = this.Normalize(newMatrix);

            return normalizedNewMatrix;
        }

        public IMarkovMatrix<double> Normalize(IMarkovMatrix<double> sourceMatrix)
        {
            MarkovMatrix<double> normalizedMatrix = new MarkovMatrix<double>();

            foreach (KeyValuePair<Tuple<char, char>, double> twoCharsAndCount in sourceMatrix)
            {
                Tuple<char, char> twoChars = twoCharsAndCount.Key;

                char fromChar = twoChars.Item1;
                char toChar = twoChars.Item2;

                double count = twoCharsAndCount.Value;

                double sum = sourceMatrix.GetSum(fromChar);

                if (sum != 0)
                {
                    double ratio = (double)count / (double)sum;

                    normalizedMatrix.IncrementOccurrence(fromChar, toChar, (double)ratio);
                }
            }

            return normalizedMatrix;
        }
    }
}
