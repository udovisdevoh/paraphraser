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
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(3, "aa", (ushort)0, "bb", (ushort)1, "cc", (ushort)2, 2, (uint)1, 0.25, (uint)2, 0.75);
            BinaryStringMarkovMatrixLoader binaryMarkovMatrixLoader = new BinaryStringMarkovMatrixLoader();

            // Act
            IMarkovMatrix<string, double> markovMatrix = binaryMarkovMatrixLoader.LoadMatrix(memoryStream);
            int actualInputCount = markovMatrix.InputCount;

            // Assert
            Assert.Equal(2, actualInputCount);
        }

        [Fact]
        public void GivenBinaryStream_LoadMatrix_ShouldGetRightOccurrenceFirstCharGroup()
        {
            // Arrange
            double expectedOccurrence = 0.25;
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(3, "aa", (ushort)0, "bb", (ushort)1, "cc", (ushort)2, 2, (uint)1, 0.25, (uint)65538, 0.75);
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
            MemoryStream memoryStream = StreamBuilder.BuildBinaryStream(3, "aa", (ushort)0, "bb", (ushort)1, "cc", (ushort)2, 2, (uint)1, 0.25, (uint)65538, 0.75);
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
    }
}
