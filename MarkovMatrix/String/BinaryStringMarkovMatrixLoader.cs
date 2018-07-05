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
            StringMarkovMatrix<double> markovMatrix = new StringMarkovMatrix<double>();

            using (BinaryReader binaryReader = new BinaryReader(inputStream))
            {
                int tokenCount = binaryReader.ReadInt32();

                for (int tokenId = 0; tokenId < tokenCount; ++tokenId)
                {
                    string word = binaryReader.ReadString();
                    ushort wordId = binaryReader.ReadUInt16();

                    markovMatrix.ValueMap.Add(word, wordId);
                    markovMatrix.ReverseValueMap.Add(wordId, word);
                }

                int occurrenceCount = binaryReader.ReadInt32();

                for (int occurrenceId = 0; occurrenceId < occurrenceCount; ++occurrenceId)
                {
                    uint combinedStrings = binaryReader.ReadUInt32();
                    Tuple<string, string> twoStrings = markovMatrix.SplitElements(combinedStrings);
                    double occurrence = GenericNumberHelper.ReadValue<double>(binaryReader);
                    markovMatrix.IncrementOccurrence(twoStrings.Item1, twoStrings.Item2, occurrence);
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
