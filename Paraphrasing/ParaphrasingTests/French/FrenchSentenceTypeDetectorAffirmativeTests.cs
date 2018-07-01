using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paraphrasing.Tests
{
    public class FrenchSentenceTypeDetectorAffirmativeTests
    {
        [Theory]
        [InlineData("j'écoute")]
        [InlineData("jhrthfdhg  sfdghg hg s sgd")]
        [InlineData("tu veux")]
        [InlineData("quoi que vous fassiez")]
        [InlineData("quoi qu'il en soit")]
        [InlineData("vous dites")]
        [InlineData("tel quelle")]
        [InlineData("tel quel")]
        [InlineData("tel quelles")]
        [InlineData("tel quels")]
        [InlineData("tel-quelle")]
        [InlineData("tel-quel")]
        [InlineData("tel-quelles")]
        [InlineData("tel-quels")]
        [InlineData("tels quelle")]
        [InlineData("tels quel")]
        [InlineData("tels quelles")]
        [InlineData("tels quels")]
        [InlineData("tels-quelle")]
        [InlineData("tels-quel")]
        [InlineData("tels-quelles")]
        [InlineData("tels-quels")]
        [InlineData("telle quelle")]
        [InlineData("telle quel")]
        [InlineData("telle quelles")]
        [InlineData("telle quels")]
        [InlineData("telle-quelle")]
        [InlineData("telle-quel")]
        [InlineData("telle-quelles")]
        [InlineData("telle-quels")]
        [InlineData("telles quelle")]
        [InlineData("telles quel")]
        [InlineData("telles quelles")]
        [InlineData("telles quels")]
        [InlineData("telles-quelle")]
        [InlineData("telles-quel")]
        [InlineData("telles-quelles")]
        [InlineData("telles-quels")]
        public void GivenSentence_ShouldDetectAffirmative(string sentence)
        {
            // Arrange
            SentenceTypeDetector sentenceDetector = new FrenchSentenceTypeDetector();
            SentenceType expectedSentenceType = SentenceType.Affirmative;

            // Act
            SentenceType actualSentenceType = sentenceDetector.GetSentenceType(sentence);

            // Assert
            Assert.Equal(expectedSentenceType, actualSentenceType);
        }

        [Theory]
        [InlineData("y asdgsdg beau?")]
        [InlineData("jhrthfdhg  sfdghg hg s sgd ? .. ")]
        public void GivenSentenceWithQuestionMark_ShouldDetectInterrogative(string sentence)
        {
            #warning todo move

            // Arrange
            SentenceTypeDetector sentenceDetector = new FrenchSentenceTypeDetector();
            SentenceType expectedSentenceType = SentenceType.Interrogative;

            // Act
            SentenceType actualSentenceType = sentenceDetector.GetSentenceType(sentence);

            // Assert
            Assert.Equal(expectedSentenceType, actualSentenceType);
        }

        [Theory]
        [InlineData("que fais-elle")]
        [InlineData("est-ce qu'elle veux")]
        [InlineData("qu'a-telle fait")]
        [InlineData("qui est-elle")]
        [InlineData("quand ira-t-elle")]
        [InlineData("elle veut faire quoi")]
        [InlineData("pourquoi vous faites ça")]
        [InlineData("voulez-vous faire ça")]
        [InlineData("comment fait-elle")]
        [InlineData("quel est son nom")]
        [InlineData("quels sont les indices")]
        [InlineData("quelle est sa chanson préférée")]
        [InlineData("quelles sons mes chances")]
        [InlineData("est-ce")]
        [InlineData("puis-je")]
        [InlineData("peux-tu")]
        [InlineData("peux-t-il")]
        [InlineData("peux-t-elle")]
        [InlineData("pouvons-nous")]
        [InlineData("voulez-vous")]
        [InlineData("peuvent-ils")]
        [InlineData("peuvent-elles")]
        [InlineData("peut-on")]
        [InlineData("à qui")]
        [InlineData("de qui")]
        public void GivenSentenceContainingInterrogativePronoun_ShouldDetectInterrogative(string sentence)
        {
            #warning todo move

            // Arrange
            SentenceTypeDetector sentenceDetector = new FrenchSentenceTypeDetector();
            SentenceType expectedSentenceType = SentenceType.Interrogative;

            // Act
            SentenceType actualSentenceType = sentenceDetector.GetSentenceType(sentence);

            // Assert
            Assert.Equal(expectedSentenceType, actualSentenceType);
        }
    }
}
