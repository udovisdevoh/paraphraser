using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class MarkovMatrixNormalizer : IMarkovMatrixNormalizer
    {
        public IMarkovMatrix<double> Normalize(IMarkovMatrix<ulong> sourceMatrix)
        {
            MarkovMatrix<double> normalizedMatrix = new MarkovMatrix<double>();

            foreach (KeyValuePair<Tuple<char, char>, ulong> twoCharsAndCount in sourceMatrix)
            {
                Tuple<char, char> twoChars = twoCharsAndCount.Key;

                char fromChar = twoChars.Item1;
                char toChar = twoChars.Item2;

                ulong count = twoCharsAndCount.Value;

                ulong sum = sourceMatrix.GetSum(fromChar);

                if (sum != 0)
                {
                    double ratio = (double)count / (double)sum;

                    normalizedMatrix.IncrementOccurrence(fromChar, toChar, ratio);
                }
            }

            return normalizedMatrix;
        }
    }
}
