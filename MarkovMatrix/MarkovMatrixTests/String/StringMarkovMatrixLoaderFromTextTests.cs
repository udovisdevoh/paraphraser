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
    public class StringMarkovMatrixLoaderFromTextTests
    {
        [Fact]
        public void GivenText_ShouldLoadMatrix()
        {
            // Arrange
            string text = "zarF! Zorf zUrf? zArf zeRf zi'RF ZARF zyRF ZarF zARff.";
            StringMarkovMatrixLoaderFromText binaryStringMarkovMatrixLoader = new StringMarkovMatrixLoaderFromText();
            double expectedProbability = 0.25;

            // Act
            IMarkovMatrix<string, double> markovMatrix = binaryStringMarkovMatrixLoader.LoadMatrix(text);
            double actualProbability = markovMatrix.GetOccurrence("zarf", "zorf");

            // Assert
            Assert.Equal(expectedProbability, actualProbability);
        }

        [Fact]
        public void GivenStream_ShouldLoadMatrix()
        {
            // Arrange
            string text = "zarF! Zorf zUrf? zArf zeRf zi'RF ZARF zyRF ZarF zARff.";
            StringMarkovMatrixLoaderFromText binaryStringMarkovMatrixLoader = new StringMarkovMatrixLoaderFromText();
            double expectedProbability = 0.25;
            Stream stream = StreamBuilder.BuildTextStream(text);

            // Act
            IMarkovMatrix<string, double> markovMatrix = binaryStringMarkovMatrixLoader.LoadMatrix(stream);
            double actualProbability = markovMatrix.GetOccurrence("zarf", "zorf");

            // Assert
            Assert.Equal(expectedProbability, actualProbability);
        }

        [Fact]
        public void GivenMatrix_ShouldNormalize()
        {
            // Arrange
            StringMarkovMatrixLoaderFromText binaryStringMarkovMatrixLoader = new StringMarkovMatrixLoaderFromText();
            StringMarkovMatrix<ulong> markovMatrix = new StringMarkovMatrix<ulong>();
            markovMatrix.IncrementOccurrence("Zarf", "zoRf");
            markovMatrix.IncrementOccurrence("zArf", "zerF");
            markovMatrix.IncrementOccurrence("zuRf", "zoRf");
            markovMatrix.IncrementOccurrence("zurF", "zErf");
            markovMatrix.IncrementOccurrence("zurf", "Zirf");
            double expectedProbability = 0.5;

            // Act
            IMarkovMatrix<string, double> normalizedMarkovMatrix = binaryStringMarkovMatrixLoader.Normalize(markovMatrix);
            double actualProbability = normalizedMarkovMatrix.GetOccurrence("zarF", "zErf");

            // Assert
            Assert.Equal(expectedProbability, actualProbability);
        }
    }
}
