using MarkovMatrices.FromText;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarkovMatrices.Tests
{
    public class LanguageDictionaryFileMatrixLoaderTests
    {
        [Fact]
        public void GivenStringFirstLine_PerformLineTransformations_ShouldGetEmptyStrings()
        {
            // Arrange
            string input = "Zarf";
            string expectedOutput = string.Empty;
            LanguageDictionaryFileMatrixLoader<ulong> languageDictionaryFileMatrixLoader = new LanguageDictionaryFileMatrixLoader<ulong>();

            // Act
            string actualOutput = languageDictionaryFileMatrixLoader.PerformLineTransformations(input, 0);

            Assert.Equal(expectedOutput, actualOutput);
        }

        [Theory]
        [InlineData("125472", "")]
        [InlineData("0/nm", "")]
        [InlineData("Amerikaans/E", "amerikaans")]
        [InlineData("stretch/BZGMDRS", "stretch")]
        [InlineData("::::::::::::::", "")]
        [InlineData("stopwords.dic", "")]
        [InlineData("حاء/57", "حاء")]
        [InlineData("أولسوي", "أولسوي")]
        [InlineData("ethnocentrism/M", "ethnocentrism")]
        [InlineData("euphemistically", "euphemistically")]
        [InlineData("	Version: 20161207", "")]
        [InlineData("zzgl.", "zzgl.")]
        [InlineData("can't", "can't")]
        [InlineData("can't/", "can't")]
        [InlineData("aña/234", "aña")]
        [InlineData("অকালবর্ষণ", "অকালবর্ষণ")]
        [InlineData("A-prøve/CEG", "a-prøve")]
        [InlineData("øygruppes/", "øygruppes")]
        [InlineData("ⲥⲡⲉⲣⲙⲟⲗⲟⲅⲟⲥ/GM", "ⲥⲡⲉⲣⲙⲟⲗⲟⲅⲟⲥ")]
        [InlineData("vidneforklaring/70,73,7,976,939,947", "vidneforklaring")]
        [InlineData("'seminari", "'seminari")]
        [InlineData("התחנחנויותיכן/c", "התחנחנויותיכן")]
        [InlineData("स्वकार्य", "स्वकार्य")]
        [InlineData("  101388", "")]
        [InlineData("/ The \"Estensione linguistica italiana - Italian Writing Aids extension\"", "")]
        [InlineData("////", "")]
        [InlineData("Abednego//", "abednego")]
        [InlineData("##################################################################", "")]
        [InlineData("# A copy should be enclosed.                                     #", "")]
        [InlineData("(	[CAT=punct2e]", "")]
        public void GivenString_PerformLineTransformations_ShouldGetFixedStrings(string input, string expectedOutput)
        {
            // Arrange
            LanguageDictionaryFileMatrixLoader<ulong> languageDictionaryFileMatrixLoader = new LanguageDictionaryFileMatrixLoader<ulong>();

            // Act
            string actualOutput = languageDictionaryFileMatrixLoader.PerformLineTransformations(input, 17);

            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}
