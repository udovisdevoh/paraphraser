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
    public class MemoryStreamBuilderTests
    {
        [Fact]
        public void GivenString_ShouldBuildMemoryStream()
        {
            // Arrange
            string expectedValue = "this is text";
            string actualValue;

            // Act
            MemoryStream memoryStream = MemoryStreamBuilder.BuildMemoryStreamFromText(expectedValue);
            using (StreamReader streamReader = new StreamReader(memoryStream))
            {
                actualValue = streamReader.ReadLine();
            }

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
