using ParaphraserMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarkovMatrices.Tests
{
    public class CharMarkovMatrixTests
    {
        [Fact]
        public void GivenEmptyMatrix_ZeroOccurrence()
        {
            // Arrange
            CharMarkovMatrix<ulong> markovMatrix = new CharMarkovMatrix<ulong>();
            ulong expectedOccurrence = 0;

            // Act
            ulong actualOccurrence = markovMatrix.GetOccurrence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurrence);
        }

        [Fact]
        public void GivenEmptyMatrix_IncrementOccurrence_CorrectOccurrence()
        {
            // Arrange
            CharMarkovMatrix<ulong> markovMatrix = new CharMarkovMatrix<ulong>();
            uint expectedOccurrence = 1;

            // Act
            markovMatrix.IncrementOccurrence('A', 'B');
            ulong actualOccurrence = markovMatrix.GetOccurrence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurrence);
        }

        [Fact]
        public void GivenMatrix_IncrementOccurrence_CorrectOccurrence()
        {
            // Arrange
            CharMarkovMatrix<ulong> markovMatrix = new CharMarkovMatrix<ulong>();
            markovMatrix.IncrementOccurrence('A', 'B');
            ulong expectedOccurrence = 2;

            // Act
            markovMatrix.IncrementOccurrence('A', 'B');
            ulong actualOccurrence = markovMatrix.GetOccurrence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurrence);
        }

        [Fact]
        public void GivenMatrix_IncrementOccurrenceWithIncrementation_CorrectOccurrence()
        {
            // Arrange
            CharMarkovMatrix<ulong> markovMatrix = new CharMarkovMatrix<ulong>();
            markovMatrix.IncrementOccurrence('A', 'B', 5);
            ulong expectedOccurrence = 6;

            // Act
            markovMatrix.IncrementOccurrence('A', 'B');
            ulong actualOccurrence = markovMatrix.GetOccurrence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurrence);
        }

        [Fact]
        public void GivenMatrix_IncrementOtherOccurrence_CorrectOccurrence()
        {
            // Arrange
            CharMarkovMatrix<ulong> markovMatrix = new CharMarkovMatrix<ulong>();
            markovMatrix.IncrementOccurrence('B', 'C');
            uint expectedOccurrence = 1;

            // Act
            markovMatrix.IncrementOccurrence('A', 'B');
            ulong actualOccurrence = markovMatrix.GetOccurrence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurrence);
        }

        [Fact]
        public void GivenWrongTypeMatrix_ThrowException()
        {
            // Arrange
            CharMarkovMatrix<string> markovMatrix;

            // Act, Assert
            Assert.Throws<ArgumentException>(() =>
            {
                markovMatrix = new CharMarkovMatrix<string>();
            });
        }

        [Fact]
        public void GivenRightTypeMatrix_DoNotThrowException()
        {
            // Arrange
            CharMarkovMatrix<ulong> markovMatrix;

            // Act, Assert
            markovMatrix = new CharMarkovMatrix<ulong>();
        }

        [Fact]
        public void GivenMatrix_Increment_ShouldGetSum()
        {
            // Arrange
            CharMarkovMatrix<ulong> markovMatrix = new CharMarkovMatrix<ulong>();
            markovMatrix.IncrementOccurrence('A', 'B');
            markovMatrix.IncrementOccurrence('A', 'C');
            markovMatrix.IncrementOccurrence('A', 'D');
            uint expectedSum = 3;

            // Act
            ulong actualSum = markovMatrix.GetSum('A');

            // Assert
            Assert.Equal(expectedSum, actualSum);
        }
    }
}
