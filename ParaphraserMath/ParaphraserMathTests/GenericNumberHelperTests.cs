using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ParaphraserMath.Tests
{
    public class GenericNumberHelperTests
    {
        [Fact]
        public void GivenInvalidType_Validate_ReturnFalse()
        {
            // Arrange
            bool returnValue;
            bool expectedReturnValue = false;

            // Act
            returnValue = GenericNumberHelper.ValidateNumberType<string>();

            // Assert
            Assert.Equal(expectedReturnValue, returnValue);
        }

        [Fact]
        public void GivenValidType_Validate_ReturnTrue()
        {
            // Arrange
            bool returnValue;
            bool expectedReturnValue = true;

            // Act
            returnValue = GenericNumberHelper.ValidateNumberType<double>();

            // Assert
            Assert.Equal(expectedReturnValue, returnValue);
        }

        [Fact]
        public void GivenNumber_Add_ShouldAdd()
        {
            // Arrange
            double sourceValue = 7;
            int valueToAdd = 4;
            double expectedValue = 11;

            // Act
            double actualValue = GenericNumberHelper.Add<double>(sourceValue, valueToAdd);

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ShouldGetValue()
        {
            // Arrange
            int sourceValue = 7;
            double expectedValue = 7;

            // Act
            double actualValue = GenericNumberHelper.GetValue<double>(sourceValue);

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
