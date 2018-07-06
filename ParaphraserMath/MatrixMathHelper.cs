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

        private const uint thirtyTwoBitsMaxValue = uint.MaxValue;

        public static ulong CombineChars(char fromChar, char toChar)
        {
            return MatrixMathHelper.CombineUInts(fromChar, toChar);
        }

        public static ulong CombineUInts(uint fromNumber, uint toNumber)
        {
            return ((ulong)fromNumber * thirtyTwoBitsMaxValue) + (ulong)toNumber;
        }

        public static Tuple<char, char> SplitChars(ulong combinedChars)
        {
            Tuple<uint, uint> tuple = MatrixMathHelper.SplitUInts(combinedChars);
            return new Tuple<char, char>((char)tuple.Item1, (char)tuple.Item2);
        }

        public static Tuple<uint, uint> SplitUInts(ulong combinedUInts)
        {
            ulong largeComponent = combinedUInts / thirtyTwoBitsMaxValue;

            uint uInt1 = (uint)largeComponent;
            uint uInt2 = (uint)(combinedUInts - (largeComponent * thirtyTwoBitsMaxValue));

            return new Tuple<uint, uint>(uInt1, uInt2);
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
