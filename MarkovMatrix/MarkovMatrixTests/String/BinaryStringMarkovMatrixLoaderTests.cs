﻿using MarkovMatrices.TestHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarkovMatrices.Tests
{
    public class BinaryStringMarkovMatrixLoaderTests
    {
        [Fact]
        public void GivenBinaryStream_LoadMatrix_ShouldGetRightInputCount()
        {
            // Arrange
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(3, "aa", (uint)0, "bb", (uint)1, "cc", (uint)2, 2, (ulong)1, 0.25, (ulong)2, 0.75);
            BinaryStringMarkovMatrixLoader binaryMarkovMatrixLoader = new BinaryStringMarkovMatrixLoader();
            int expectedInputCount = 2;

            // Act
            IMarkovMatrix<string, double> markovMatrix = binaryMarkovMatrixLoader.LoadMatrix(memoryStream);
            int actualInputCount = markovMatrix.InputCount;

            // Assert
            Assert.Equal(expectedInputCount, actualInputCount);
        }

        [Fact]
        public void GivenBinaryStreamWithWhiteList_LoadMatrix_ShouldGetRightInputCount()
        {
            // Arrange
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(3, "aa", (uint)0, "bb", (uint)1, "cc", (uint)2, 2, (ulong)1, 0.25, (ulong)2, 0.75);
            BinaryStringMarkovMatrixLoader binaryMarkovMatrixLoader = new BinaryStringMarkovMatrixLoader();
            int expectedInputCount = 1;
            HashSet<string> whiteList = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "bb" };

            // Act
            IMarkovMatrix<string, double> markovMatrix = binaryMarkovMatrixLoader.LoadMatrix(memoryStream, whiteList);
            int actualInputCount = markovMatrix.InputCount;

            // Assert
            Assert.Equal(expectedInputCount, actualInputCount);
        }

        [Fact]
        public void GivenBinaryStream_LoadMatrix_ShouldGetRightOccurrenceFirstCharGroup()
        {
            // Arrange
            double expectedOccurrence = 0.25;
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(3, "aa", (uint)0, "bb", (uint)1, "cc", (uint)2, 2, (ulong)1, 0.25, (ulong)4294967297, 0.75);
            BinaryStringMarkovMatrixLoader binaryMarkovMatrixLoader = new BinaryStringMarkovMatrixLoader();

            // Act
            IMarkovMatrix<string, double> markovMatrix = binaryMarkovMatrixLoader.LoadMatrix(memoryStream);
            int actualInputCount = markovMatrix.InputCount;
            double actualOccurence = markovMatrix.GetOccurrence("Aa", "Bb");

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurence);
        }

        [Fact]
        public void GivenBinaryStream_LoadMatrix_ShouldGetRightOccurrenceSecondCharGroup()
        {
            // Arrange
            double expectedOccurrence = 0.75;
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(3, "aa", (uint)0, "bb", (uint)1, "cc", (uint)2, 2, (ulong)1, 0.25, (ulong)4294967297, 0.75);
            BinaryStringMarkovMatrixLoader binaryMarkovMatrixLoader = new BinaryStringMarkovMatrixLoader();

            // Act
            IMarkovMatrix<string, double> markovMatrix = binaryMarkovMatrixLoader.LoadMatrix(memoryStream);
            int actualInputCount = markovMatrix.InputCount;
            double actualOccurence = markovMatrix.GetOccurrence("Bb", "Cc");

            // Assert
            Assert.Equal(expectedOccurrence, actualOccurence);
        }

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

        [Fact]
        public void GivenBinaryStreamAndMaxSize_LoadMatrix_ShouldThrow()
        {
            // Arrange
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(3, "aa", (uint)0, "bb", (uint)1, "cc", (uint)2, 2, (ulong)1, 0.25, (ulong)2, 0.75);
            BinaryStringMarkovMatrixLoader binaryMarkovMatrixLoader = new BinaryStringMarkovMatrixLoader();
            HashSet<string> whiteList = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "bb" };

            // Assert
            Assert.Throws<NotSupportedException>(() =>
            {
                // Act
                IMarkovMatrix<string, double> markovMatrix = binaryMarkovMatrixLoader.LoadMatrix(memoryStream, 27);
            });
        }

        [Fact]
        public void GivenBinaryStreamWhileListAndMaxSize_LoadMatrix_ShouldThrow()
        {
            // Arrange
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(3, "aa", (uint)0, "bb", (uint)1, "cc", (uint)2, 2, (ulong)1, 0.25, (ulong)2, 0.75);
            BinaryStringMarkovMatrixLoader binaryMarkovMatrixLoader = new BinaryStringMarkovMatrixLoader();
            HashSet<string> whiteList = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "bb" };

            // Assert
            Assert.Throws<NotSupportedException>(() =>
            {
                // Act
                IMarkovMatrix<string, double> markovMatrix = binaryMarkovMatrixLoader.LoadMatrix(memoryStream, whiteList, 27);
            });
        }
    }
}
