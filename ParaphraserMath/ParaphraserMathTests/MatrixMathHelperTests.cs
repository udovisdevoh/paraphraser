using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ParaphraserMath.Tests
{
    public class MatrixMathHelperTests
    {
        [Fact]
        public void Given_CombineChars_GetUint()
        {
            // Arrange
            uint expectedCombinedChars = 4259906;
            // Act
            uint actualCombinedChars = MatrixMathHelper.CombineChars('A', 'B');

            // Assert
            Assert.Equal(expectedCombinedChars, actualCombinedChars);
        }

        [Fact]
        public void Given_SplitUint_GetFirstChar()
        {
            // Arrange
            char expectedChar1 = 'A';
            uint combinedChars = 4259906;

            // Act
            Tuple<char, char> actualChars = MatrixMathHelper.SplitChars(combinedChars);

            // Assert
            Assert.Equal(expectedChar1, actualChars.Item1);
        }

        [Fact]
        public void Given_SplitUint_GetSecondChar()
        {
            // Arrange
            char expectedChar2 = 'B';
            uint combinedChars = 4259906;

            // Act
            Tuple<char, char> actualChars = MatrixMathHelper.SplitChars(combinedChars);

            // Assert
            Assert.Equal(expectedChar2, actualChars.Item2);
        }

        [Fact]
        public void GivenTwoMatrices_GetDotProduct()
        {
            #warning Implement (use mock)
        }
    }
}
