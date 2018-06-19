using MarkovMatrices.TestHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarkovMatrices.Tests
{
    public class BinaryMarkovMatrixLoaderTests
    {
        [Fact]
        public void GivenBinaryStream_LoadMatrix_ShouldGetRightInputCount()
        {
            // Arrange
            int expectedInputCount = 2;
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(expectedInputCount, 'A', 'B', (ulong)1, 'C', 'D', (ulong)3);
            BinaryMarkovMatrixLoader<ulong> binaryMarkovMatrixLoader = new BinaryMarkovMatrixLoader<ulong>();

            // Act
            IMarkovMatrix<ulong> markovMatrix = binaryMarkovMatrixLoader.LoadMatrix(memoryStream);
            int actualInputCount = markovMatrix.InputCount;

            // Assert
            Assert.Equal(expectedInputCount, actualInputCount);

        }
        [Fact]
        public void GivenBinaryStream_LoadMatrix_ShouldGetRightOccurrenceFirstCharGroup()
        {
            // Arrange
            ulong expectedOccurrence = 2;
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(2, 'A', 'B', expectedOccurrence, 'C', 'D', (ulong)3);
            BinaryMarkovMatrixLoader<ulong> binaryMarkovMatrixLoader = new BinaryMarkovMatrixLoader<ulong>();

            // Act
            IMarkovMatrix<ulong> markovMatrix = binaryMarkovMatrixLoader.LoadMatrix(memoryStream);
            ulong actualOccurence = markovMatrix.GetOccurrence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurence);
        }

        [Fact]
        public void GivenBinaryStream_LoadMatrix_ShouldGetRightOccurrenceSecondCharGroup()
        {
            // Arrange
            ulong expectedOccurrence = 3;
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(2, 'A', 'B', (ulong)2, 'C', 'D', expectedOccurrence);
            BinaryMarkovMatrixLoader<ulong> binaryMarkovMatrixLoader = new BinaryMarkovMatrixLoader<ulong>();

            // Act
            IMarkovMatrix<ulong> markovMatrix = binaryMarkovMatrixLoader.LoadMatrix(memoryStream);
            ulong actualOccurence = markovMatrix.GetOccurrence('C', 'D');

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurence);
        }
    }
}
