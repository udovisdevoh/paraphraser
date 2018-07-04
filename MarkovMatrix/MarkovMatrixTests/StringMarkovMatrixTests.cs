using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarkovMatrices
{
    public class StringMarkovMatrixTests
    {
        [Fact]
        public void GivenStringMatrix_ShouldCombineElements()
        {
            // Arrange
            StringMarkovMatrix<ulong> markovMatrix = new StringMarkovMatrix<ulong>();
            markovMatrix.IncrementOccurrence("Aa", "Bb");
            uint expectedCombinedStrings = 1;

            // Act
            uint actualCombinedStrings = markovMatrix.CombineElements("Aa", "Bb");

            // Assert
            Assert.Equal(expectedCombinedStrings, actualCombinedStrings);
        }

        [Fact]
        public void GivenStringMatrixAndWord_ShouldGetWordId()
        {
            // Arrange
            StringMarkovMatrix<ulong> markovMatrix = new StringMarkovMatrix<ulong>();
            markovMatrix.IncrementOccurrence("Aa", "Bb");
            markovMatrix.IncrementOccurrence("Bb", "Cc");
            ushort expectedWordId = 2;

            // Act
            ushort actualWordId = markovMatrix.GetWordId("Cc");

            // Assert
            Assert.Equal(expectedWordId, actualWordId);
        }

        [Fact]
        public void GivenStringMatrixAndWordId_ShouldGetWord()
        {
            // Arrange
            StringMarkovMatrix<ulong> markovMatrix = new StringMarkovMatrix<ulong>();
            markovMatrix.IncrementOccurrence("Aa", "Bb");
            markovMatrix.IncrementOccurrence("Bb", "Cc");
            string expectedWord = "cc";

            // Act
            string actualWord = markovMatrix.GetWord(2);

            // Assert
            Assert.Equal(expectedWord, actualWord);
        }

        [Fact]
        public void GivenStringMatrixAndCombineWords_SplitElements_ShouldGetFirstWord()
        {
            // Arrange
            StringMarkovMatrix<ulong> markovMatrix = new StringMarkovMatrix<ulong>();
            markovMatrix.IncrementOccurrence("Aa", "Bb");
            markovMatrix.IncrementOccurrence("Bb", "Cc");
            Tuple<string, string> expectedSplittedElements = new Tuple<string, string>("aa", "cc");

            // Act
            Tuple<string, string> actualSplittedElements = markovMatrix.SplitElements(2);

            // Assert
            Assert.Equal(expectedSplittedElements.Item1, actualSplittedElements.Item1);
        }

        [Fact]
        public void GivenStringMatrixAndCombineWords_SplitElements_ShouldGetSecondWord()
        {
            // Arrange
            StringMarkovMatrix<ulong> markovMatrix = new StringMarkovMatrix<ulong>();
            markovMatrix.IncrementOccurrence("Aa", "Bb");
            markovMatrix.IncrementOccurrence("Bb", "Cc");
            Tuple<string, string> expectedSplittedElements = new Tuple<string, string>("aa", "cc");

            // Act
            Tuple<string, string> actualSplittedElements = markovMatrix.SplitElements(2);

            // Assert
            Assert.Equal(expectedSplittedElements.Item2, actualSplittedElements.Item2);
        }
    }
}
