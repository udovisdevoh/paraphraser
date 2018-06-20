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
            #warning Replace TextMarkovMatrixLoader and MarkovMatrixNormalizer with mocks, remove assembly reference

            // Arrange
            LanguageDetector languageDetector = new LanguageDetector(new TextMarkovMatrixLoader<ulong>(), new MarkovMatrixNormalizer());
            IMarkovMatrix<float> englishMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();

            // Act
            languageDetector.AddLanguage("English", englishMatrix);

            // Assert
            Assert.Equal(1, languageDetector.Count);
        }

        [Fact]
        public void GivenNoLanguagesAndText_DetectLanguage_ShouldThrow()
        {
            #warning Replace TextMarkovMatrixLoader and MarkovMatrixNormalizer with mocks, remove assembly reference

            // Arrange
            LanguageDetector languageDetector = new LanguageDetector(new TextMarkovMatrixLoader<ulong>(), new MarkovMatrixNormalizer());

            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                // Act
                languageDetector.DetectLanguage("je suis un");
            });
        }

        [Fact]
        public void GivenTwoLanguagesAndText_ShouldDetectFrench()
        {
            // Arrange
            LanguageDetector languageDetector = new LanguageDetector(new TextMarkovMatrixLoader<ulong>(), new MarkovMatrixNormalizer());
            IMarkovMatrix<float> englishMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrix<float> frenchMatrix = LanguageDetectorTestHelper.BuildFrenchLanguageMatrixMock();

            // Act
            languageDetector.AddLanguage("English", englishMatrix);
            languageDetector.AddLanguage("French", frenchMatrix);

            // Assert
            Assert.Equal("French", languageDetector.DetectLanguage("ça c'est est du"));
        }

        [Fact]
        public void GivenTwoLanguagesAndText_ShouldDetectEnglish()
        {
            // Arrange
            LanguageDetector languageDetector = new LanguageDetector(new TextMarkovMatrixLoader<ulong>(), new MarkovMatrixNormalizer());
            IMarkovMatrix<float> englishMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrix<float> frenchMatrix = LanguageDetectorTestHelper.BuildFrenchLanguageMatrixMock();

            // Act
            languageDetector.AddLanguage("English", englishMatrix);
            languageDetector.AddLanguage("French", frenchMatrix);

            // Assert
            Assert.Equal("English", languageDetector.DetectLanguage("that is"));
        }
    }
}
