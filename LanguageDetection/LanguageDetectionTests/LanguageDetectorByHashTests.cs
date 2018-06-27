using LanguageDetection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LanguageDetectionTests
{
    public class LanguageDetectorByHashTests
    {
        [Fact]
        public void GivenTwoLanguagesAndText_ShouldGetLanguagesByProximities()
        {
            // Arrange
            LanguageDetectorByHash languageDetectorByHash = new LanguageDetectorByHash();

            Dictionary<string, double> frenchWordList = new Dictionary<string, double>() { { "ceci", 1 }, { "est", 0.75 }, { "un", 0.25 }, { "chapeau", 0.1 } };
            Dictionary<string, double> englishWordList = new Dictionary<string, double>() { { "this", 1 }, { "is", 0.75 }, { "a", 0.25 }, { "hat", 0.1 } };

            // Act
            languageDetectorByHash.AddLanguage("French", frenchWordList);
            languageDetectorByHash.AddLanguage("English", englishWordList);
            KeyValuePair<string, double>[] languagesByProximities = languageDetectorByHash.GetLanguageProximities("a hat");

            // Assert
            Assert.Equal(0.175, languagesByProximities[0].Value);
        }

        [Fact]
        public void GivenTwoLanguagesAndEnglishText_DetectLanguage_ShouldDetectEnglish()
        {
            // Arrange
            string inputText = "a hat";
            string expectedLanguage = "English";
            LanguageDetectorByHash languageDetectorByHash = new LanguageDetectorByHash();

            Dictionary<string, double> frenchWordList = new Dictionary<string, double>() { { "ceci", 1 }, { "est", 0.75 }, { "un", 0.25 }, { "chapeau", 0.1 } };
            Dictionary<string, double> englishWordList = new Dictionary<string, double>() { { "this", 1 }, { "is", 0.75 }, { "a", 0.25 }, { "hat", 0.1 } };

            // Act
            languageDetectorByHash.AddLanguage("French", frenchWordList);
            languageDetectorByHash.AddLanguage("English", englishWordList);
            string actualLanguage = languageDetectorByHash.DetectLanguage(inputText);

            // Assert
            Assert.Equal(expectedLanguage, actualLanguage);
        }

        [Fact]
        public void GivenTwoLanguagesAndFrenchText_DetectLanguage_ShouldDetectFrench()
        {
            // Arrange
            string inputText = "un chapeau";
            string expectedLanguage = "French";
            LanguageDetectorByHash languageDetectorByHash = new LanguageDetectorByHash();

            Dictionary<string, double> frenchWordList = new Dictionary<string, double>() { { "ceci", 1 }, { "est", 0.75 }, { "un", 0.25 }, { "chapeau", 0.1 } };
            Dictionary<string, double> englishWordList = new Dictionary<string, double>() { { "this", 1 }, { "is", 0.75 }, { "a", 0.25 }, { "hat", 0.1 } };

            // Act
            languageDetectorByHash.AddLanguage("French", frenchWordList);
            languageDetectorByHash.AddLanguage("English", englishWordList);
            string actualLanguage = languageDetectorByHash.DetectLanguage(inputText);

            // Assert
            Assert.Equal(expectedLanguage, actualLanguage);
        }
    }
}
