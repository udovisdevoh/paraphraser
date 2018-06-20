﻿using ParaphraserMath;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class BinaryMarkovMatrixLoader<T> : IMarkovMatrixLoader<T>
        where T : struct
    {
        public IMarkovMatrix<T> LoadMatrix(Stream inputStream)
        {
            MarkovMatrix<T> markovMatrix = new MarkovMatrix<T>();

            using (BinaryReader binaryReader = new BinaryReader(inputStream))
            {
                int elementCount = binaryReader.ReadInt32();

                for (int elementId = 0; elementId < elementCount; ++elementId)
                {
                    uint combinedChars = binaryReader.ReadUInt32();
                    Tuple<char, char> twoChars = MatrixMathHelper.SplitChars(combinedChars);
                    T occurrenceCount = GenericNumberHelper.ReadValue<T>(binaryReader);
                    markovMatrix.IncrementOccurrence(twoChars.Item1, twoChars.Item2, occurrenceCount);
                }
            }

            return markovMatrix;
        }
    }
}