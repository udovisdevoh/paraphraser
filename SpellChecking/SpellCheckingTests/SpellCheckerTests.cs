using SpellChecking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SpellCheckingTests
{
    public class SpellCheckerTests
    {
        [Fact]
        public void GivenWord_ShouldCorrectSpelling()
        {
            // Arrange
            string wrongSpelling = "obiviously";
            string expectedCorrectedSpelling = "obviously";
            string actualCorrectedSpelling;
            SpellChecker spellChecker = new SpellChecker("./Dictionaries", "english");

            // Act
            actualCorrectedSpelling = spellChecker.GetCorrectedWord(wrongSpelling);

            // Assert
            Assert.Equal(expectedCorrectedSpelling, actualCorrectedSpelling);
        }

        [Fact]
        public void GivenWord_ShouldCorrectSpellingWithCache()
        {
            // Arrange
            string wrongSpelling = "obiviously";
            string expectedCorrectedSpelling = "obviously";
            string actualCorrectedSpelling;
            SpellChecker spellChecker = new SpellChecker("./Dictionaries", "english");

            // Act
            actualCorrectedSpelling = spellChecker.GetCorrectedWord(wrongSpelling); // No cache
            actualCorrectedSpelling = spellChecker.GetCorrectedWord(wrongSpelling); // With cache

            // Assert
            Assert.Equal(expectedCorrectedSpelling, actualCorrectedSpelling);
        }
        [Fact]
        public void GivenText_FindExistingWord_ShouldFindIt()
        {
            // Arrange
            string wordToFind = "StarFishes";
            SpellChecker spellChecker = new SpellChecker("./Dictionaries", "english");

            // Act
            bool result = spellChecker.ContainsWord(wordToFind);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenText_FindFakeWord_ShouldNotFindIt()
        {
            // Arrange
            string wordToFind = "zorf";
            SpellChecker spellChecker = new SpellChecker("./Dictionaries", "english");

            // Act
            bool result = spellChecker.ContainsWord(wordToFind);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GivenText_ShouldCorrectSpelling()
        {
            // Arrange
            string wrongSpelling = "this aint the rght thing to doo arent wont";
            string expectedCorrectedSpelling = "this ain't the right thing to do aren't wont";
            string actualCorrectedSpelling;
            SpellChecker spellChecker = new SpellChecker("./Dictionaries", "english");

            // Act
            actualCorrectedSpelling = spellChecker.GetCorrectedText(wrongSpelling, null);

            // Assert
            Assert.Equal(expectedCorrectedSpelling, actualCorrectedSpelling);
        }

        [Fact]
        public void GivenText_ShouldCorrectSpellingReplaceUnmatchedWord()
        {
            // Arrange
            string wrongSpelling = "this aint the rght thing to doo arent wont ydfgrwqegtrerssssdfff434gwgwtwesedhdf6534dfdfhdg34";
            string expectedCorrectedSpelling = "this ain't the right thing to do aren't wont unmatched";
            string actualCorrectedSpelling;
            SpellChecker spellChecker = new SpellChecker("./Dictionaries", "english");

            // Act
            actualCorrectedSpelling = spellChecker.GetCorrectedText(wrongSpelling, "unmatched");

            // Assert
            Assert.Equal(expectedCorrectedSpelling, actualCorrectedSpelling);
        }


        [Fact]
        public void GivenText_CountExistingWords()
        {
            // Arrange
            string[] wordsToFind = new string[] { "starfishes", "yes", "models", "criterion", "criteria", "zorf" };
            SpellChecker spellChecker = new SpellChecker("./Dictionaries", "english");
            int expectedExistingWordCount = 5;

            // Act
            int actualExistingWordCount = spellChecker.CountExistingWords(wordsToFind);

            // Assert
            Assert.Equal(expectedExistingWordCount, actualExistingWordCount);
        }
    }
}
