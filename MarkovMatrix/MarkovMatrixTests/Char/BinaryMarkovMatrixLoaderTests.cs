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
            BinaryMarkovMatrixLoader binaryMarkovMatrixLoader = new BinaryMarkovMatrixLoader();

            // Act
            IMarkovMatrix<char, double> markovMatrix = binaryMarkovMatrixLoader.LoadMatrix(memoryStream);
            int actualInputCount = markovMatrix.InputCount;

            // Assert
            Assert.Equal(expectedInputCount, actualInputCount);
        }

        [Fact]
        public void GivenBinaryStreamAndWhiteList_LoadMatrix_ShouldGetRightInputCount()
        {
            // Arrange
            int expectedInputCount = 1;
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(expectedInputCount, 'A', 'B', (ulong)1, 'C', 'D', (ulong)3);
            BinaryMarkovMatrixLoader binaryMarkovMatrixLoader = new BinaryMarkovMatrixLoader();
            HashSet<char> whiteList = new HashSet<char>() { 'B' };

            // Act
            IMarkovMatrix<char, double> markovMatrix = binaryMarkovMatrixLoader.LoadMatrix(memoryStream, whiteList);
            int actualInputCount = markovMatrix.InputCount;

            // Assert
            Assert.Equal(expectedInputCount, actualInputCount);
        }

        [Fact]
        public void GivenBinaryStream_LoadMatrix_ShouldGetRightOccurrenceFirstCharGroup()
        {
            // Arrange
            double expectedOccurrence = 2;
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(2, 'A', 'B', expectedOccurrence, 'C', 'D', 3.0);
            BinaryMarkovMatrixLoader binaryMarkovMatrixLoader = new BinaryMarkovMatrixLoader();

            // Act
            IMarkovMatrix<char, double> markovMatrix = binaryMarkovMatrixLoader.LoadMatrix(memoryStream);
            double actualOccurence = markovMatrix.GetOccurrence('A', 'B');

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurence);
        }

        [Fact]
        public void GivenBinaryStream_LoadMatrix_ShouldGetRightOccurrenceSecondCharGroup()
        {
            // Arrange
            double expectedOccurrence = 3;
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(2, 'A', 'B', 2.0, 'C', 'D', expectedOccurrence);
            BinaryMarkovMatrixLoader binaryMarkovMatrixLoader = new BinaryMarkovMatrixLoader();

            // Act
            IMarkovMatrix<char, double> markovMatrix = binaryMarkovMatrixLoader.LoadMatrix(memoryStream);
            double actualOccurence = markovMatrix.GetOccurrence('C', 'D');

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurence);
        }

        [Fact]
        public void GivenText_LoadMatrix_ShouldThrow()
        {
            // Arrange
            string text = "zarf";
            BinaryMarkovMatrixLoader binaryMarkovMatrixLoader = new BinaryMarkovMatrixLoader();

            // Act, Assert
            Assert.Throws<NotSupportedException>(() =>
            {
                IMarkovMatrix<char, double> markovMatrix = binaryMarkovMatrixLoader.LoadMatrix(text);
            });
        }
    }
}
