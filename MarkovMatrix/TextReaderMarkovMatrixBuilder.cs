using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class TextReaderMarkovMatrixBuilder<T> : IMarkovMatrixBuilder<T>
    {
        #region Members
        private Stream stream;
        #endregion

        #region Constructors
        public TextReaderMarkovMatrixBuilder(Stream stream)
        {
            this.stream = stream;
        }
        #endregion

        public IMarkovMatrix<T> BuildMatrix()
        {
            MarkovMatrix<T> markovMatrix = new MarkovMatrix<T>();
            using (StreamReader streamReader = new StreamReader(this.stream))
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
                    markovMatrix.IncrementOccurence(previousCharacter.Value, currentCharacter);
                }
                previousCharacter = currentCharacter;
            }
        }
    }
}
