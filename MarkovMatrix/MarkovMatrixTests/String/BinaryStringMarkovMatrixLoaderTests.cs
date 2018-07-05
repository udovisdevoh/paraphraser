using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarkovMatrices.Tests
{
    public class BinaryStringMarkovMatrixLoaderTests
    {
        [Fact]
        public void GivenText_LoadMatrix_ShouldThrow()
        {
            // Arrange
            string text = "zarf";
            BinaryStringMarkovMatrixLoader binaryStringMarkovMatrixLoader = new BinaryStringMarkovMatrixLoader();

            // Act, Assert
            Assert.Throws<NotSupportedException>(() =>
            {
                IMarkovMatrix<string, double> markovMatrix = binaryStringMarkovMatrixLoader.LoadMatrix(text);
            });
        }
    }
}
