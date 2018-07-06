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
            StringMarkovMatrixLoaderFromText stringMarkovMatrixLoaderFromText = new StringMarkovMatrixLoaderFromText();
            double expectedProbability = 0.25;

            // Act
            IMarkovMatrix<string, double> markovMatrix = stringMarkovMatrixLoaderFromText.LoadMatrix(text);
            double actualProbability = markovMatrix.GetOccurrence("zarf", "zorf");

            // Assert
            Assert.Equal(expectedProbability, actualProbability);
        }

        [Fact]
        public void GivenTextWithWhiteList_ShouldLoadMatrix()
        {
            // Arrange
            string text = "zarF! Zorf zUrf? zArf zeRf zi'RF ZARF zyRF ZarF zARff.";
            StringMarkovMatrixLoaderFromText stringMarkovMatrixLoaderFromText = new StringMarkovMatrixLoaderFromText();
            double expectedProbability = 0.0;
            HashSet<string> whiteList = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "zerf" };

            // Act
            IMarkovMatrix<string, double> markovMatrix = stringMarkovMatrixLoaderFromText.LoadMatrix(text, whiteList);
            double actualProbability = markovMatrix.GetOccurrence("zarf", "zorf");

            // Assert
            Assert.Equal(expectedProbability, actualProbability);
        }

        [Fact]
        public void GivenStream_ShouldLoadMatrix()
        {
            // Arrange
            string text = "zarF! Zorf zUrf? zArf zeRf zi'RF ZARF zyRF ZarF zARff.";
            StringMarkovMatrixLoaderFromText stringMarkovMatrixLoaderFromText = new StringMarkovMatrixLoaderFromText();
            double expectedProbability = 0.25;
            Stream stream = StreamBuilder.BuildTextStream(text);

            // Act
            IMarkovMatrix<string, double> markovMatrix = stringMarkovMatrixLoaderFromText.LoadMatrix(stream);
            double actualProbability = markovMatrix.GetOccurrence("zarf", "zorf");

            // Assert
            Assert.Equal(expectedProbability, actualProbability);
        }

        [Fact]
        public void GivenStreamWithLargeMaxSize_ShouldLoadMatrix()
        {
            // Arrange
            string text = "zarF! Zorf zUrf? zArf zeRf zi'RF ZARF zyRF ZarF zARff.";
            StringMarkovMatrixLoaderFromText stringMarkovMatrixLoaderFromText = new StringMarkovMatrixLoaderFromText();
            double expectedProbability = 1.0;
            Stream stream = StreamBuilder.BuildTextStream(text);

            // Act
            IMarkovMatrix<string, double> markovMatrix = stringMarkovMatrixLoaderFromText.LoadMatrix(stream, 30);
            double actualProbability = markovMatrix.GetOccurrence("zyrf", "zarf");

            // Assert
            Assert.Equal(expectedProbability, actualProbability);
        }

        [Fact]
        public void GivenStreamWithSmallMaxSize_ShouldLoadMatrix()
        {
            // Arrange
            string text = "zarF! Zorf zUrf? zArf zeRf zi'RF ZARF zyRF ZarF zARff.";
            StringMarkovMatrixLoaderFromText stringMarkovMatrixLoaderFromText = new StringMarkovMatrixLoaderFromText();
            double expectedProbability = 0.0;
            Stream stream = StreamBuilder.BuildTextStream(text);

            // Act
            IMarkovMatrix<string, double> markovMatrix = stringMarkovMatrixLoaderFromText.LoadMatrix(stream, 3);
            double actualProbability = markovMatrix.GetOccurrence("zyrf", "zarf");

            // Assert
            Assert.Equal(expectedProbability, actualProbability);
        }

        [Fact]
        public void GivenStreamWithWhiteList_ShouldLoadMatrix()
        {
            // Arrange
            string text = "zarF! Zorf zUrf? zArf zeRf zi'RF ZARF zyRF ZarF zARff.";
            StringMarkovMatrixLoaderFromText stringMarkovMatrixLoaderFromText = new StringMarkovMatrixLoaderFromText();
            double expectedProbability = 0.0;
            Stream stream = StreamBuilder.BuildTextStream(text);
            HashSet<string> whiteList = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "zerf" };

            // Act
            IMarkovMatrix<string, double> markovMatrix = stringMarkovMatrixLoaderFromText.LoadMatrix(stream, whiteList);
            double actualProbability = markovMatrix.GetOccurrence("zarf", "zorf");

            // Assert
            Assert.Equal(expectedProbability, actualProbability);
        }

        [Fact]
        public void GivenMatrix_ShouldNormalize()
        {
            // Arrange
            StringMarkovMatrixLoaderFromText stringMarkovMatrixLoaderFromText = new StringMarkovMatrixLoaderFromText();
            StringMarkovMatrix<ulong> markovMatrix = new StringMarkovMatrix<ulong>();
            markovMatrix.IncrementOccurrence("Zarf", "zoRf");
            markovMatrix.IncrementOccurrence("zArf", "zerF");
            markovMatrix.IncrementOccurrence("zuRf", "zoRf");
            markovMatrix.IncrementOccurrence("zurF", "zErf");
            markovMatrix.IncrementOccurrence("zurf", "Zirf");
            double expectedProbability = 0.5;

            // Act
            IMarkovMatrix<string, double> normalizedMarkovMatrix = stringMarkovMatrixLoaderFromText.Normalize(markovMatrix);
            double actualProbability = normalizedMarkovMatrix.GetOccurrence("zarF", "zErf");

            // Assert
            Assert.Equal(expectedProbability, actualProbability);
        }

        [Fact]
        public void GivenMatrixAndMaxSize_Normalize_ShouldReduceMatrix()
        {
            // Arrange
            StringMarkovMatrixLoaderFromText stringMarkovMatrixLoaderFromText = new StringMarkovMatrixLoaderFromText();
            StringMarkovMatrix<ulong> markovMatrix = new StringMarkovMatrix<ulong>();
            markovMatrix.IncrementOccurrence("Zarf", "zoRf");
            markovMatrix.IncrementOccurrence("zArf", "zerF");
            markovMatrix.IncrementOccurrence("zuRf", "zoRf");
            markovMatrix.IncrementOccurrence("zurF", "zErf");
            markovMatrix.IncrementOccurrence("zurf", "Zirf");
            int expectedInputCount = 3;

            // Act
            IMarkovMatrix<string, double> normalizedMarkovMatrix = stringMarkovMatrixLoaderFromText.Normalize(markovMatrix, expectedInputCount);

            // Assert
            Assert.Equal(expectedInputCount, normalizedMarkovMatrix.InputCount);
        }
    }
}
