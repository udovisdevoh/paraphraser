using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StringManipulation.Tests
{
    public class StringFormatterTests
    {
        [Fact]
        public void GivenString_FormatLanguageName_GetFormattedName()
        {
            // Arrange
            string unformattedName = "   la Langue\n\r\t de Shake'n'Bake";
            string expectedFormattedName = "La langue de shake'n'bake";
            string actualFormattedName;

            // Act
            actualFormattedName = StringFormatter.FormatLanguageName(unformattedName);

            // Assert
            Assert.Equal(expectedFormattedName, actualFormattedName);
        }

        [Fact]
        public void GivenString_FormatInputText_GetFormattedInputText()
        {
            // Arrange
            string unformattedName = "   la Langue\n\r\t , de Shake'n'Bakœ.";
            string expectedFormattedName = "La langue de shake'n'bakoe";
            string actualFormattedName;

            // Act
            actualFormattedName = StringFormatter.FormatInputText(unformattedName);

            // Assert
            Assert.Equal(expectedFormattedName, actualFormattedName);
        }

        [Fact]
        public void GivenAccentedText_RemoveDiacritics()
        {
            // Arrange
            string accentedText = "ÉÛÌÔËÀ C'est à l'école qu'il faut faire ses leçons avant Noël Ññãõă Șș Țț Ğğ Şş Ăă ẞß Ççøå";
            string expectedTextWithoutAccents = "EUIOEA C'est a l'ecole qu'il faut faire ses lecons avant Noel Nnaoa Ss Tt Gg Ss Aa Ss Ccoa";

            // Act
            string actualTextWithoutAccents = StringFormatter.RemoveDiacritics(accentedText);

            // Assert
            Assert.Equal(expectedTextWithoutAccents, actualTextWithoutAccents);
        }

        [Fact]
        public void GivenAccentedChar_RemoveDiacritics()
        {
            // Arrange
            char accentedChar = 'Â';
            char expectedCharWithoutAccents = 'A';

            // Act
            char actualCharWithoutAccents = StringFormatter.RemoveDiacritics(accentedChar);

            // Assert
            Assert.Equal(expectedCharWithoutAccents, actualCharWithoutAccents);
        }

        [Fact]
        public void GivenText_FixApostrophe()
        {
            // Arrange
            string sourceText = "Apostrophe ’’’";
            string expectedFixedText = "Apostrophe '''";

            // Act
            string actualFixedText = StringFormatter.FixApostrophe(sourceText);

            // Assert
            Assert.Equal(expectedFixedText, actualFixedText);
        }

        [Fact]
        public void GivenText_RemovePunctuation()
        {
            // Arrange
            string sourceText = "b.,?\"&*a!' ";
            string expectedFixedText = "b    & a ' ";

            // Act
            string actualFixedText = StringFormatter.RemovePunctuation(sourceText, '&', '\'');

            // Assert
            Assert.Equal(expectedFixedText, actualFixedText);
        }

        [Fact]
        public void GivenText_RemoveDoubleSpacesEntersTabsEtc()
        {
            // Arrange
            string sourceText = "   haha     ha \t \r \n \n \t  ";
            string expectedFixedText = "haha ha";

            // Act
            string actualFixedText = StringFormatter.RemoveDoubleTabsSpacesAndEnters(sourceText);

            // Assert
            Assert.Equal(expectedFixedText, actualFixedText);
        }

        [Fact]
        public void GivenText_UcFirst()
        {
            // Arrange
            string sourceText = "i am";
            string expectedFixedText = "I am";

            // Act
            string actualFixedText = StringFormatter.UcFirst(sourceText);

            // Assert
            Assert.Equal(expectedFixedText, actualFixedText);
        }

        [Fact]
        public void GivenEmptyText_UcFirst()
        {
            // Arrange
            string sourceText = "";
            string expectedFixedText = "";

            // Act
            string actualFixedText = StringFormatter.UcFirst(sourceText);

            // Assert
            Assert.Equal(expectedFixedText, actualFixedText);
        }

        [Fact]
        public void GivenStringAndChar_ShouldSplitBeforeCharacter()
        {
            // Arrange
            string input = ".3456sdfgsdg/sdfga34";
            string expectedOutput = ".3456sdfgsdg";
            char character = '/';

            // Act
            string actualOutput = StringFormatter.SplitBefore(input, character);

            // Assert
            Assert.Equal(expectedOutput, actualOutput);
        }

        [Fact]
        public void GivenText_RemoveLigatures()
        {
            // Arrange
            string sourceText = "œŒæÆ";
            string expectedFixedText = "oeOEaeAE";

            // Act
            string actualFixedText = StringFormatter.RemoveLigatures(sourceText);

            // Assert
            Assert.Equal(expectedFixedText, actualFixedText);
        }
    }
}
