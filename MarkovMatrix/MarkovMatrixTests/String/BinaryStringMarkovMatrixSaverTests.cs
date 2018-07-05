using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarkovMatrices.Tests
{
    public class BinaryStringMarkovMatrixSaverTests
    {
        [Fact]
        public void GivenMatrix_SaveMatrix_ShouldSaveToBinaryStreamWithCorrectOccurrence()
        {
            // Arrange
            double expectedOccurrence = 3.3;
            StringMarkovMatrix<double> markovMatrix = new StringMarkovMatrix<double>();
            markovMatrix.IncrementOccurrence("Aa", "Bb", 2.2);
            markovMatrix.IncrementOccurrence("Cc", "Dd", expectedOccurrence);
            MemoryStream memoryStream = new MemoryStream();
            BinaryStringMarkovMatrixSaver binaryStringMarkovMatrixSaver = new BinaryStringMarkovMatrixSaver();

            // Act
            binaryStringMarkovMatrixSaver.SaveMatrix(markovMatrix, memoryStream);

            // Assert
            memoryStream.Position = 0;
            BinaryStringMarkovMatrixLoader binaryStringMarkovMatrixLoader = new BinaryStringMarkovMatrixLoader();
            IMarkovMatrix<string, double> loadedMatrix = binaryStringMarkovMatrixLoader.LoadMatrix(memoryStream);
            double actualOccurrence = loadedMatrix.GetOccurrence("Cc", "Dd");
            Assert.Equal(expectedOccurrence, actualOccurrence);
        }
    }
}
