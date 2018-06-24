using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarkovMatrices.Tests
{
    public class MarkovMatrixCharacterCombinerTests
    {
        [Fact]
        public void GivenMatrix_Normalize_ShouldLoadNormalizedMatrixGetNormalizedValueDoubleOccurence()
        {
            // Arrange
            double expectedOccurence = 0.5f;
            MarkovMatrixCharacterCombiner markovMatrixCharacterCombiner = new MarkovMatrixCharacterCombiner(letter => letter);
            MarkovMatrix<double> markovMatrix = new MarkovMatrix<double>();
            markovMatrix.IncrementOccurrence('A', 'B');
            markovMatrix.IncrementOccurrence('A', 'C');
            markovMatrix.IncrementOccurrence('A', 'D');
            markovMatrix.IncrementOccurrence('A', 'D');

            markovMatrix.IncrementOccurrence('B', 'B');
            markovMatrix.IncrementOccurrence('B', 'A');
            markovMatrix.IncrementOccurrence('B', 'A');
            markovMatrix.IncrementOccurrence('B', 'B');

            // Act
            IMarkovMatrix<double> normalizedMatrix = markovMatrixCharacterCombiner.Normalize(markovMatrix);
            double actualOccurrence = Math.Round(normalizedMatrix.GetOccurrence('A', 'D'), 2);

            // Assert
            Assert.Equal(expectedOccurence, actualOccurrence);
        }

        [Fact]
        public void GivenMatrix_Transform_ShouldBuildMatrixWithCombinedCharacters()
        {
            // Arrange
            double expectedOccurence = Math.Round(0.666666, 3);
            MarkovMatrixCharacterCombiner markovMatrixCharacterCombiner = new MarkovMatrixCharacterCombiner(letter => StringFormatter.RemoveDiacritics(letter));
            MarkovMatrix<double> markovMatrix = new MarkovMatrix<double>();
            markovMatrix.IncrementOccurrence('a', 'e');
            markovMatrix.IncrementOccurrence('à', 'ê');
            markovMatrix.IncrementOccurrence('ä', 'è');
            markovMatrix.IncrementOccurrence('a', 'é');
            markovMatrix.IncrementOccurrence('a', 'i');
            markovMatrix.IncrementOccurrence('a', 'i');
            markovMatrix.IncrementOccurrence('b', 'e');
            markovMatrix.IncrementOccurrence('b', 'i');

            // Act
            IMarkovMatrix<double> transformedMatrix = markovMatrixCharacterCombiner.Transform(markovMatrix);
            double actualOccurrence = Math.Round(transformedMatrix.GetOccurrence('a', 'e'), 3);

            // Assert
            Assert.Equal(expectedOccurence, actualOccurrence);
        }
    }
}
