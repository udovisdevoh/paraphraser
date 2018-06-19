using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class MarkovMatrixNormalizer : IMarkovMatrixNormalizer
    {
        public IMarkovMatrix<float> Normalize(IMarkovMatrix<ulong> sourceMatrix)
        {
            MarkovMatrix<float> normalizedMatrix = new MarkovMatrix<float>();

            foreach (KeyValuePair<Tuple<char, char>, ulong> twoCharsAndCount in sourceMatrix)
            {
                Tuple<char, char> twoChars = twoCharsAndCount.Key;

                char fromChar = twoChars.Item1;
                char toChar = twoChars.Item2;

                ulong count = twoCharsAndCount.Value;

                ulong sum = sourceMatrix.GetSum(fromChar);

                if (sum != 0)
                {
                    double ratio = (float)count / (float)sum;

                    normalizedMatrix.IncrementOccurrence(fromChar, toChar, (float)ratio);
                }
            }

            return normalizedMatrix;
        }
    }
}
