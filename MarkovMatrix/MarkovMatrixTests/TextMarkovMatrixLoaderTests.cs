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
    public class TextMarkovMatrixLoaderTests
    {
        [Fact]
        public void GivenStream_LoadMatrix()
        {
            // Arrange
            Stream stream = StreamBuilder.BuildTextStream("Pseudolachnostylis is a genus of plants in the Phyllanthaceae first described as a genus in 1899");
            TextMarkovMatrixLoader textMarkovMatrixLoader = new TextMarkovMatrixLoader();

            // Act
            IMarkovMatrix<ulong> markovMatrix = textMarkovMatrixLoader.LoadMatrix(stream);

            // Assert
            Assert.True(markovMatrix.InputCount > 0);
        }

        [Fact]
        public void GivenText_LoadMatrix()
        {
            // Arrange
            string text = "Pseudolachnostylis is a genus of plants in the Phyllanthaceae first described as a genus in 1899";
            TextMarkovMatrixLoader textMarkovMatrixLoader = new TextMarkovMatrixLoader();

            // Act
            IMarkovMatrix<ulong> markovMatrix = textMarkovMatrixLoader.LoadMatrix(text);

            // Assert
            Assert.True(markovMatrix.InputCount > 0);
        }

        [Fact]
        public void GivenStream_LoadMatrix_ShouldHaveCorrectOccurrence()
        {
            // Arrange
            Stream stream = StreamBuilder.BuildTextStream("Pseudolachnostylis is a genus of plants in the Phyllanthaceae first described as a genus in 1899");
            TextMarkovMatrixLoader textMarkovMatrixLoader = new TextMarkovMatrixLoader();
            ulong expectedOccurrence = 3;

            // Act
            IMarkovMatrix<ulong> markovMatrix = textMarkovMatrixLoader.LoadMatrix(stream);
            ulong actualOccurrence = markovMatrix.GetOccurrence('l', 'a');

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurrence);
        }
    }
}
