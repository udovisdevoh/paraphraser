using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class TextReaderMarkovMatrixLoader<T> : IMarkovMatrixLoader<T>
        where T : struct
    {
        #region Members
        private Stream inputStream;
        #endregion

        #region Constructors
        public TextReaderMarkovMatrixLoader(Stream inputStream)
        {
            this.inputStream = inputStream;
        }
        #endregion

        public IMarkovMatrix<T> LoadMatrix()
        {
            MarkovMatrix<T> markovMatrix = new MarkovMatrix<T>();
            using (StreamReader streamReader = new StreamReader(this.inputStream))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (!string.IsNullOrEmpty(line))
                    {
                        this.PopulateMatrixFromLine(markovMatrix, line);
                    }
                }
            }

            return markovMatrix;
        }

        private void PopulateMatrixFromLine(MarkovMatrix<T> markovMatrix, string line)
        {
            char[] characters = line.ToCharArray();

            char? previousCharacter = null;
            foreach (char currentCharacter in characters)
            {
                if (previousCharacter != null)
                {
                    markovMatrix.IncrementOccurrence(previousCharacter.Value, currentCharacter);
                }
                previousCharacter = currentCharacter;
            }
        }
    }
}
