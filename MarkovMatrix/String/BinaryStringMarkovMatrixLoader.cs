using ParaphraserMath;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class BinaryStringMarkovMatrixLoader : IMarkovMatrixLoader<string, double>
    {
        public IMarkovMatrix<string, double> LoadMatrix(Stream inputStream)
        {
            #warning Add unit tests

            StringMarkovMatrix<double> markovMatrix = new StringMarkovMatrix<double>();

            using (BinaryReader binaryReader = new BinaryReader(inputStream))
            {
                int elementCount = binaryReader.ReadInt32();

                for (int elementId = 0; elementId < elementCount; ++elementId)
                {
                    string word = binaryReader.ReadString();
                    ushort wordId = binaryReader.ReadUInt16();

                    markovMatrix.ValueMap.Add(word, wordId);
                }

                for (int elementId = 0; elementId < elementCount; ++elementId)
                {
                    uint combinedStrings = binaryReader.ReadUInt32();
                    Tuple<string, string> twoStrings = markovMatrix.SplitElements(combinedStrings);
                    double occurrenceCount = GenericNumberHelper.ReadValue<double>(binaryReader);
                    markovMatrix.IncrementOccurrence(twoStrings.Item1, twoStrings.Item2, occurrenceCount);
                }
            }

            return markovMatrix;
        }

        public IMarkovMatrix<string, double> LoadMatrix(string text)
        {
            throw new NotSupportedException();
        }
    }
}
