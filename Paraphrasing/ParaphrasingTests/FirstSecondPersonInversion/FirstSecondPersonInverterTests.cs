using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paraphrasing.Tests
{
    public class FirstSecondPersonInverterTests
    {
        #warning Todo Add unit tests

        [Theory]
        [InlineData("I say this is great.", "You say this is great.")]
        [InlineData("Do I say this is great.", "Do you say this is great.")]
        [InlineData("Steve and I are going.", "Steve and you are going.")]
        public void GivenSentenceWithI_ShouldConvertIToYou(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }

        [Theory]
        [InlineData("", "")]
        public void GivenSentenceWithIam_ShouldConvertIToYouAre(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }

        [Theory]
        [InlineData("", "")]
        public void GivenSentenceWithIm_ShouldConvertIToYouAre(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }

        [Theory]
        [InlineData("", "")]
        public void GivenSentenceWithIWas_ShouldConvertIToYouWere(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
    }
}
