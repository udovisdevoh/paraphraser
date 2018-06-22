using ParaphraserMath;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class BinaryMarkovMatrixSaver<T>
        where T : struct
    {
        public void SaveMatrix(IMarkovMatrix<T> markovMatrix, Stream outputStream)
        {
            BinaryWriter binaryWriter = new BinaryWriter(outputStream);
            binaryWriter.Write(markovMatrix.InputCount);
            foreach (KeyValuePair<Tuple<char, char>, T> twoCharsAndOccurrenceCount in markovMatrix)
            {
                Tuple<char, char> twoChars = twoCharsAndOccurrenceCount.Key;
                T occurrenceCount = twoCharsAndOccurrenceCount.Value;
                uint combinedChars = MatrixMathHelper.CombineChars(twoChars.Item1, twoChars.Item2);
                binaryWriter.Write(combinedChars);
                GenericNumberHelper.WriteValue<T>(binaryWriter, occurrenceCount);
            }
            binaryWriter.Flush();
        }
    }
}
