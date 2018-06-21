using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StringManipulation.Tests
{
    public class WordExtractorTests
    {
        [Fact]
        public void GivenText_GetWordsAndPunctuationTokens_ShouldGetWords()
        {
            // Arrange
            string text = "'Oh, you & can't.";
            string[] expectedWords = { "'", "Oh", ",", " ", "you", " ", "&", " ", "can", "'", "t", "." };

            // Act
            string[] actualWords = WordExtractor.GetWordsAndPunctuationTokens(text);

            // Assert
            Assert.Equal(expectedWords, actualWords);
        }

        [Fact]
        public void GivenTextEndingWithWordStartingWithWord_GetWordsAndPunctuationTokens_ShouldGetWords()
        {
            // Arrange
            string text = "Oh, you & can't. zarf";
            string[] expectedWords = { "Oh", ",", " ", "you", " ", "&", " ", "can", "'", "t", ".", " ", "zarf" };

            // Act
            string[] actualWords = WordExtractor.GetWordsAndPunctuationTokens(text);

            // Assert
            Assert.Equal(expectedWords, actualWords);
        }
    }
}
