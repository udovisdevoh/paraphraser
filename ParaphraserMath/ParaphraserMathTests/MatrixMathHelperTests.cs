﻿using LanguageDetection.TestHelpers;
using MarkovMatrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ParaphraserMath.Tests
{
    public class MatrixMathHelperTests
    {
        [Fact]
        public void Given_CombineChars_GetUint()
        {
            // Arrange
            uint expectedCombinedChars = 4259906;
            // Act
            uint actualCombinedChars = MatrixMathHelper.CombineChars('A', 'B');

            // Assert
            Assert.Equal(expectedCombinedChars, actualCombinedChars);
        }

        [Fact]
        public void Given_SplitUint_GetFirstChar()
        {
            // Arrange
            char expectedChar1 = 'A';
            uint combinedChars = 4259906;

            // Act
            Tuple<char, char> actualChars = MatrixMathHelper.SplitChars(combinedChars);

            // Assert
            Assert.Equal(expectedChar1, actualChars.Item1);
        }

        [Fact]
        public void Given_SplitUint_GetSecondChar()
        {
            // Arrange
            char expectedChar2 = 'B';
            uint combinedChars = 4259906;

            // Act
            Tuple<char, char> actualChars = MatrixMathHelper.SplitChars(combinedChars);

            // Assert
            Assert.Equal(expectedChar2, actualChars.Item2);
        }

        [Fact]
        public void GivenTwoMatrices_GetDotProduct()
        {
            // Arrange
            double expectedDotProduct = 0.25;
            IMarkovMatrix<double> englishMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrix<double> frenchMatrix = LanguageDetectorTestHelper.BuildFrenchLanguageMatrixMock();

            // Act
            double actualDotProduct = MatrixMathHelper.GetDotProduct(englishMatrix, frenchMatrix);

            // Assert
            Assert.Equal(expectedDotProduct, actualDotProduct);
        }

        [Fact]
        public void GivenDoubles_GetStandardDeviation()
        {
            // Arrange
            double[] numbers = new double[] { 1.0, 2.5, 10.1, -0.5 };
            double expectedStandardDeviation = 4.71195288601234;

            // Act
            double actualStandardDeviation = MatrixMathHelper.GetStandardDeviation(numbers);

            // Assert
            Assert.Equal(Math.Round(expectedStandardDeviation, 3), Math.Round(actualStandardDeviation, 3));
        }

        [Fact]
        public void GivenTwoMatrices_GetDistance()
        {
            // Arrange
            double expectedDistance = 0.894427190999916;
            IMarkovMatrix<double> englishMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrix<double> frenchMatrix = LanguageDetectorTestHelper.BuildFrenchLanguageMatrixMock();

            // Act
            double actualDistance = MatrixMathHelper.GetDistance(englishMatrix, frenchMatrix);

            // Assert
            Assert.Equal(Math.Round(expectedDistance, 3), Math.Round(actualDistance, 3));
        }

        [Fact]
        public void GivenDoubles_MostVariantSet_ShouldHaveGreaterStandardVariation()
        {
            // Arrange
            double[] numbersLeastVariant = new double[] { 1.0, 2.5, 10.1, -0.5 };
            double[] numbersMostVariant = new double[] { 2.0, 3.5, 100.1, -0.50 };

            // Act
            double actualStandardDeviationLeastVariant = MatrixMathHelper.GetStandardDeviation(numbersLeastVariant);
            double actualStandardDeviationMostVariant = MatrixMathHelper.GetStandardDeviation(numbersMostVariant);

            // Assert
            Assert.True(actualStandardDeviationMostVariant > actualStandardDeviationLeastVariant);
        }
    }
}
