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
        [InlineData("I think what you say is weird")]
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
    }
}
