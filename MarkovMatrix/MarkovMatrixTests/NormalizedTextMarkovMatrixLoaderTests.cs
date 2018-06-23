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
            NormalizedTextMarkovMatrixLoader textMarkovMatrixLoader = new NormalizedTextMarkovMatrixLoader(new TextMarkovMatrixLoader(), new MarkovMatrixNormalizer());

            // Act
            IMarkovMatrix<double> markovMatrix = textMarkovMatrixLoader.LoadMatrix(stream);

            // Assert
            Assert.True(markovMatrix.InputCount > 0);
        }

        [Fact]
        public void GivenStream_LoadMatrix_ShouldHaveCorrectProbability()
        {
            // Arrange
            Stream stream = StreamBuilder.BuildTextStream("Pseudolachnostylis is a genus of plants in the Phyllanthaceae first described as a genus in 1899");
            NormalizedTextMarkovMatrixLoader textMarkovMatrixLoader = new NormalizedTextMarkovMatrixLoader(new TextMarkovMatrixLoader(), new MarkovMatrixNormalizer());
            double expectedProbability = 0.6;

            // Act
            IMarkovMatrix<double> markovMatrix = textMarkovMatrixLoader.LoadMatrix(stream);
            double actualProbability = markovMatrix.GetOccurrence('l', 'a');

            // Assert
            Assert.Equal(expectedProbability, actualProbability);
        }
    }
}
