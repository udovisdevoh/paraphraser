using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringManipulation
{
    public static class RomanNumerals
    {
        private static HashSet<string> romanNumeralHash;

        static RomanNumerals()
        {
            RomanNumerals.romanNumeralHash = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            for (int number = 0; number < 4000; ++number)
            {
                string romanNumeral = RomanNumerals.ToRomanNumeral(number);
                romanNumeralHash.Add(romanNumeral.ToUpperInvariant());
            }
        }

        public static string ToRomanNumeral(int number)
        {
            if (number < 0 || number > 3999)
            {
                throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            }

            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRomanNumeral(number - 1000);
            if (number >= 900) return "CM" + ToRomanNumeral(number - 900);
            if (number >= 500) return "D" + ToRomanNumeral(number - 500);
            if (number >= 400) return "CD" + ToRomanNumeral(number - 400);
            if (number >= 100) return "C" + ToRomanNumeral(number - 100);
            if (number >= 90) return "XC" + ToRomanNumeral(number - 90);
            if (number >= 50) return "L" + ToRomanNumeral(number - 50);
            if (number >= 40) return "XL" + ToRomanNumeral(number - 40);
            if (number >= 10) return "X" + ToRomanNumeral(number - 10);
            if (number >= 9) return "IX" + ToRomanNumeral(number - 9);
            if (number >= 5) return "V" + ToRomanNumeral(number - 5);
            if (number >= 4) return "IV" + ToRomanNumeral(number - 4);
            if (number >= 1) return "I" + ToRomanNumeral(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }

        public static bool IsRomanNumeral(string romanNumeral)
        {
            romanNumeral = romanNumeral.ToUpperInvariant().Trim();
            return RomanNumerals.romanNumeralHash.Contains(romanNumeral);
        }
    }
}
