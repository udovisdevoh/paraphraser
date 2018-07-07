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
            return this.LoadMatrix(inputStream, null);
        }

        public IMarkovMatrix<string, double> LoadMatrix(Stream inputStream, int maxSize)
        {
            return this.LoadMatrix(inputStream, null, maxSize);
        }

        public IMarkovMatrix<string, double> LoadMatrix(Stream inputStream, HashSet<string> optionalWhiteList)
        {
            return this.LoadMatrix(inputStream, optionalWhiteList, -1);
        }

        public IMarkovMatrix<string, double> LoadMatrix(Stream inputStream, HashSet<string> optionalWhiteList, int maxSize)
        {
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

            markovMatrix = this.TrimMatrix(markovMatrix, optionalWhiteList, maxSize);

            IMarkovMatrix<string, double> normalizedMatrix = this.Normalize(markovMatrix);
            return normalizedMatrix;
        }

        public StringMarkovMatrix<ulong> TrimMatrix(StringMarkovMatrix<ulong> sourceMatrix, HashSet<string> optionalWhiteList, int maxSize)
        {
            if (maxSize == -1)
            {
                return sourceMatrix;
            }

            IEnumerable<KeyValuePair<Tuple<string, string>, ulong>> twoWordsAndCounts = sourceMatrix.OrderByDescending(keyValuePair => keyValuePair.Value);

            StringMarkovMatrix<ulong> trimmedMatrix = new StringMarkovMatrix<ulong>();

            int counter = 0;
            foreach (KeyValuePair<Tuple<string, string>, ulong> twoWordsAndCount in sourceMatrix)
            {
                Tuple<string, string> twoWords = twoWordsAndCount.Key;

                string fromWord = twoWords.Item1;
                string toWord = twoWords.Item2;

                ulong count = twoWordsAndCount.Value;

                if (counter < maxSize || (optionalWhiteList != null && (optionalWhiteList.Contains(fromWord) || optionalWhiteList.Contains(toWord))))
                {
                    trimmedMatrix.IncrementOccurrence(fromWord, toWord, count);
                    ++counter;
                }
            }

            return trimmedMatrix;
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

        public IMarkovMatrix<string, double> Normalize(IMarkovMatrix<string, ulong> sourceMatrix)
        {
            StringMarkovMatrix<double> normalizedMatrix = new StringMarkovMatrix<double>();

            foreach (KeyValuePair<Tuple<string, string>, ulong> twoWordsAndCount in sourceMatrix)
            {
                Tuple<string, string> twoWords = twoWordsAndCount.Key;

                string fromWord = twoWords.Item1;
                string toWord = twoWords.Item2;

                ulong count = twoWordsAndCount.Value;

                ulong sum = sourceMatrix.GetSum(fromWord);

                if (sum != 0)
                {
                    double ratio = (double)count / (double)sum;

                    normalizedMatrix.IncrementOccurrence(fromWord, toWord, (double)ratio);
                }
            }

            return normalizedMatrix;
        }

        public IMarkovMatrix<string, double> LoadMatrix(string text)
        {
            return this.LoadMatrix(text, null);
        }

        public IMarkovMatrix<string, double> LoadMatrix(string text, HashSet<string> optionalWhiteList)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return this.LoadMatrix(stream, optionalWhiteList);
        }
    }
}
