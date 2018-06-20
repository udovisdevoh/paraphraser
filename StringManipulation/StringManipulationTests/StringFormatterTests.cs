using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StringManipulation.Tests
{
    public class StringFormatterTests
    {
        [Fact]
        public void GivenString_FormatLanguageName_GetFormattedName()
        {
            // Arrange
            string unformattedName = "   la Langue\n\r\t de Shake'n'Bake";
            string expectedFormattedName = "La langue de shake'n'bake";
            string actualFormattedName;

            // Act
            actualFormattedName = StringFormatter.FormatLanguageName(unformattedName);

            // Assert
            Assert.Equal(expectedFormattedName, actualFormattedName);
        }
    }
}
