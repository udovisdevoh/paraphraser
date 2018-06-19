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
            #warning Implement
            #warning Add unit tests
            MarkovMatrix<double> normalizedMatrix = new MarkovMatrix<double>();

            foreach (KeyValuePair<Tuple<char, char>, ulong> twoCharsAndCount in sourceMatrix)
            {
                Tuple<char, char> twoChars = twoCharsAndCount.Key;
                ulong count = twoCharsAndCount.Value;
            }

            return normalizedMatrix;
        }
    }
}
