using LanguageDetection;
using LanguageDetection.TestHelpers;
using MarkovMatrices;
using Moq;
using SpellChecking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LanguageDetectionTests
{
    public class LanguageDetectorByLeastCorrectionTest
    {
        [Fact]
        public void GivenLanguageDetectorByLeastCorrection_AddLanguage_ShouldAddLanguage()
        {
            // Arrange
            IMarkovMatrix<double> outputMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrixLoader<double> comparisonMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(outputMatrix);

            LanguageDetectorByLeastCorrection languageDetectorByLeastCorrection = new LanguageDetectorByLeastCorrection(comparisonMatrixLoader);
            Mock<ISpellChecker> frenchSpellCheckingMock = new Mock<ISpellChecker>();
            frenchSpellCheckingMock.Setup(spellCheck => spellCheck.CountExistingWords(It.IsAny<string[]>())).Returns(2);

            Mock<ISpellChecker> englishSpellCheckingMock = new Mock<ISpellChecker>();
            englishSpellCheckingMock.Setup(spellCheck => spellCheck.CountExistingWords(It.IsAny<string[]>())).Returns(0);

            // Act
            languageDetectorByLeastCorrection.AddLanguage("French", frenchSpellCheckingMock.Object);
        }

        [Fact]
        public void GivenLanguageDetectorByLeastCorrectionAndTwoLanguages_ShouldGetLanguages()
        {
            // Arrange
            IMarkovMatrix<double> outputMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrixLoader<double> comparisonMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(outputMatrix);

            LanguageDetectorByLeastCorrection languageDetectorByLeastCorrection = new LanguageDetectorByLeastCorrection(comparisonMatrixLoader);
            Mock<ISpellChecker> frenchSpellCheckingMock = new Mock<ISpellChecker>();

            Mock<ISpellChecker> englishSpellCheckingMock = new Mock<ISpellChecker>();

            string[] expectedLanguageList = new string[] { "French", "English" };

            // Act
            languageDetectorByLeastCorrection.AddLanguage("French", frenchSpellCheckingMock.Object);
            languageDetectorByLeastCorrection.AddLanguage("English", englishSpellCheckingMock.Object);
            IEnumerable<string> actualLanguageList = languageDetectorByLeastCorrection.GetLanguageList();

            // Assert
            Assert.Equal(expectedLanguageList.OrderBy(value => value), actualLanguageList.OrderBy(value => value));
        }

        [Fact]
        public void GivenTwoLanguagesAndText_ShouldGetLanguageProximities()
        {
            // Arrange
            IMarkovMatrix<double> outputMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrixLoader<double> comparisonMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(outputMatrix);

            // Arrange
            LanguageDetectorByLeastCorrection languageDetector = new LanguageDetectorByLeastCorrection(comparisonMatrixLoader);
            Mock<ISpellChecker> frenchSpellCheckingMock = new Mock<ISpellChecker>();
            frenchSpellCheckingMock.Setup(spellCheck => spellCheck.GetCorrectedText(It.IsAny<string>(), It.IsAny<string>())).Returns("ceci est une poule");

            Mock<ISpellChecker> englishSpellCheckingMock = new Mock<ISpellChecker>();
            englishSpellCheckingMock.Setup(spellCheck => spellCheck.GetCorrectedText(It.IsAny<string>(), It.IsAny<string>())).Returns("such est une pool");

            // Act
            languageDetector.AddLanguage("French", frenchSpellCheckingMock.Object);
            languageDetector.AddLanguage("English", englishSpellCheckingMock.Object);
            KeyValuePair<string, double>[] languagesByProximities = languageDetector.GetLanguageProximities("cecci est une poole");

            // Assert
            Assert.Equal(1, languagesByProximities[0].Value);
        }
    }
}
