using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarkovMatrices.Tests
{
    public class MarkovMatrixTests
    {
        [Fact]
        public void Given_Matrix_CombineChars_GetUint()
        {
            // Arrange
            uint expectedCombinedChars = 4259906;
            MarkovMatrix markovMatrix = new MarkovMatrix();

            // Act
            uint actualCombinedChars = markovMatrix.CombineChars('A', 'B');

            // Assert
            Assert.Equal(expectedCombinedChars, actualCombinedChars);
        }

        [Fact]
        public void Given_EmptyMatrix_ZeroOccurence()
        {
            // Arrange
            MarkovMatrix markovMatrix = new MarkovMatrix();
            uint expectedOccurence = 0;

            // Act
            uint actualOccurence = markovMatrix.GetOccurence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurence, actualOccurence);
        }

        [Fact]
        public void Given_EmptyMatrix_IncrementOccurence_CorrectOccurence()
        {
            // Arrange
            MarkovMatrix markovMatrix = new MarkovMatrix();
            uint expectedOccurence = 1;

            // Act
            markovMatrix.IncrementOccurence('A', 'B');
            uint actualOccurence = markovMatrix.GetOccurence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurence, actualOccurence);
        }

        [Fact]
        public void Given_Matrix_IncrementOccurence_CorrectOccurence()
        {
            // Arrange
            MarkovMatrix markovMatrix = new MarkovMatrix();
            markovMatrix.IncrementOccurence('A', 'B');
            uint expectedOccurence = 2;

            // Act
            markovMatrix.IncrementOccurence('A', 'B');
            uint actualOccurence = markovMatrix.GetOccurence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurence, actualOccurence);
        }

        [Fact]
        public void Given_Matrix_IncrementOtherOccurence_CorrectOccurence()
        {
            // Arrange
            MarkovMatrix markovMatrix = new MarkovMatrix();
            markovMatrix.IncrementOccurence('B', 'C');
            uint expectedOccurence = 1;

            // Act
            markovMatrix.IncrementOccurence('A', 'B');
            uint actualOccurence = markovMatrix.GetOccurence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurence, actualOccurence);
        }
    }
}
