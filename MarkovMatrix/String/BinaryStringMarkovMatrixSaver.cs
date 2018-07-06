using ParaphraserMath;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class BinaryStringMarkovMatrixSaver : IMarkovMatrixSaver<string, double>
    {
        public void SaveMatrix(IMarkovMatrix<string, double> markovMatrix, Stream outputStream)
        {
            BinaryWriter binaryWriter = new BinaryWriter(outputStream);
            binaryWriter.Write(markovMatrix.ValueMap.Count);

            foreach (KeyValuePair<string, uint> wordAndId in markovMatrix.ValueMap)
            {
                string word = wordAndId.Key;
                uint wordId = wordAndId.Value;
                binaryWriter.Write(word);
                binaryWriter.Write(wordId);
            }

            binaryWriter.Write(markovMatrix.InputCount);

            foreach (KeyValuePair<Tuple<string, string>, double> twoWordsAndOccurrenceCount in markovMatrix)
            {
                Tuple<string, string> twoWords = twoWordsAndOccurrenceCount.Key;
                double occurrenceCount = twoWordsAndOccurrenceCount.Value;
                ulong combinedChars = markovMatrix.CombineElements(twoWords.Item1, twoWords.Item2);
                binaryWriter.Write(combinedChars);
                GenericNumberHelper.WriteValue<double>(binaryWriter, occurrenceCount);
            }
            binaryWriter.Flush();
        }
    }
}
