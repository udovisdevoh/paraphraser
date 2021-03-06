﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class MarkovMatrixNormalizer : IMarkovMatrixNormalizer<char>
    {
        public IMarkovMatrix<char, double> Normalize(IMarkovMatrix<char, ulong> sourceMatrix)
        {
            CharMarkovMatrix<double> normalizedMatrix = new CharMarkovMatrix<double>();

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

                    normalizedMatrix.IncrementOccurrence(fromChar, toChar, (double)ratio);
                }
            }

            return normalizedMatrix;
        }

        public IMarkovMatrix<char, double> Convert(IMarkovMatrix<char, ulong> sourceMatrix)
        {
            CharMarkovMatrix<double> convertedMatrix = new CharMarkovMatrix<double>();

            foreach (KeyValuePair<Tuple<char, char>, ulong> twoCharsAndCount in sourceMatrix)
            {
                Tuple<char, char> twoChars = twoCharsAndCount.Key;

                char fromChar = twoChars.Item1;
                char toChar = twoChars.Item2;

                ulong count = twoCharsAndCount.Value;

                double ratio = (double)count;

                convertedMatrix.IncrementOccurrence(fromChar, toChar, (double)ratio);
            }

            return convertedMatrix;
        }
    }
}
