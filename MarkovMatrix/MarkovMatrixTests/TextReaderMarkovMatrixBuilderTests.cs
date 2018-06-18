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
    public class TextReaderMarkovMatrixBuilderTests
    {
        [Fact]
        public void Given_Stream_BuildMatrix()
        {
            // Arrange
            Stream stream = StringStreamBuilder.Build("Pseudolachnostylis is a genus of plants in the Phyllanthaceae first described as a genus in 1899");
            TextReaderMarkovMatrixBuilder<ulong> textReaderMarkovMatrixBuilder = new TextReaderMarkovMatrixBuilder<ulong>(stream);

            // Act
            IMarkovMatrix<ulong> markovMatrix = textReaderMarkovMatrixBuilder.BuildMatrix();

            // Assert
            Assert.True(markovMatrix.InputCount > 0);
        }
    }
}
