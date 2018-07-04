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
            double expectedOccurrence = 3.0;
            CharMarkovMatrix<double> markovMatrix = new CharMarkovMatrix<double>();
            markovMatrix.IncrementOccurrence('A', 'B', 2);
            markovMatrix.IncrementOccurrence('C', 'D', expectedOccurrence);
            MemoryStream memoryStream = new MemoryStream();
            BinaryMarkovMatrixSaver binaryMarkovMatrixSaver = new BinaryMarkovMatrixSaver();

            // Act
            binaryMarkovMatrixSaver.SaveMatrix(markovMatrix, memoryStream);

            // Assert
            memoryStream.Position = 0;
            BinaryMarkovMatrixLoader binaryMarkovMatrixLoader = new BinaryMarkovMatrixLoader();
            IMarkovMatrix<char, double> loadedMatrix = binaryMarkovMatrixLoader.LoadMatrix(memoryStream);
            double actualOccurrence = loadedMatrix.GetOccurrence('C', 'D');
            Assert.Equal(expectedOccurrence, actualOccurrence);
        }
    }
}
