using LanguageDetection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LanguageDetectionTests
{
    public class CompositeLanguageDetectorTests
    {
        [Fact]
        public void GivenEmptyCompositeLanguageDetector_DetectLanguageShouldThrow()
        {
            // Arrange
            CompositeLanguageDetector compositeLanguageDetector = new CompositeLanguageDetector();

            // Act, Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                compositeLanguageDetector.DetectLanguage("text");
            });
        }

        [Fact]
        public void GivenCompositeLanguageDetector_AddLanguageDetectLanguage_ShouldDetectLanguage()
        {
            // Arrange
            string expectedDetectedLanguage = "English";
            double expectedLanguageProximity = 0.5;
            string actualDetectedLanguage;
            CompositeLanguageDetector compositeLanguageDetector = new CompositeLanguageDetector();
            Mock<ILanguageDetector> languageDetectorMockFactory = new Mock<ILanguageDetector>();
            KeyValuePair<string, double>[] languageProximities = new KeyValuePair<string, double>[1] { new KeyValuePair<string, double>(expectedDetectedLanguage, expectedLanguageProximity) };
            languageDetectorMockFactory.Setup(LanguageDetector => LanguageDetector.GetLanguageProximities("text")).Returns(languageProximities);
            compositeLanguageDetector.AddLanguageDetector(languageDetectorMockFactory.Object);

            // Act
            actualDetectedLanguage = compositeLanguageDetector.DetectLanguage("text");

            // Assert
            Assert.Equal(expectedDetectedLanguage, actualDetectedLanguage);
        }

        [Fact]
        public void GivenCompositeLanguageDetector_GetLanguageProximity()
        {
            // Arrange
            string expectedDetectedLanguage = "English";
            double expectedLanguageProximity = 0.5;
            CompositeLanguageDetector compositeLanguageDetector = new CompositeLanguageDetector();
            Mock<ILanguageDetector> languageDetectorMockFactory = new Mock<ILanguageDetector>();
            KeyValuePair<string, double>[] expectedLanguageProximities = new KeyValuePair<string, double>[1] { new KeyValuePair<string, double>(expectedDetectedLanguage, expectedLanguageProximity) };
            languageDetectorMockFactory.Setup(LanguageDetector => LanguageDetector.GetLanguageProximities("text")).Returns(expectedLanguageProximities);
            compositeLanguageDetector.AddLanguageDetector(languageDetectorMockFactory.Object);

            // Act
            KeyValuePair<string, double>[] actualLanguageProximities = compositeLanguageDetector.GetLanguageProximities("text");

            // Assert
            Assert.Equal(expectedLanguageProximities, actualLanguageProximities);
        }
    }
}
