using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StringManipulation.Tests
{
    public class StringAnalysisTests
    {
        [Fact]
        public void Given_SpaceChar_DetectPunctuation_ShouldReturnTrue()
        {
            // Assert
            Assert.True(StringAnalysis.IsPunctuationOrSpace(' '));
        }

        [Fact]
        public void Given_PunctuationChar_DetectPunctuation_ShouldReturnTrue()
        {
            // Assert
            Assert.True(StringAnalysis.IsPunctuationOrSpace(','));
        }

        [Fact]
        public void Given_LetterChar_DetectPunctuation_ShouldReturnFalse()
        {
            // Assert
            Assert.False(StringAnalysis.IsPunctuationOrSpace('A'));
        }

        [Fact]
        public void GivenWord_ShouldGetIdenticalWordRegardlessPunctuation()
        {
            // Arrange
            string sourceWord = "arent";
            string expectedIdenticalWord = "aren't";
            string actualIdenticalWord;
            List<string> otherWords = new List<string>() { "isn't", "ain't", "aren't", "wont" };

            // Act
            actualIdenticalWord = StringAnalysis.GetIdenticalWordRegardlessPunctuation(sourceWord, otherWords);

            // Assert
            Assert.Equal(expectedIdenticalWord, actualIdenticalWord);
        }

        [Fact]
        public void GivenWord_ShouldGetMostSimilarWord()
        {
            // Arrange
            string sourceWord = "wont";
            string expectedIdenticalWord = "want";
            string actualIdenticalWord;
            List<string> otherWords = new List<string>() { "win", "wan", "wun", "want", "shon" };

            // Act
            actualIdenticalWord = StringAnalysis.GetMostSimilarWord(sourceWord, otherWords);

            // Assert
            Assert.Equal(expectedIdenticalWord, actualIdenticalWord);
        }

        [Fact]
        public void GivenTwoDifferentWords_ShouldGetLargeLevenshteinDistance()
        {
            // Arrange
            string word1 = "zarf";
            string word2 = "echo";
            int expectedLevenshteinDistance = 4;

            // Act
            int actualLevenshteinDistance = StringAnalysis.GetLevenshteinDistance(word1, word2);

            // Assert
            Assert.Equal(expectedLevenshteinDistance, actualLevenshteinDistance);
        }

        [Fact]
        public void GivenTwoSimilarWords_ShouldGetSmallLevenshteinDistance()
        {
            // Arrange
            string word1 = "zarf";
            string word2 = "ezurfe";
            int expectedLevenshteinDistance = 3;

            // Act
            int actualLevenshteinDistance = StringAnalysis.GetLevenshteinDistance(word1, word2);

            // Assert
            Assert.Equal(expectedLevenshteinDistance, actualLevenshteinDistance);
        }

        [Fact]
        public void GivenStringStartingWithNumber_DetectStartsWithNumber_ShouldReturnTrue()
        {
            // Arrange
            string input = "18sasdfgd";

            // Act
            bool isStartWithNumber = StringAnalysis.StartsWithNumber(input);

            // Assert
            Assert.True(isStartWithNumber);
        }

        [Fact]
        public void GivenStringStartingWithLetter_DetectStartsWithNumber_ShouldReturnFalse()
        {
            // Arrange
            string input = "Asasdfgd12";

            // Act
            bool isStartWithNumber = StringAnalysis.StartsWithNumber(input);

            // Assert
            Assert.False(isStartWithNumber);
        }

        [Fact]
        public void GivenStringStartingWithPunctuationOrSpace_DetectStartsWithPunctuationOrSpace_ShouldReturnTrue()
        {
            // Arrange
            string input = ".sasdfgd";

            // Act
            bool isStartWithPunctuationOrSpace = StringAnalysis.StartsWithPunctuationOrSpace(input);

            // Assert
            Assert.True(isStartWithPunctuationOrSpace);
        }

        [Fact]
        public void GivenStringStartingWithLetter_DetectStartsWithPunctuationOrSpace_ShouldReturnFalse()
        {
            // Arrange
            string input = "sasdfgd";

            // Act
            bool isStartWithPunctuationOrSpace = StringAnalysis.StartsWithPunctuationOrSpace(input);

            // Assert
            Assert.False(isStartWithPunctuationOrSpace);
        }
    }
}
