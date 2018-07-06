using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class TextMarkovMatrixLoader : IMarkovMatrixLoader<char, ulong>
    {
        public IMarkovMatrix<char, ulong> LoadMatrix(Stream inputStream)
        {
            return this.LoadMatrix(inputStream, null);
        }
        public IMarkovMatrix<char, ulong> LoadMatrix(Stream inputStream, HashSet<char> optionalWhiteList)
        {
            #warning Add unit tests for optionalWhiteList

            CharMarkovMatrix<ulong> markovMatrix = new CharMarkovMatrix<ulong>();
            using (StreamReader streamReader = new StreamReader(inputStream))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (!string.IsNullOrEmpty(line))
                    {
                        this.PopulateMatrixFromLine(markovMatrix, line, optionalWhiteList);
                    }
                }
            }

            return markovMatrix;
        }

        private void PopulateMatrixFromLine(CharMarkovMatrix<ulong> markovMatrix, string line, HashSet<char> optionalWhiteList)
        {
            line = this.PerformLineTransformations(line);

            if (!string.IsNullOrEmpty(line))
            {
                char[] characters = line.ToCharArray();

                char? previousCharacter = ' '; // Starts with space
                foreach (char currentCharacter in characters)
                {
                    if (optionalWhiteList == null || optionalWhiteList.Contains(previousCharacter.Value) || optionalWhiteList.Contains(currentCharacter))
                    {
                        markovMatrix.IncrementOccurrence(previousCharacter.Value, currentCharacter);
                    }
                    previousCharacter = currentCharacter;
                }

                if (optionalWhiteList == null || optionalWhiteList.Contains(previousCharacter.Value) || optionalWhiteList.Contains(' '))
                {
                    markovMatrix.IncrementOccurrence(previousCharacter.Value, ' '); // Ends with space
                }
            }
        }

        public virtual string PerformLineTransformations(string line)
        {
            return line;
        }

        public IMarkovMatrix<char, ulong> LoadMatrix(string text)
        {
            return this.LoadMatrix(text, null);
        }

        public IMarkovMatrix<char, ulong> LoadMatrix(string text, HashSet<char> optionalWhiteList)
        {
            #warning Add unit tests for optionalWhiteList

            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;

            return this.LoadMatrix(stream, optionalWhiteList);
        }
    }
}
