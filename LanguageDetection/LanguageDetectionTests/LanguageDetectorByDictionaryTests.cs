using LanguageDetection;
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
    public class LanguageDetectorByDictionaryTests
    {
        [Fact]
        public void GivenLanguageDetectorByDictionaryWithLanguages_ShouldGetLanguages()
        {
            // Arrange
            Mock<ISpellChecker> spellCheckingMock = new Mock<ISpellChecker>();
            LanguageDetectorByDictionary languageDetectorByDictionary = new LanguageDetectorByDictionary();
            IEnumerable<string> expectedLanguageList = new string[] { "English", "French" };

            // Act
            languageDetectorByDictionary.AddLanguage("English", spellCheckingMock.Object);
            languageDetectorByDictionary.AddLanguage("French", spellCheckingMock.Object);
            IEnumerable<string> actualLanguageList = languageDetectorByDictionary.GetLanguageList();

            // Assert
            Assert.Equal(expectedLanguageList.OrderBy(value => value), actualLanguageList.OrderBy(value => value));
        }

        [Fact]
        public void GivenTwoLanguagesAndText_ShouldDetectFrench()
        {
            // Arrange
            LanguageDetectorByDictionary languageDetector = new LanguageDetectorByDictionary();
            Mock<ISpellChecker> frenchSpellCheckingMock = new Mock<ISpellChecker>();
            frenchSpellCheckingMock.Setup(spellCheck => spellCheck.CountExistingWords(It.IsAny<string[]>())).Returns(2);

            Mock<ISpellChecker> englishSpellCheckingMock = new Mock<ISpellChecker>();
            englishSpellCheckingMock.Setup(spellCheck => spellCheck.CountExistingWords(It.IsAny<string[]>())).Returns(0);

            // Act
            languageDetector.AddLanguage("French", frenchSpellCheckingMock.Object);
            languageDetector.AddLanguage("English", englishSpellCheckingMock.Object);

            // Assert
            Assert.Equal("French", languageDetector.DetectLanguage("un chapeau"));
        }

        [Fact]
        public void GivenTwoLanguagesAndText_ShouldDetectEnglish()
        {
            // Arrange
            LanguageDetectorByDictionary languageDetector = new LanguageDetectorByDictionary();
            Mock<ISpellChecker> frenchSpellCheckingMock = new Mock<ISpellChecker>();
            frenchSpellCheckingMock.Setup(spellCheck => spellCheck.CountExistingWords(It.IsAny<string[]>())).Returns(0);

            Mock<ISpellChecker> englishSpellCheckingMock = new Mock<ISpellChecker>();
            englishSpellCheckingMock.Setup(spellCheck => spellCheck.CountExistingWords(It.IsAny<string[]>())).Returns(2);

            // Act
            languageDetector.AddLanguage("French", frenchSpellCheckingMock.Object);
            languageDetector.AddLanguage("English", englishSpellCheckingMock.Object);

            // Assert
            Assert.Equal("English", languageDetector.DetectLanguage("a hat"));
        }

        [Fact]
        public void GivenTwoLanguagesAndText_ShouldGetLanguagesByProximities()
        {
            // Arrange
            LanguageDetectorByDictionary languageDetector = new LanguageDetectorByDictionary();
            Mock<ISpellChecker> frenchSpellCheckingMock = new Mock<ISpellChecker>();
            frenchSpellCheckingMock.Setup(spellCheck => spellCheck.CountExistingWords(It.IsAny<string[]>())).Returns(0);

            Mock<ISpellChecker> englishSpellCheckingMock = new Mock<ISpellChecker>();
            englishSpellCheckingMock.Setup(spellCheck => spellCheck.CountExistingWords(It.IsAny<string[]>())).Returns(2);

            // Act
            languageDetector.AddLanguage("French", frenchSpellCheckingMock.Object);
            languageDetector.AddLanguage("English", englishSpellCheckingMock.Object);
            KeyValuePair<string, double>[] languagesByProximities = languageDetector.GetLanguageProximities("a hat");

            // Assert
            Assert.Equal(1, languagesByProximities[0].Value);
        }
    }
}
