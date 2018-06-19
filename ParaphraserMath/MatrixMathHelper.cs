using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaphraserMath
{
    public static class MatrixMathHelper
    {
        public static uint CombineChars(char fromChar, char toChar)
        {
            return ((uint)fromChar * 65536) + (uint)toChar;
        }

        public static Tuple<char, char> SplitChars(uint combinedChars)
        {
            #warning Todo add unit tests
            uint largeComponent = combinedChars / 65536;

            char char1 = (char)largeComponent;
            char char2 = (char)(combinedChars - largeComponent);

            return new Tuple<char, char>(char1, char2);
        }
    }
}
