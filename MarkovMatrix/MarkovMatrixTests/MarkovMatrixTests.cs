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
            MarkovMatrix<ulong> markovMatrix = new MarkovMatrix<ulong>();

            // Act
            uint actualCombinedChars = markovMatrix.CombineChars('A', 'B');

            // Assert
            Assert.Equal(expectedCombinedChars, actualCombinedChars);
        }

        [Fact]
        public void Given_EmptyMatrix_ZeroOccurence()
        {
            // Arrange
            MarkovMatrix<ulong> markovMatrix = new MarkovMatrix<ulong>();
            ulong expectedOccurence = 0;

            // Act
            ulong actualOccurence = markovMatrix.GetOccurence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurence, actualOccurence);
        }

        [Fact]
        public void Given_EmptyMatrix_IncrementOccurence_CorrectOccurence()
        {
            // Arrange
            MarkovMatrix<ulong> markovMatrix = new MarkovMatrix<ulong>();
            uint expectedOccurence = 1;

            // Act
            markovMatrix.IncrementOccurence('A', 'B');
            ulong actualOccurence = markovMatrix.GetOccurence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurence, actualOccurence);
        }

        [Fact]
        public void Given_Matrix_IncrementOccurence_CorrectOccurence()
        {
            // Arrange
            MarkovMatrix<ulong> markovMatrix = new MarkovMatrix<ulong>();
            markovMatrix.IncrementOccurence('A', 'B');
            ulong expectedOccurence = 2;

            // Act
            markovMatrix.IncrementOccurence('A', 'B');
            ulong actualOccurence = markovMatrix.GetOccurence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurence, actualOccurence);
        }

        [Fact]
        public void Given_Matrix_IncrementOtherOccurence_CorrectOccurence()
        {
            // Arrange
            MarkovMatrix<ulong> markovMatrix = new MarkovMatrix<ulong>();
            markovMatrix.IncrementOccurence('B', 'C');
            uint expectedOccurence = 1;

            // Act
            markovMatrix.IncrementOccurence('A', 'B');
            ulong actualOccurence = markovMatrix.GetOccurence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurence, actualOccurence);
        }

        [Fact]
        public void Given_WrongTypeMatrix_ThrowException()
        {
            // Arrange
            MarkovMatrix<string> markovMatrix;

            // Act, Assert
            Assert.Throws<ArgumentException>(() =>
            {
                markovMatrix = new MarkovMatrix<string>();
            });
        }

        [Fact]
        public void Given_RightTypeMatrix_DoNotThrowException()
        {
            // Arrange
            MarkovMatrix<ulong> markovMatrix;

            // Act, Assert
            markovMatrix = new MarkovMatrix<ulong>();
        }
    }
}
