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
    public class BinaryMarkovMatrixLoaderSaverTests
    {
        [Fact]
        public void Given_BinaryStream_LoadMatrix_ShouldGetRightInputCount()
        {
            // Arrange
            int expectedInputCount = 2;
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(expectedInputCount, 'A', 'B', (ulong)1, 'C', 'D', (ulong)3);
            BinaryMarkovMatrixLoaderSaver<ulong> binaryMarkovMatrixLoaderSaver = new BinaryMarkovMatrixLoaderSaver<ulong>(memoryStream);

            // Act
            IMarkovMatrix<ulong> markovMatrix = binaryMarkovMatrixLoaderSaver.LoadMatrix();
            int actualInputCount = markovMatrix.InputCount;

            // Assert
            Assert.Equal(expectedInputCount, actualInputCount);

        }
        [Fact]
        public void Given_BinaryStream_LoadMatrix_ShouldGetRightOccurrenceFirstCharGroup()
        {
            // Arrange
            ulong expectedOccurrence = 2;
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(2, 'A', 'B', expectedOccurrence, 'C', 'D', (ulong)3);
            BinaryMarkovMatrixLoaderSaver<ulong> binaryMarkovMatrixLoaderSaver = new BinaryMarkovMatrixLoaderSaver<ulong>(memoryStream);

            // Act
            IMarkovMatrix<ulong> markovMatrix = binaryMarkovMatrixLoaderSaver.LoadMatrix();
            ulong actualOccurence = markovMatrix.GetOccurrence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurence);
        }

        [Fact]
        public void Given_BinaryStream_LoadMatrix_ShouldGetRightOccurrenceSecondCharGroup()
        {
            // Arrange
            ulong expectedOccurrence = 3;
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(2, 'A', 'B', (ulong)2, 'C', 'D', expectedOccurrence);
            BinaryMarkovMatrixLoaderSaver<ulong> binaryMarkovMatrixLoaderSaver = new BinaryMarkovMatrixLoaderSaver<ulong>(memoryStream);

            // Act
            IMarkovMatrix<ulong> markovMatrix = binaryMarkovMatrixLoaderSaver.LoadMatrix();
            ulong actualOccurence = markovMatrix.GetOccurrence('C', 'D');

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurence);
        }
    }
}
