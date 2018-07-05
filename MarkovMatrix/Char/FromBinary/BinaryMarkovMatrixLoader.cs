﻿using ParaphraserMath;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class BinaryMarkovMatrixLoader : IMarkovMatrixLoader<char, double>
    {
        public IMarkovMatrix<char, double> LoadMatrix(Stream inputStream)
        {
            CharMarkovMatrix<double> markovMatrix = new CharMarkovMatrix<double>();

            using (BinaryReader binaryReader = new BinaryReader(inputStream))
            {
                int elementCount = binaryReader.ReadInt32();

                for (int elementId = 0; elementId < elementCount; ++elementId)
                {
                    uint combinedChars = binaryReader.ReadUInt32();
                    Tuple<char, char> twoChars = MatrixMathHelper.SplitChars(combinedChars);
                    double occurrenceCount = GenericNumberHelper.ReadValue<double>(binaryReader);
                    markovMatrix.IncrementOccurrence(twoChars.Item1, twoChars.Item2, occurrenceCount);
                }
            }

            return markovMatrix;
        }

        public IMarkovMatrix<char, double> LoadMatrix(string text)
        {
            throw new NotSupportedException();
        }
    }
}