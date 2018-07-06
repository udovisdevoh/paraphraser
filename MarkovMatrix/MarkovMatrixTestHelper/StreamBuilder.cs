using ParaphraserMath;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices.TestHelper
{
    public static class StreamBuilder
    {
        public static MemoryStream BuildTextStream(string text)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static MemoryStream BuildBinaryStream(int count,
            char fromChar1,
            char toChar1,
            double occurrence1,
            char fromChar2,
            char toChar2,
            double occurrence2)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(count);
            ulong combinedChars1 = MatrixMathHelper.CombineChars(fromChar1, toChar1);
            writer.Write(combinedChars1);
            writer.Write(occurrence1);
            ulong combinedChars2 = MatrixMathHelper.CombineChars(fromChar2, toChar2);
            writer.Write(combinedChars2);
            writer.Write(occurrence2);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static MemoryStream BuildBinaryStream(int wordCount,
            string word1,
            uint word1Id,
            string word2,
            uint word2Id,
            string word3,
            uint word3Id,
            int occurrenceCount,
            ulong word1And2,
            double occurrence1,
            ulong word2And3,
            double occurrence2)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(wordCount);
            writer.Write(word1);
            writer.Write(word1Id);
            writer.Write(word2);
            writer.Write(word2Id);
            writer.Write(word3);
            writer.Write(word3Id);
            writer.Write(occurrenceCount);
            writer.Write(word1And2);
            writer.Write(occurrence1);
            writer.Write(word2And3);
            writer.Write(occurrence2);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
