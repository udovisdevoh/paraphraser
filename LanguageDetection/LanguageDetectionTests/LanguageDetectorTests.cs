using LanguageDetection;
using LanguageDetection.TestHelpers;
using MarkovMatrices;
using MarkovMatrices.TestHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LanguageDetectionTests
{
    public class LanguageDetectorTests
    {
        [Fact]
        public void GivenLanguageDetector_AddLanguage()
        {
            // Arrange
            LanguageDetector languageDetector = new LanguageDetector(new TextMarkovMatrixLoader<ulong>(), new MarkovMatrixNormalizer());
            IMarkovMatrix<float> englishMatrix = LanguageDetectorTestHelper.BuildLanguageMatrix("this is english text");

            // Act
            languageDetector.AddLanguage("English", englishMatrix);

            // Assert
            Assert.Equal(1, languageDetector.Count);
        }

        [Fact]
        public void GivenTwoLanguagesAndText_ShouldDetectFrench()
        {
            // Arrange
            LanguageDetector languageDetector = new LanguageDetector(new TextMarkovMatrixLoader<ulong>(), new MarkovMatrixNormalizer());
            IMarkovMatrix<float> englishMatrix = LanguageDetectorTestHelper.BuildLanguageMatrix("this is english text");
            IMarkovMatrix<float> frenchMatrix = LanguageDetectorTestHelper.BuildLanguageMatrix("ceci est du texte en français");

            // Act
            languageDetector.AddLanguage("English", englishMatrix);
            languageDetector.AddLanguage("French", frenchMatrix);

            // Assert
            Assert.Equal("French", languageDetector.DetectLanguage("je suis un"));
        }

        [Fact]
        public void GivenTwoLanguagesAndText_ShouldDetectEnglish()
        {
            // Arrange
            LanguageDetector languageDetector = new LanguageDetector(new TextMarkovMatrixLoader<ulong>(), new MarkovMatrixNormalizer());
            IMarkovMatrix<float> englishMatrix = LanguageDetectorTestHelper.BuildLanguageMatrix("this is english text");
            IMarkovMatrix<float> frenchMatrix = LanguageDetectorTestHelper.BuildLanguageMatrix("ceci est du texte en français");

            // Act
            languageDetector.AddLanguage("English", englishMatrix);
            languageDetector.AddLanguage("French", frenchMatrix);

            // Assert
            Assert.Equal("English", languageDetector.DetectLanguage("I am a"));
        }
    }
}
