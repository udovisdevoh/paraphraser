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
            IMarkovMatrix<double> inputMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrixLoader<double> markovMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(inputMatrix);
            LanguageDetector languageDetector = new LanguageDetector(markovMatrixLoader);
            IMarkovMatrix<double> englishMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();

            // Act
            languageDetector.AddLanguage("English", englishMatrix);

            // Assert
            Assert.Equal(1, languageDetector.Count);
        }

        [Fact]
        public void GivenNoLanguagesAndText_DetectLanguage_ShouldThrow()
        {
            // Arrange
            IMarkovMatrix<double> inputMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrixLoader<double> markovMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(inputMatrix);
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
            IMarkovMatrix<double> inputMatrix = LanguageDetectorTestHelper.BuildFrenchLanguageMatrixMock();
            IMarkovMatrixLoader<double> markovMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(inputMatrix);
            LanguageDetector languageDetector = new LanguageDetector(markovMatrixLoader);
            IMarkovMatrix<double> englishMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrix<double> frenchMatrix = LanguageDetectorTestHelper.BuildFrenchLanguageMatrixMock();

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
            IMarkovMatrix<double> inputMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrixLoader<double> markovMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(inputMatrix);
            LanguageDetector languageDetector = new LanguageDetector(markovMatrixLoader);
            IMarkovMatrix<double> englishMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrix<double> frenchMatrix = LanguageDetectorTestHelper.BuildFrenchLanguageMatrixMock();

            // Act
            languageDetector.AddLanguage("English", englishMatrix);
            languageDetector.AddLanguage("French", frenchMatrix);

            // Assert
            Assert.Equal("English", languageDetector.DetectLanguage("that is"));
        }
    }
}
