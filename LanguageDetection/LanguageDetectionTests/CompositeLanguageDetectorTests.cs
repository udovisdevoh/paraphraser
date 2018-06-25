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
        public void GivenCompositeLanguageDetector_AddComponentDetectorWithMatchingLanguage_ShouldNotThrow()
        {
            // Arrange
            CompositeLanguageDetector compositeLanguageDetector = new CompositeLanguageDetector();

            Mock<ILanguageDetector> languageDetectorMockFactory1 = new Mock<ILanguageDetector>();
            languageDetectorMockFactory1.Setup(LanguageDetector => LanguageDetector.GetLanguageList()).Returns(new string[] { "French", "English" });

            Mock<ILanguageDetector> languageDetectorMockFactory2 = new Mock<ILanguageDetector>();
            languageDetectorMockFactory2.Setup(LanguageDetector => LanguageDetector.GetLanguageList()).Returns(new string[] { "French", "English" });

            // Act, Assert
            compositeLanguageDetector.AddLanguageDetector(languageDetectorMockFactory1.Object);
            compositeLanguageDetector.AddLanguageDetector(languageDetectorMockFactory2.Object);
        }

        [Fact]
        public void GivenCompositeLanguageDetector_AddComponentDetectorWithMissingLanguage_ShouldThrow()
        {
            // Arrange
            CompositeLanguageDetector compositeLanguageDetector = new CompositeLanguageDetector();

            Mock<ILanguageDetector> languageDetectorMockFactory1 = new Mock<ILanguageDetector>();
            languageDetectorMockFactory1.Setup(LanguageDetector => LanguageDetector.GetLanguageList()).Returns(new string[] { "French", "English" });

            Mock<ILanguageDetector> languageDetectorMockFactory2 = new Mock<ILanguageDetector>();
            languageDetectorMockFactory2.Setup(LanguageDetector => LanguageDetector.GetLanguageList()).Returns(new string[] { "French" });

            // Act
            compositeLanguageDetector.AddLanguageDetector(languageDetectorMockFactory1.Object);

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                compositeLanguageDetector.AddLanguageDetector(languageDetectorMockFactory2.Object);
            });
        }

        [Fact]
        public void GivenCompositeLanguageDetector_AddComponentDetectorWithExceedingLanguage_ShouldThrow()
        {
            // Arrange
            CompositeLanguageDetector compositeLanguageDetector = new CompositeLanguageDetector();

            Mock<ILanguageDetector> languageDetectorMockFactory1 = new Mock<ILanguageDetector>();
            languageDetectorMockFactory1.Setup(LanguageDetector => LanguageDetector.GetLanguageList()).Returns(new string[] { "French", "English" });

            Mock<ILanguageDetector> languageDetectorMockFactory2 = new Mock<ILanguageDetector>();
            languageDetectorMockFactory2.Setup(LanguageDetector => LanguageDetector.GetLanguageList()).Returns(new string[] { "French", "English", "Spanish" });

            // Act
            compositeLanguageDetector.AddLanguageDetector(languageDetectorMockFactory1.Object);

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                compositeLanguageDetector.AddLanguageDetector(languageDetectorMockFactory2.Object);
            });
        }

        [Fact]
        public void GivenCompositeLanguageDetector_AddComponentDetectorWithNoLanguage_ShouldThrow()
        {
            // Arrange
            CompositeLanguageDetector compositeLanguageDetector = new CompositeLanguageDetector();

            Mock<ILanguageDetector> languageDetectorMockFactory = new Mock<ILanguageDetector>();
            languageDetectorMockFactory.Setup(LanguageDetector => LanguageDetector.GetLanguageList()).Returns(new string[] { });

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                compositeLanguageDetector.AddLanguageDetector(languageDetectorMockFactory.Object);
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
            languageDetectorMockFactory.Setup(LanguageDetector => LanguageDetector.GetLanguageList()).Returns(new string[] { expectedDetectedLanguage });
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
            languageDetectorMockFactory.Setup(LanguageDetector => LanguageDetector.GetLanguageList()).Returns(new string[] { expectedDetectedLanguage });
            compositeLanguageDetector.AddLanguageDetector(languageDetectorMockFactory.Object);

            // Act
            KeyValuePair<string, double>[] actualLanguageProximities = compositeLanguageDetector.GetLanguageProximities("text");

            // Assert
            Assert.Equal(expectedLanguageProximities, actualLanguageProximities);
        }
    }
}
