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
        public void GivenText_ShouldCorrectSpelling()
        {
            // Arrange
            string wrongSpelling = "this aint the rght thing to doo arent wont";
            string expectedCorrectedSpelling = "this ain't the right thing to do aren't wont";
            string actualCorrectedSpelling;
            SpellChecker spellChecker = new SpellChecker("./Dictionaries", "english");

            // Act
            actualCorrectedSpelling = spellChecker.GetCorrectedText(wrongSpelling);

            // Assert
            Assert.Equal(expectedCorrectedSpelling, actualCorrectedSpelling);
        }
    }
}
