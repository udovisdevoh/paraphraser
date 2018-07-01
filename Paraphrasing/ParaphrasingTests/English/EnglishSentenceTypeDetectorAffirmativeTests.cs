using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paraphrasing.Tests
{
    public class EnglishSentenceTypeDetectorAffirmativeTests
    {
        [Theory]
        [InlineData("I listen")]
        [InlineData("whatever you say")]
        [InlineData("what you say is weird")]
        [InlineData("whether you like it or not")]
        public void GivenSentence_ShouldDetectAffirmative(string sentence)
        {
            // Arrange
            SentenceTypeDetector sentenceDetector = new EnglishSentenceTypeDetector();
            SentenceType expectedSentenceType = SentenceType.Affirmative;

            // Act
            SentenceType actualSentenceType = sentenceDetector.GetSentenceType(sentence);

            // Assert
            Assert.Equal(expectedSentenceType, actualSentenceType);
        }

        [Theory]
        [InlineData("y asdgsdg right?")]
        [InlineData("jhrthfdhg  sfdghg hg s sgd ? .. ")]
        public void GivenSentenceWithQuestionMark_ShouldDetectInterrogative(string sentence)
        {
            #warning todo move

            // Arrange
            SentenceTypeDetector sentenceDetector = new EnglishSentenceTypeDetector();
            SentenceType expectedSentenceType = SentenceType.Interrogative;

            // Act
            SentenceType actualSentenceType = sentenceDetector.GetSentenceType(sentence);

            // Assert
            Assert.Equal(expectedSentenceType, actualSentenceType);
        }

        [Theory]
        [InlineData("what do you say")]
        [InlineData("the fuck were you're thinking")]
        [InlineData("which one you prefer")]
        [InlineData("whichever one you prefer")]
        [InlineData("whichsoe'er one you prefer")]
        [InlineData("whichsoever'er one you prefer")]
        [InlineData("who's the best")]
        [InlineData("who is the best")]
        [InlineData("you gave it to whom")]
        [InlineData("you gave it to whomever")]
        [InlineData("you gave it to whoever")]
        [InlineData("you gave it to whose")]
        [InlineData("you gave it to whosoever")]
        public void GivenSentenceContainingInterrogativePronoun_ShouldDetectInterrogative(string sentence)
        {
            #warning todo move

            // Arrange
            SentenceTypeDetector sentenceDetector = new EnglishSentenceTypeDetector();
            SentenceType expectedSentenceType = SentenceType.Interrogative;

            // Act
            SentenceType actualSentenceType = sentenceDetector.GetSentenceType(sentence);

            // Assert
            Assert.Equal(expectedSentenceType, actualSentenceType);
        }

        [Theory]
        [InlineData("are you ready")]
        [InlineData("would you do it")]
        public void GivenSentenceSecondWordPronoun_ShouldDetectInterrogative(string sentence)
        {
            #warning todo move

            // Arrange
            SentenceTypeDetector sentenceDetector = new EnglishSentenceTypeDetector();
            SentenceType expectedSentenceType = SentenceType.Interrogative;

            // Act
            SentenceType actualSentenceType = sentenceDetector.GetSentenceType(sentence);

            // Assert
            Assert.Equal(expectedSentenceType, actualSentenceType);
        }
    }
}
