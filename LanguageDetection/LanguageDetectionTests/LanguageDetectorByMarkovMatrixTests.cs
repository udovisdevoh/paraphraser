﻿using LanguageDetection;
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
    public class LanguageDetectorByMarkovMatrixTests
    {
        [Fact]
        public void GivenLanguageDetector_AddLanguage()
        {
            // Arrange
            IMarkovMatrix<char, double> inputMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrixLoader<char, double> markovMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(inputMatrix);
            LanguageDetectorByMarkovMatrix languageDetector = new LanguageDetectorByMarkovMatrix(markovMatrixLoader);
            IMarkovMatrix<char, double> englishMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();

            // Act
            languageDetector.AddLanguage("English", englishMatrix);

            // Assert
            Assert.Equal(1, languageDetector.Count);
        }

        [Fact]
        public void GivenNoLanguagesAndText_DetectLanguage_ShouldThrow()
        {
            // Arrange
            IMarkovMatrix<char, double> inputMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrixLoader<char, double> markovMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(inputMatrix);
            LanguageDetectorByMarkovMatrix languageDetector = new LanguageDetectorByMarkovMatrix(markovMatrixLoader);

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
            IMarkovMatrix<char, double> inputMatrix = LanguageDetectorTestHelper.BuildFrenchLanguageMatrixMock();
            IMarkovMatrixLoader<char, double> markovMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(inputMatrix);
            LanguageDetectorByMarkovMatrix languageDetector = new LanguageDetectorByMarkovMatrix(markovMatrixLoader);
            IMarkovMatrix<char, double> englishMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrix<char, double> frenchMatrix = LanguageDetectorTestHelper.BuildFrenchLanguageMatrixMock();

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
            IMarkovMatrix<char, double> inputMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrixLoader<char, double> markovMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(inputMatrix);
            LanguageDetectorByMarkovMatrix languageDetector = new LanguageDetectorByMarkovMatrix(markovMatrixLoader);
            IMarkovMatrix<char, double> englishMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrix<char, double> frenchMatrix = LanguageDetectorTestHelper.BuildFrenchLanguageMatrixMock();

            // Act
            languageDetector.AddLanguage("English", englishMatrix);
            languageDetector.AddLanguage("French", frenchMatrix);

            // Assert
            Assert.Equal("English", languageDetector.DetectLanguage("that is"));
        }

        [Fact]
        public void GivenTwoLanguagesAndText_GetDetectLanguageProximities_ShouldGetTwoLanguages()
        {
            // Arrange
            IMarkovMatrix<char, double> inputMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrixLoader<char, double> markovMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(inputMatrix);
            LanguageDetectorByMarkovMatrix languageDetector = new LanguageDetectorByMarkovMatrix(markovMatrixLoader);
            IMarkovMatrix<char, double> englishMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrix<char, double> frenchMatrix = LanguageDetectorTestHelper.BuildFrenchLanguageMatrixMock();

            // Act
            languageDetector.AddLanguage("English", englishMatrix);
            languageDetector.AddLanguage("French", frenchMatrix);
            KeyValuePair<string, double>[] languageProximities = languageDetector.GetLanguageProximities("that is");

            // Assert
            Assert.Equal(2, languageProximities.Length);
        }

        [Fact]
        public void GivenTwoLanguagesAndText_GetDetectLanguageProximities_ShouldGetEnglishFirst()
        {
            // Arrange
            IMarkovMatrix<char, double> inputMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrixLoader<char, double> markovMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(inputMatrix);
            LanguageDetectorByMarkovMatrix languageDetector = new LanguageDetectorByMarkovMatrix(markovMatrixLoader);
            IMarkovMatrix<char, double> englishMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrix<char, double> frenchMatrix = LanguageDetectorTestHelper.BuildFrenchLanguageMatrixMock();

            // Act
            languageDetector.AddLanguage("English", englishMatrix);
            languageDetector.AddLanguage("French", frenchMatrix);
            KeyValuePair<string, double>[] languageProximities = languageDetector.GetLanguageProximities("that is");

            // Assert
            Assert.Equal("English", languageProximities[0].Key);
        }

        [Fact]
        public void GivenLanguageDetector_ShouldGetLanguageList()
        {
            // Arrange
            IMarkovMatrix<char, double> englishMatrix = LanguageDetectorTestHelper.BuildEnglishLanguageMatrixMock();
            IMarkovMatrix<char, double> frenchMatrix = LanguageDetectorTestHelper.BuildFrenchLanguageMatrixMock();
            IMarkovMatrixLoader<char, double> markovMatrixLoader = LanguageDetectorTestHelper.BuildNormalizedTextMarkovMatrixLoader(englishMatrix);
            LanguageDetectorByMarkovMatrix languageDetector = new LanguageDetectorByMarkovMatrix(markovMatrixLoader);
            IEnumerable<string> expectedLanguageList = new string[] { "English", "French" };

            // Act
            languageDetector.AddLanguage("English", englishMatrix);
            languageDetector.AddLanguage("French", frenchMatrix);
            IEnumerable<string> actualLanguageList = languageDetector.GetLanguageList();

            // Assert
            Assert.Equal(expectedLanguageList.OrderBy(value => value), actualLanguageList.OrderBy(value => value));
        }
    }
}
