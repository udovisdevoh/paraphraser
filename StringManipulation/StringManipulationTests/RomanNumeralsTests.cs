using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StringManipulation.Tests
{
    public class RomanNumeralsTests
    {
        [Fact]
        public void GivenNumberTooLarge_ToRomanNumeral_ShouldThrow()
        {
            // Arrange
            int number = 787345;

            // Act, Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                string romanNumeral = RomanNumerals.ToRomanNumeral(number);
            });
        }

        [Fact]
        public void GivenNumberTooSmall_ToRomanNumeral_ShouldThrow()
        {
            // Arrange
            int number = -1;

            // Act, Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                string romanNumeral = RomanNumerals.ToRomanNumeral(number);
            });
        }

        [Fact]
        public void GivenNumber_ToRomanNumeral_ShouldConvertToRomanNumeral()
        {
            // Arrange
            int number = 16;
            string expectedRomanNumeral = "XVI";

            // Act
            string actualRomanNumeral = RomanNumerals.ToRomanNumeral(number);

            // Assert
            Assert.Equal(expectedRomanNumeral, actualRomanNumeral);
        }

        [Fact]
        public void GivenRomanNumeral_IsRomanNumeral_ShouldDetectRomanNumeral()
        {
            // Arrange
            string romanNumeral = "XVI";

            // Act
            bool result = RomanNumerals.IsRomanNumeral(romanNumeral);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenRomanNumeralWithSpaceAndLowerCase_IsRomanNumeral_ShouldDetectRomanNumeral()
        {
            // Arrange
            string romanNumeral = " XviI  ";

            // Act
            bool result = RomanNumerals.IsRomanNumeral(romanNumeral);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenNonRomanNumeral_IsRomanNumeral_ShouldNotDetectRomanNumeral()
        {
            // Arrange
            string romanNumeral = "vil";

            // Act
            bool result = RomanNumerals.IsRomanNumeral(romanNumeral);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GivenFakeRomanNumeral_IsRomanNumeral_ShouldNotDetectRomanNumeral()
        {
            // Arrange
            string romanNumeral = "VIX";

            // Act
            bool result = RomanNumerals.IsRomanNumeral(romanNumeral);

            // Assert
            Assert.False(result);
        }
    }
}
