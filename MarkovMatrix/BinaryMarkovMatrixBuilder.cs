using ParaphraserMath;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class BinaryMarkovMatrixBuilder<T> : IMarkovMatrixBuilder<T>
    {
        #region Members
        private Stream inputStream;
        #endregion

        #region Constructors
        public BinaryMarkovMatrixBuilder(Stream inputStream)
        {
            this.inputStream = inputStream;
        }
        #endregion

        public IMarkovMatrix<T> BuildMatrix()
        {
            #warning Add unit tests

            MarkovMatrix<T> markovMatrix = new MarkovMatrix<T>();

            using (BinaryReader binaryReader = new BinaryReader(this.inputStream))
            {
                ulong elementCount = binaryReader.ReadUInt64();

                for (ulong elementId = 0; elementId < elementCount; ++elementId)
                {
                    uint combinedChars = binaryReader.ReadUInt32();
                    Tuple<char, char> twoChars = MatrixMathHelper.SplitChars(combinedChars);
                    T occurrenceCount = GenericNumberHelper.ReadValue<T>(binaryReader);
                    markovMatrix.IncrementOccurrence(twoChars.Item1, twoChars.Item2, occurrenceCount);
                }
            }

            return markovMatrix;
        }

        public void SaveMatrix(IMarkovMatrix<T> markovMatrix, Stream outputStream)
        {
            #warning Implement
            #warning Add unit tests
            throw new NotImplementedException();
        }
    }
}
