using MarkovMatrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaphraserMath
{
    public static class MatrixMathHelper
    {
        public delegate double MatrixComparer(double probability1, double probability2);

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

        public static double GetDotProduct<TKey>(IMarkovMatrix<TKey, double> smallMatrix, IMarkovMatrix<TKey, double> largeMatrix)
        {
            double dotProduct = 0.0;

            foreach (KeyValuePair<Tuple<TKey, TKey>, double> charsAndProbability in smallMatrix)
            {
                Tuple<TKey, TKey> characters = charsAndProbability.Key;
                double probability = charsAndProbability.Value;
                TKey sourceComponent = characters.Item1;
                TKey destinationComponent = characters.Item2;

                double otherMatrixProbability = largeMatrix.GetOccurrence(sourceComponent, destinationComponent);

                dotProduct += probability * otherMatrixProbability;
            }

            return dotProduct;
        }

        public static double GetStandardDeviation(double[] numbers)
        {
            double standardDeviation = 0;
            if (numbers.Length > 0)
            {
                double average = numbers.Average();
                double sum = numbers.Sum(d => Math.Pow(d - average, 2));
                standardDeviation = Math.Sqrt((sum) / (numbers.Length - 1));
            }
            return standardDeviation;
        }

        public static double GetDistance<TKey>(IMarkovMatrix<TKey, double> smallMatrix, IMarkovMatrix<TKey, double> largeMatrix)
        {
            double distance = 0.0;
            double maxDistance = 0.0;

            foreach (KeyValuePair<Tuple<TKey, TKey>, double> charsAndProbability in smallMatrix)
            {
                Tuple<TKey, TKey> characters = charsAndProbability.Key;
                double probability = charsAndProbability.Value;
                TKey sourceComponent = characters.Item1;
                TKey destinationComponent = characters.Item2;

                double otherMatrixProbability = largeMatrix.GetOccurrence(sourceComponent, destinationComponent);

                distance += Math.Pow(Math.Abs(probability - otherMatrixProbability), 2.0);
                maxDistance += 1.0;
            }

            maxDistance = Math.Sqrt(maxDistance);

            if (maxDistance == 0)
            {
                return 1.0;
            }

            return Math.Sqrt(distance) / maxDistance;
        }
    }
}
