using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class TextMarkovMatrixLoader<T> : IMarkovMatrixLoader<T>
        where T : struct
    {
        public IMarkovMatrix<T> LoadMatrix(Stream inputStream)
        {
            MarkovMatrix<T> markovMatrix = new MarkovMatrix<T>();
            using (StreamReader streamReader = new StreamReader(inputStream))
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
            line = this.PerformLineTransformations(line);

            if (!string.IsNullOrEmpty(line))
            {
                char[] characters = line.ToCharArray();

                char? previousCharacter = ' '; // Starts with space
                foreach (char currentCharacter in characters)
                {
                    markovMatrix.IncrementOccurrence(previousCharacter.Value, currentCharacter);
                    previousCharacter = currentCharacter;
                }

                markovMatrix.IncrementOccurrence(previousCharacter.Value, ' '); // Ends with space
            }
        }

        public virtual string PerformLineTransformations(string line)
        {
            return line;
        }
    }
}
