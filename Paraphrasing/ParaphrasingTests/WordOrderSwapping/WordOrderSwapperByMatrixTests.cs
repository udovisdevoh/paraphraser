using MarkovMatrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paraphrasing.Tests
{
    public class WordOrderSwapperByMatrixTests
    {
        [Fact]
        public void GivenWordSwapperAndMatrix_ShouldPerformLongSwaps()
        {
            // Arrange
            string sourceText = "This red is a apple.";
            string expectedSwappedText = "This is a red apple.";
            IMarkovMatrix<string, double> languageWordMatrix = ParaphrasingTestHelper.BuildLanguageWordMatrixMock("red", "apple", 0.75, "red", "is", 0.25, "this", "red", 0.25, "a", "red", 0.26);
            IMarkovMatrixLoader<string, double> textMatrixLoader = ParaphrasingTestHelper.BuildStringMarkovMatrixLoader();
            IWordOrderSwapper wordOrderSwapper = new WordOrderSwapperByMatrix(languageWordMatrix, textMatrixLoader);
            HashSet<string> wordsToSwap = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "red" };

            // Act
            string actualSwappedText = wordOrderSwapper.SwapWordOrder(sourceText, wordsToSwap, null, null, 1);

            // Assert
            Assert.Equal(expectedSwappedText.ToLowerInvariant(), actualSwappedText.ToLowerInvariant());
        }

        [Fact]
        public void GivenWordSwapperWordsToSkipAndMatrix_ShouldPerformLongSwaps()
        {
            // Arrange
            string sourceText = "This red is a apple.";
            string expectedSwappedText = "This is red a apple.";
            IMarkovMatrix<string, double> languageWordMatrix = ParaphrasingTestHelper.BuildLanguageWordMatrixMock("red", "apple", 0.75, "red", "is", 0.25, "this", "red", 0.25, "a", "red", 0.26);
            IMarkovMatrixLoader<string, double> textMatrixLoader = ParaphrasingTestHelper.BuildStringMarkovMatrixLoader();
            IWordOrderSwapper wordOrderSwapper = new WordOrderSwapperByMatrix(languageWordMatrix, textMatrixLoader);
            HashSet<string> wordsToSwap = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "red" };
            HashSet<string> wordsToSkipWhileSwapping = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "a" };

            // Act
            string actualSwappedText = wordOrderSwapper.SwapWordOrder(sourceText, wordsToSwap, wordsToSkipWhileSwapping, null, 1);

            // Assert
            Assert.Equal(expectedSwappedText.ToLowerInvariant(), actualSwappedText.ToLowerInvariant());
        }
    }
}
