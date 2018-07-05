using StringManipulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class StringMarkovMatrixLoaderFromText : IMarkovMatrixLoader<string, double>
    {
        public IMarkovMatrix<string, double> LoadMatrix(Stream inputStream)
        {
            #warning Todo add unit tests

            StringMarkovMatrix<ulong> markovMatrix = new StringMarkovMatrix<ulong>();
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

            return Normalize(markovMatrix);
        }

        private void PopulateMatrixFromLine(StringMarkovMatrix<ulong> markovMatrix, string line)
        {
            string[] words = WordExtractor.GetLowerInvariantWords(line, '\'');
            if (words.Length > 0)
            {
                string previousWord = " ";
                foreach (string currentWord in words)
                {
                    markovMatrix.IncrementOccurrence(previousWord, currentWord);
                    previousWord = currentWord;
                }
                markovMatrix.IncrementOccurrence(previousWord, " ");
            }
        }

        public StringMarkovMatrix<double> Normalize(StringMarkovMatrix<ulong> sourceMatrix)
        {
            #warning Todo add unit tests

            StringMarkovMatrix<double> normalizedMatrix = new StringMarkovMatrix<double>();

            foreach (KeyValuePair<Tuple<string, string>, ulong> twoWordsAndCount in sourceMatrix)
            {
                Tuple<string, string> twoWords = twoWordsAndCount.Key;

                string fromChar = twoWords.Item1;
                string toChar = twoWords.Item2;

                ulong count = twoWordsAndCount.Value;

                ulong sum = sourceMatrix.GetSum(fromChar);

                if (sum != 0)
                {
                    double ratio = (double)count / (double)sum;

                    normalizedMatrix.IncrementOccurrence(fromChar, toChar, (double)ratio);
                }
            }

            return normalizedMatrix;
        }

        public IMarkovMatrix<string, double> LoadMatrix(string text)
        {
            #warning Todo add unit tests

            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return this.LoadMatrix(stream);
        }
    }
}
