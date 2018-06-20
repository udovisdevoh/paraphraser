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

        public static float GetDotProduct(IMarkovMatrix<float> normalizedInputMatrix, IMarkovMatrix<float> languageMatrix)
        {
            #warning Implement
            #warning Add unit tests
            throw new NotImplementedException();
        }
    }
}
