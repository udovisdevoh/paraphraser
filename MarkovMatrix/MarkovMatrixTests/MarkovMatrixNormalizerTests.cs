using MarkovMatrices.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarkovMatrices.Tests
{
    public class MarkovMatrixNormalizerTests
    {
        [Fact]
        public void GivenMatrix_Normalize_ShouldBuildNormalizedMatrixGetNormalizedValueSingleOccurence()
        {
            // Arrange
            double expectedOccurence = 0.25;
            MarkovMatrixNormalizer markovMatrixNormalizer = new MarkovMatrixNormalizer();
            MarkovMatrix<ulong> markovMatrix = new MarkovMatrix<ulong>();
            markovMatrix.IncrementOccurrence('A', 'B');
            markovMatrix.IncrementOccurrence('A', 'C');
            markovMatrix.IncrementOccurrence('A', 'D');
            markovMatrix.IncrementOccurrence('A', 'D');

            markovMatrix.IncrementOccurrence('B', 'B');
            markovMatrix.IncrementOccurrence('B', 'A');
            markovMatrix.IncrementOccurrence('B', 'A');
            markovMatrix.IncrementOccurrence('B', 'B');

            // Act
            IMarkovMatrix<double> normalizedMatrix = markovMatrixNormalizer.Normalize(markovMatrix);
            double actualOccurrence = Math.Round(normalizedMatrix.GetOccurrence('A', 'B'), 2);

            // Assert
            Assert.Equal(expectedOccurence, actualOccurrence);
        }

        [Fact]
        public void GivenMatrix_Normalize_ShouldBuildNormalizedMatrixGetNormalizedValueDoubleOccurence()
        {
            // Arrange
            double expectedOccurence = 0.5;
            MarkovMatrixNormalizer markovMatrixNormalizer = new MarkovMatrixNormalizer();
            MarkovMatrix<ulong> markovMatrix = new MarkovMatrix<ulong>();
            markovMatrix.IncrementOccurrence('A', 'B');
            markovMatrix.IncrementOccurrence('A', 'C');
            markovMatrix.IncrementOccurrence('A', 'D');
            markovMatrix.IncrementOccurrence('A', 'D');

            markovMatrix.IncrementOccurrence('B', 'B');
            markovMatrix.IncrementOccurrence('B', 'A');
            markovMatrix.IncrementOccurrence('B', 'A');
            markovMatrix.IncrementOccurrence('B', 'B');

            // Act
            IMarkovMatrix<double> normalizedMatrix = markovMatrixNormalizer.Normalize(markovMatrix);
            double actualOccurrence = Math.Round(normalizedMatrix.GetOccurrence('A', 'D'), 2);

            // Assert
            Assert.Equal(expectedOccurence, actualOccurrence);
        }
    }
}
