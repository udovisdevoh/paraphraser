using LanguageDetection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LanguageDetectionTests
{
    public class WordListReaderTests
    {
        [Fact]
        public void GivenString_BuildWordCountProbability_ShouldGetProbability()
        {
            // Arrange
            string text = "zarf zorf zarf";
            MemoryStream stream = MemoryStreamBuilder.BuildMemoryStreamFromText(text);
            Dictionary<string, double> expectedWordCountValues = new Dictionary<string, double>() { { "zarf", 1.0 }, { "zorf", 0.5 } };

            // Act
            Dictionary<string, double> actualWordCountValues = WordListReader.BuildWordCountProbability(stream);

            // Assert
            Assert.Equal(expectedWordCountValues.OrderBy(keyValuePair => keyValuePair.Value), actualWordCountValues.OrderBy(keyValuePair => keyValuePair.Value));
        }

        [Fact]
        public void GivenString_BuildWordCountList_ShouldGetWordCount()
        {
            // Arrange
            string text = "zarf zorf zarf";
            MemoryStream stream = MemoryStreamBuilder.BuildMemoryStreamFromText(text);
            Dictionary<string, int> expectedWordCountValues = new Dictionary<string, int>() { { "zarf", 2 }, { "zorf", 1 } };

            // Act
            Dictionary<string, int> actualWordCountValues = WordListReader.GetWordCount(stream);

            // Assert
            Assert.Equal(expectedWordCountValues.OrderBy(keyValuePair => keyValuePair.Value), actualWordCountValues.OrderBy(keyValuePair => keyValuePair.Value));
        }

        [Fact]
        public void GivenDictionary_Normalize_ShouldGetWNormalizedToOne()
        {
            // Arrange
            Dictionary<string, int> values = new Dictionary<string, int>() { { "zarf", 10 }, { "zorf", 5 } };
            Dictionary<string, double> expectedNormalizedValues = new Dictionary<string, double>() { { "zarf", 1 }, { "zorf", 0.5 } };

            // Act
            Dictionary<string, double> actualNormalizedValues = WordListReader.NormalizeToMax(values);

            // Assert
            Assert.Equal(expectedNormalizedValues.OrderBy(keyValuePair => keyValuePair.Value), actualNormalizedValues.OrderBy(keyValuePair => keyValuePair.Value));
        }
    }
}
