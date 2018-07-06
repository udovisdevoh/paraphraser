using MarkovMatrices;
using ParaphaserBootstrap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordMatrixConstructionApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Bootstrap bootstrap = new Bootstrap();

            IMarkovMatrixLoader<string, double> stringMarkovMatrixLoaderFromText = bootstrap.BuildStringMarkovMatrixLoaderFromText();
            IMarkovMatrixSaver<string, double> binaryStringMarkovMatrixSaver = bootstrap.BuildBinaryStringMarkovMatrixSaver();

            const string inputFile = "./LanguageSamples/lyrics.en.txt";
            const string outputFile = "./english.word.matrix.bin";

            HashSet<string> whiteListedWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "ain't", "aint", "am", "are",
                "aren't", "can", "can't", "canst", "cant", "could", "couldn't", "did", "didn't", "didst",
                "do", "does", "doesn't", "doest", "d'ya", "d'you", "got", "had", "has", "have", "haven't",
                "how", "how'd", "how's", "how'm", "is", "isn't", "may", "must", "shall", "should", "shouldn't",
                "wanna", "want", "was", "wasn't", "were", "wha'", "wha'cha", "weren't", "wha's", "what",
                "whatcha", "what'll", "what're", "whats", "when", "when's", "where", "where'd", "where're",
                "where've", "which", "who", "who'd", "who'll", "whom", "who's", "whose", "whut", "why", "will",
                "won't", "wont", "would", "wouldn't", "what's"
            };

            using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open))
            {
                using (FileStream outputFileStream = new FileStream(outputFile, FileMode.Create))
                {
                    IMarkovMatrix<string, double> matrix = stringMarkovMatrixLoaderFromText.LoadMatrix(inputFileStream, whiteListedWords);
                    binaryStringMarkovMatrixSaver.SaveMatrix(matrix, outputFileStream);
                }
            }
        }
    }
}
