using ParaphraserMath;
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
        public void Given_EmptyMatrix_ZeroOccurrence()
        {
            // Arrange
            MarkovMatrix<ulong> markovMatrix = new MarkovMatrix<ulong>();
            ulong expectedOccurrence = 0;

            // Act
            ulong actualOccurrence = markovMatrix.GetOccurrence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurrence);
        }

        [Fact]
        public void Given_EmptyMatrix_IncrementOccurrence_CorrectOccurrence()
        {
            // Arrange
            MarkovMatrix<ulong> markovMatrix = new MarkovMatrix<ulong>();
            uint expectedOccurrence = 1;

            // Act
            markovMatrix.IncrementOccurrence('A', 'B');
            ulong actualOccurrence = markovMatrix.GetOccurrence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurrence);
        }

        [Fact]
        public void Given_Matrix_IncrementOccurrence_CorrectOccurrence()
        {
            // Arrange
            MarkovMatrix<ulong> markovMatrix = new MarkovMatrix<ulong>();
            markovMatrix.IncrementOccurrence('A', 'B');
            ulong expectedOccurrence = 2;

            // Act
            markovMatrix.IncrementOccurrence('A', 'B');
            ulong actualOccurrence = markovMatrix.GetOccurrence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurrence);
        }

        [Fact]
        public void Given_Matrix_IncrementOtherOccurrence_CorrectOccurrence()
        {
            // Arrange
            MarkovMatrix<ulong> markovMatrix = new MarkovMatrix<ulong>();
            markovMatrix.IncrementOccurrence('B', 'C');
            uint expectedOccurrence = 1;

            // Act
            markovMatrix.IncrementOccurrence('A', 'B');
            ulong actualOccurrence = markovMatrix.GetOccurrence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurrence);
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
