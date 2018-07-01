using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paraphrasing.Tests.English
{
    public class EnglishSentenceTypeDetectorInterrogativeTests
    {
        [Theory]
        [InlineData("ain't that just the place you wish you were?")]
        [InlineData("anybody out there trying to get through?")]
        [InlineData("are our hearts still there?")]
        [InlineData("aren't you humans supposed to look like me?")]
        [InlineData("but is it me or mr. bones rappin'?")]
        public void GivenInterrogativeSentence_ShouldBeDetectedRegardlessQuestionMark(string sentence)
        {
            // Arrange
            sentence = sentence.Replace("?", "");
            SentenceTypeDetector sentenceDetector = new EnglishSentenceTypeDetector();
            SentenceType expectedSentenceType = SentenceType.Interrogative;

            // Act
            SentenceType actualSentenceType = sentenceDetector.GetSentenceType(sentence);

            // Assert
            Assert.Equal(expectedSentenceType, actualSentenceType);
        }
    }
}
