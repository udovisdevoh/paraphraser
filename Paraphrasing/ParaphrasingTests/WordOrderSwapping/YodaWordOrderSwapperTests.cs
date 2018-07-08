using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paraphrasing.Tests
{
    public class YodaWordOrderSwapperTests
    {
        [Fact]
        public void GivenText_SwapWordOrder_ShouldSwap()
        {
            // Arrange
            string text = "Would this guy stay there";
            HashSet<string> wordsToSwap = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "would" };
            YodaWordOrderSwapper yodaWordOrderSwapper = new YodaWordOrderSwapper();
            string expectedSwappedText = "guy stay there, this would";

            // Act
            string actualSwappedText = yodaWordOrderSwapper.SwapQuestionWord(text, wordsToSwap, null, 1);

            // Assert
            Assert.Equal(expectedSwappedText.ToLowerInvariant(), actualSwappedText.ToLowerInvariant());
        }

        [Fact]
        public void GivenTextAndWordsToSkip_SwapWordOrder_ShouldSwap()
        {
            // Arrange
            string text = "Would this guy stay there";
            HashSet<string> wordsToSwap = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "would" };
            HashSet<string> wordsToSkip = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "this" };
            YodaWordOrderSwapper yodaWordOrderSwapper = new YodaWordOrderSwapper();
            string expectedSwappedText = "stay there, this guy would";

            // Act
            string actualSwappedText = yodaWordOrderSwapper.SwapQuestionWord(text, wordsToSwap, wordsToSkip, 1);

            // Assert
            Assert.Equal(expectedSwappedText.ToLowerInvariant(), actualSwappedText.ToLowerInvariant());
        }
    }
}
