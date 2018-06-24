﻿using MarkovMatrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaphraserMath
{
    public static class MatrixMathHelper
    {
        private const int sixteenBitsMaxValue = 65536;

        public static uint CombineChars(char fromChar, char toChar)
        {
            return ((uint)fromChar * sixteenBitsMaxValue) + (uint)toChar;
        }

        public static Tuple<char, char> SplitChars(uint combinedChars)
        {
            uint largeComponent = combinedChars / sixteenBitsMaxValue;

            char char1 = (char)largeComponent;
            char char2 = (char)(combinedChars - (largeComponent * sixteenBitsMaxValue));

            return new Tuple<char, char>(char1, char2);
        }

        public static double GetDotProduct(IMarkovMatrix<double> smallMatrix, IMarkovMatrix<double> largeMatrix)
        {
            double dotProduct = 0.0;

            foreach (KeyValuePair<Tuple<char, char>, double> charsAndProbability in smallMatrix)
            {
                Tuple<char, char> characters = charsAndProbability.Key;
                double probability = charsAndProbability.Value;
                char char1 = characters.Item1;
                char char2 = characters.Item2;

                double otherMatrixProbability = largeMatrix.GetOccurrence(char1, char2);

                dotProduct += (probability * otherMatrixProbability);
            }

            return dotProduct;
        }
    }
}
