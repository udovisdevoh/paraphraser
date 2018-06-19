﻿using MarkovMatrices.TestHelper;
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
        public void GivenMatrix_Normalize_ShouldLoadNormalizedMatrixGetNormalizedValueSingleOccurence()
        {
            // Arrange
            float expectedOccurence = 0.25f;
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
            IMarkovMatrix<float> normalizedMatrix = markovMatrixNormalizer.Normalize(markovMatrix);
            double actualOccurrence = Math.Round(normalizedMatrix.GetOccurrence('A', 'B'), 2);

            // Assert
            Assert.Equal(expectedOccurence, actualOccurrence);
        }

        [Fact]
        public void GivenMatrix_Normalize_ShouldLoadNormalizedMatrixGetNormalizedValueDoubleOccurence()
        {
            // Arrange
            float expectedOccurence = 0.5f;
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
            IMarkovMatrix<float> normalizedMatrix = markovMatrixNormalizer.Normalize(markovMatrix);
            double actualOccurrence = Math.Round(normalizedMatrix.GetOccurrence('A', 'D'), 2);

            // Assert
            Assert.Equal(expectedOccurence, actualOccurrence);
        }
    }
}
