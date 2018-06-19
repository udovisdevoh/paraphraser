using ParaphraserMath;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class BinaryMarkovMatrixLoaderSaver<T> : IMarkovMatrixLoader<T>
        where T : struct
    {
        #region Members
        private Stream inputStream;
        #endregion

        #region Constructors
        public BinaryMarkovMatrixLoaderSaver(Stream inputStream)
        {
            this.inputStream = inputStream;
        }
        #endregion

        public IMarkovMatrix<T> LoadMatrix()
        {
            #warning Add unit tests

            MarkovMatrix<T> markovMatrix = new MarkovMatrix<T>();

            using (BinaryReader binaryReader = new BinaryReader(this.inputStream))
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

        public void SaveMatrix(IMarkovMatrix<T> markovMatrix, Stream outputStream)
        {
            #warning Add unit tests

            using (BinaryWriter binaryWriter = new BinaryWriter(outputStream))
            {
                binaryWriter.Write(markovMatrix.InputCount);

                foreach (KeyValuePair<Tuple<char, char>, T> twoCharsAndOccurrenceCount in markovMatrix)
                {
                    Tuple<char, char> twoChars = twoCharsAndOccurrenceCount.Key;
                    T occurrenceCount = twoCharsAndOccurrenceCount.Value;
                    uint combinedChars = MatrixMathHelper.CombineChars(twoChars.Item1, twoChars.Item2);
                    GenericNumberHelper.WriteValue<T>(binaryWriter, occurrenceCount);
                }
            }
        }
    }
}
