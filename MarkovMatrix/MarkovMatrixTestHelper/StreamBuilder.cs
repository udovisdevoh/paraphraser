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

        public static MemoryStream BuildBinaryStream(int count, char fromChar1, char toChar1, double occurrence1, char fromChar2, char toChar2, double occurrence2)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(count);
            writer.Write(MatrixMathHelper.CombineChars(fromChar1, toChar1));
            writer.Write(occurrence1);
            writer.Write(MatrixMathHelper.CombineChars(fromChar2, toChar2));
            writer.Write(occurrence2);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
