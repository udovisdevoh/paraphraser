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
    public class TextReaderMarkovMatrixLoaderTests
    {
        [Fact]
        public void Given_Stream_LoadMatrix()
        {
            // Arrange
            Stream stream = StringStreamBuilder.Build("Pseudolachnostylis is a genus of plants in the Phyllanthaceae first described as a genus in 1899");
            TextReaderMarkovMatrixLoader<ulong> textReaderMarkovMatrixLoader = new TextReaderMarkovMatrixLoader<ulong>(stream);

            // Act
            IMarkovMatrix<ulong> markovMatrix = textReaderMarkovMatrixLoader.LoadMatrix();

            // Assert
            Assert.True(markovMatrix.InputCount > 0);
        }
    }
}
