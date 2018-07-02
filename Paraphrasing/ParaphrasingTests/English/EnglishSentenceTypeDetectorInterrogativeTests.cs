using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paraphrasing.Tests.English
{
    public class EnglishSentenceTypeDetectorInterrogativeTests
    {
        [Fact]
        public void GivenInterrogativeSentences_ShouldBeDetectedRegardlessQuestionMark()
        {
            string line;
            using (StreamReader streamReader = new StreamReader("./SentencesSamples/en.interrogative.short.txt"))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    GivenInterrogativeSentence_ShouldBeDetectedRegardlessQuestionMark(line);
                }
            }
        }

        private void GivenInterrogativeSentence_ShouldBeDetectedRegardlessQuestionMark(string sentence)
        {
            // Arrange
            sentence = sentence.Replace("?", "");
            SentenceTypeDetector sentenceDetector = new EnglishSentenceTypeDetector();
            SentenceType expectedSentenceType = SentenceType.Interrogative;

            // Act
            Debug.WriteLine(sentence);
            SentenceType actualSentenceType = sentenceDetector.GetSentenceType(sentence);

            // Assert
            if (expectedSentenceType != actualSentenceType)
            {
                Assert.Equal("should be interrogative sentence", sentence); // hack pour afficher dans la console de xUnit
            }
            Assert.Equal(expectedSentenceType, actualSentenceType);
        }
    }
}
