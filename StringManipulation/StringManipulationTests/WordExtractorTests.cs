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
            string[] expectedWords = { "'", "oh", ",", " ", "you", " ", "&", " ", "can", "'", "t", "." };

            // Act
            string[] actualWords = WordExtractor.GetWordsAndPunctuationTokens(text);

            // Assert
            Assert.Equal(expectedWords, actualWords);
        }

        [Fact]
        public void GivenText_GetLowerInvarianWords_ShouldGetLowercasedWordsOnly()
        {
            // Arrange
            string text = "'Oh, you & can't.";
            string[] expectedWords = { "oh", "you", "can", "t" };

            // Act
            string[] actualWords = WordExtractor.GetLowerInvariantWords(text);

            // Assert
            Assert.Equal(expectedWords, actualWords);
        }

        [Fact]
        public void GivenTextEndingWithWordStartingWithWord_GetWordsAndPunctuationTokens_ShouldGetWords()
        {
            // Arrange
            string text = "Oh, you & can't. zarf";
            string[] expectedWords = { "oh", ",", " ", "you", " ", "&", " ", "can", "'", "t", ".", " ", "zarf" };

            // Act
            string[] actualWords = WordExtractor.GetWordsAndPunctuationTokens(text);

            // Assert
            Assert.Equal(expectedWords, actualWords);
        }

        [Fact]
        public void GivenLanguageWordCountProbabiltyAndWordList_ShouldGetWordListSumOfProbability()
        {
            // Arrange
            Dictionary<string, double> languageWordCountProbability = new Dictionary<string, double>() { { "this", 0.7 }, { "is", 0.4 }, { "a", 0.1 } };
            string[] inputWords = new string[] { "this", "is" };
            double expectedSumOfProbability = 1.1;

            // Act
            double actualSumOfProbability = WordExtractor.GetExistingWordsSumOfProbabilities(languageWordCountProbability, inputWords);

            // Assert
            Assert.Equal(expectedSumOfProbability, actualSumOfProbability);
        }
    }
}
