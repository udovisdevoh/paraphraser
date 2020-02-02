using StringManipulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class StringTwoLettersMarkovMatrixLoaderFromText : IMarkovMatrixLoader<string, double>
    {
        public IMarkovMatrix<string, double> LoadMatrix(Stream inputStream, bool isNormalize)
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

            IMarkovMatrix<string, double> convertedMatrix;
            if (isNormalize)
            {
                convertedMatrix = this.Normalize(markovMatrix);
            }
            else
            {
                convertedMatrix = this.Convert(markovMatrix);
            }
            return convertedMatrix;
        }

        private void PopulateMatrixFromLine(StringMarkovMatrix<ulong> markovMatrix, string line)
        {
            string[] twoLetterGroups = this.GetLowerInvariantTwoLetterGroups(line).ToArray();
            if (twoLetterGroups.Length > 0)
            {
                string previousGroup = "  ";
                foreach (string currentGroup in twoLetterGroups)
                {
                    markovMatrix.IncrementOccurrence(previousGroup, currentGroup);
                    previousGroup = currentGroup;
                }

                markovMatrix.IncrementOccurrence(previousGroup, "  ");
            }
        }

        private IEnumerable<string> GetLowerInvariantTwoLetterGroups(string line)
        {
            line = line.ToLowerInvariant();

            char[] letters = line.ToCharArray();

            for (int index = 0; index < letters.Length - 1; ++index)
            {
                char letter1 = letters[index];
                char letter2 = letters[index + 1];

                yield return $"{letter1}{letter2}";
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

        public IMarkovMatrix<string, double> Convert(IMarkovMatrix<string, ulong> sourceMatrix)
        {
            StringMarkovMatrix<double> convertedMatrix = new StringMarkovMatrix<double>();

            foreach (KeyValuePair<Tuple<string, string>, ulong> twoWordsAndCount in sourceMatrix)
            {
                Tuple<string, string> twoWords = twoWordsAndCount.Key;

                string fromWord = twoWords.Item1;
                string toWord = twoWords.Item2;

                ulong count = twoWordsAndCount.Value;


                convertedMatrix.IncrementOccurrence(fromWord, toWord, (double)count);
            }

            return convertedMatrix;
        }

        public IMarkovMatrix<string, double> LoadMatrix(string text, bool isNormalize)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return this.LoadMatrix(stream, isNormalize);
        }
    }
}
