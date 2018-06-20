using LanguageDetection;
using LanguageDetection.TestHelpers;
using MarkovMatrices;
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
            IMarkovMatrix<float> inputMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrixLoader<float> markovMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(inputMatrix);
            LanguageDetector languageDetector = new LanguageDetector(markovMatrixLoader);
            IMarkovMatrix<float> englishMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();

            // Act
            languageDetector.AddLanguage("English", englishMatrix);

            // Assert
            Assert.Equal(1, languageDetector.Count);
        }

        [Fact]
        public void GivenNoLanguagesAndText_DetectLanguage_ShouldThrow()
        {
            // Arrange
            IMarkovMatrix<float> inputMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrixLoader<float> markovMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(inputMatrix);
            LanguageDetector languageDetector = new LanguageDetector(markovMatrixLoader);

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
            IMarkovMatrix<float> inputMatrix = LanguageDetectorTestHelper.BuildFrenchLanguageMatrixMock();
            IMarkovMatrixLoader<float> markovMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(inputMatrix);
            LanguageDetector languageDetector = new LanguageDetector(markovMatrixLoader);
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
            IMarkovMatrix<float> inputMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrixLoader<float> markovMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(inputMatrix);
            LanguageDetector languageDetector = new LanguageDetector(markovMatrixLoader);
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
