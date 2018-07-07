using MarkovMatrices.TestHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarkovMatrices.Tests
{
    public class TextMarkovMatrixLoaderTests
    {
        [Fact]
        public void GivenStream_LoadMatrix()
        {
            // Arrange
            Stream stream = StreamBuilder.BuildTextStream("Pseudolachnostylis is a genus of plants in the Phyllanthaceae first described as a genus in 1899");
            TextMarkovMatrixLoader textMarkovMatrixLoader = new TextMarkovMatrixLoader();
            int expectedInputCount = 69;

            // Act
            IMarkovMatrix<char, ulong> markovMatrix = textMarkovMatrixLoader.LoadMatrix(stream);

            // Assert
            Assert.Equal(expectedInputCount, markovMatrix.InputCount);
        }

        [Fact]
        public void GivenStreamAndWhiteList_LoadMatrix_ShouldFilter()
        {
            // Arrange
            Stream stream = StreamBuilder.BuildTextStream("Pseudolachnostylis is a genus of plants in the Phyllanthaceae first described as a genus in 1899");
            TextMarkovMatrixLoader textMarkovMatrixLoader = new TextMarkovMatrixLoader();
            HashSet<char> ignoreList = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };
            int expectedInputCount = 37;

            // Act
            IMarkovMatrix<char, ulong> markovMatrix = textMarkovMatrixLoader.LoadMatrix(stream, ignoreList);

            // Assert
            Assert.Equal(expectedInputCount, markovMatrix.InputCount);
        }

        [Fact]
        public void GivenText_LoadMatrix()
        {
            // Arrange
            string text = "Pseudolachnostylis is a genus of plants in the Phyllanthaceae first described as a genus in 1899";
            TextMarkovMatrixLoader textMarkovMatrixLoader = new TextMarkovMatrixLoader();
            int expectedInputCount = 69;

            // Act
            IMarkovMatrix<char, ulong> markovMatrix = textMarkovMatrixLoader.LoadMatrix(text);

            // Assert
            Assert.Equal(expectedInputCount, markovMatrix.InputCount);
        }

        [Fact]
        public void GivenTextAndWhiteList_LoadMatrix_ShouldFilter()
        {
            // Arrange
            string text = "Pseudolachnostylis is a genus of plants in the Phyllanthaceae first described as a genus in 1899";
            TextMarkovMatrixLoader textMarkovMatrixLoader = new TextMarkovMatrixLoader();
            HashSet<char> ignoreList = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };
            int expectedInputCount = 37;

            // Act
            IMarkovMatrix<char, ulong> markovMatrix = textMarkovMatrixLoader.LoadMatrix(text, ignoreList);

            // Assert
            Assert.Equal(expectedInputCount, markovMatrix.InputCount);
        }

        [Fact]
        public void GivenStream_LoadMatrix_ShouldHaveCorrectOccurrence()
        {
            // Arrange
            Stream stream = StreamBuilder.BuildTextStream("Pseudolachnostylis is a genus of plants in the Phyllanthaceae first described as a genus in 1899");
            TextMarkovMatrixLoader textMarkovMatrixLoader = new TextMarkovMatrixLoader();
            ulong expectedOccurrence = 3;

            // Act
            IMarkovMatrix<char, ulong> markovMatrix = textMarkovMatrixLoader.LoadMatrix(stream);
            ulong actualOccurrence = markovMatrix.GetOccurrence('l', 'a');

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurrence);
        }

        [Fact]
        public void GivenStreamAndMaxSize_LoadMatrix_ShouldThrow()
        {
            // Arrange
            Stream stream = StreamBuilder.BuildTextStream("Pseudolachnostylis is a genus of plants in the Phyllanthaceae first described as a genus in 1899");
            TextMarkovMatrixLoader textMarkovMatrixLoader = new TextMarkovMatrixLoader();

            // Assert
            Assert.Throws<NotSupportedException>(() =>
            {
                // Act
                IMarkovMatrix<char, ulong> markovMatrix = textMarkovMatrixLoader.LoadMatrix(stream, 27);
            });
        }

        [Fact]
        public void GivenStreamWhiteListAndMaxSize_LoadMatrix_ShouldThrow()
        {
            // Arrange
            Stream stream = StreamBuilder.BuildTextStream("Pseudolachnostylis is a genus of plants in the Phyllanthaceae first described as a genus in 1899");
            TextMarkovMatrixLoader textMarkovMatrixLoader = new TextMarkovMatrixLoader();
            HashSet<char> whiteList = new HashSet<char>() { 'A' };

            // Assert
            Assert.Throws<NotSupportedException>(() =>
            {
                // Act
                IMarkovMatrix<char, ulong> markovMatrix = textMarkovMatrixLoader.LoadMatrix(stream, whiteList, 27);
            });
        }
    }
}