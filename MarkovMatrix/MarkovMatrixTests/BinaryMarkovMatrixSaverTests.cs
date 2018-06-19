using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarkovMatrices.Tests
{
    public class BinaryMarkovMatrixSaverTests
    {
        [Fact]
        public void GivenMatrix_SaveMatrix_ShouldSaveToBinaryStreamWithCorrectOccurrence()
        {
            // Arrange
            ulong expectedOccurrence = 3;
            MarkovMatrix<ulong> markovMatrix = new MarkovMatrix<ulong>();
            markovMatrix.IncrementOccurrence('A', 'B', 2);
            markovMatrix.IncrementOccurrence('C', 'D', expectedOccurrence);
            MemoryStream memoryStream = new MemoryStream();
            BinaryMarkovMatrixSaver<ulong> binaryMarkovMatrixSaver = new BinaryMarkovMatrixSaver<ulong>();

            // Act
            binaryMarkovMatrixSaver.SaveMatrix(markovMatrix, memoryStream);

            // Assert
            memoryStream.Position = 0;
            BinaryMarkovMatrixLoader<ulong> binaryMarkovMatrixLoader = new BinaryMarkovMatrixLoader<ulong>();
            IMarkovMatrix<ulong> loadedMatrix = binaryMarkovMatrixLoader.LoadMatrix(memoryStream);
            ulong actualOccurrence = loadedMatrix.GetOccurrence('C', 'D');
            Assert.Equal(expectedOccurrence, actualOccurrence);
        }
    }
}
