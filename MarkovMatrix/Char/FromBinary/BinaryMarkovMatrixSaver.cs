using ParaphraserMath;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class BinaryMarkovMatrixSaver : IMarkovMatrixSaver<char, double>
    {
        public void SaveMatrix(IMarkovMatrix<char, double> markovMatrix, Stream outputStream)
        {
            BinaryWriter binaryWriter = new BinaryWriter(outputStream);
            binaryWriter.Write(markovMatrix.InputCount);
            foreach (KeyValuePair<Tuple<char, char>, double> twoCharsAndOccurrenceCount in markovMatrix)
            {
                Tuple<char, char> twoChars = twoCharsAndOccurrenceCount.Key;
                double occurrenceCount = twoCharsAndOccurrenceCount.Value;
                ulong combinedChars = MatrixMathHelper.CombineChars(twoChars.Item1, twoChars.Item2);
                binaryWriter.Write(combinedChars);
                GenericNumberHelper.WriteValue<double>(binaryWriter, occurrenceCount);
            }
            binaryWriter.Flush();
        }
    }
}
