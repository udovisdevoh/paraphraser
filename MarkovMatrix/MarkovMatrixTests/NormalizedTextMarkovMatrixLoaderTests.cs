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
    public class NormalizedTextMarkovMatrixLoaderTests
    {
        [Fact]
        public void GivenStream_LoadMatrix()
        {
            // Arrange
            Stream stream = StreamBuilder.BuildTextStream("Pseudolachnostylis is a genus of plants in the Phyllanthaceae first described as a genus in 1899");
            NormalizedTextMarkovMatrixLoader textMarkovMatrixLoader = new NormalizedTextMarkovMatrixLoader(new TextMarkovMatrixLoader<ulong>(), new MarkovMatrixNormalizer());

            // Act
            IMarkovMatrix<float> markovMatrix = textMarkovMatrixLoader.LoadMatrix(stream);

            // Assert
            Assert.True(markovMatrix.InputCount > 0);
        }

        [Fact]
        public void GivenStream_LoadMatrix_ShouldHaveCorrectProbability()
        {
            // Arrange
            Stream stream = StreamBuilder.BuildTextStream("Pseudolachnostylis is a genus of plants in the Phyllanthaceae first described as a genus in 1899");
            NormalizedTextMarkovMatrixLoader textMarkovMatrixLoader = new NormalizedTextMarkovMatrixLoader(new TextMarkovMatrixLoader<ulong>(), new MarkovMatrixNormalizer());
            float expectedProbability = 0.6f;

            // Act
            IMarkovMatrix<float> markovMatrix = textMarkovMatrixLoader.LoadMatrix(stream);
            float actualProbability = markovMatrix.GetOccurrence('l', 'a');

            // Assert
            Assert.Equal(expectedProbability, actualProbability);
        }
    }
}
