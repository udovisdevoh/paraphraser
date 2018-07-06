using ParaphraserMath;
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
            return this.LoadMatrix(inputStream, null);
        }

        public IMarkovMatrix<char, double> LoadMatrix(Stream inputStream, HashSet<char> optionalWhiteList)
        {
            #warning Add unit tests for optionalWhiteList

            CharMarkovMatrix<double> markovMatrix = new CharMarkovMatrix<double>();

            using (BinaryReader binaryReader = new BinaryReader(inputStream))
            {
                int elementCount = binaryReader.ReadInt32();

                for (int elementId = 0; elementId < elementCount; ++elementId)
                {
                    ulong combinedChars = binaryReader.ReadUInt64();
                    Tuple<char, char> twoChars = MatrixMathHelper.SplitChars(combinedChars);
                    double occurrenceCount = GenericNumberHelper.ReadValue<double>(binaryReader);

                    if (optionalWhiteList is null || optionalWhiteList.Contains(twoChars.Item1) || optionalWhiteList.Contains(twoChars.Item2))
                    {
                        markovMatrix.IncrementOccurrence(twoChars.Item1, twoChars.Item2, occurrenceCount);
                    }
                }
            }

            return markovMatrix;
        }

        public IMarkovMatrix<char, double> LoadMatrix(string text)
        {
            return this.LoadMatrix(text, null);
        }

        public IMarkovMatrix<char, double> LoadMatrix(string text, HashSet<char> optionalWhiteList)
        {
            throw new NotSupportedException();
        }
    }
}
