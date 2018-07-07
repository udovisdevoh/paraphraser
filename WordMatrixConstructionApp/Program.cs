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

            //Program.TestLoadPerformance(bootstrap);
            Program.GenerateWordMatrix(bootstrap);
        }

        private static void GenerateWordMatrix(Bootstrap bootstrap)
        {
            IMarkovMatrixLoader<string, double> stringMarkovMatrixLoaderFromText = bootstrap.BuildStringMarkovMatrixLoaderFromText();
            IMarkovMatrixSaver<string, double> binaryStringMarkovMatrixSaver = bootstrap.BuildBinaryStringMarkovMatrixSaver();

            //const int maxMatrixSize = 100_000;
            //const int maxMatrixSize = 0;
            const int maxMatrixSize = -1;
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
                    //IMarkovMatrix<string, double> matrix = stringMarkovMatrixLoaderFromText.LoadMatrix(inputFileStream, whiteListedWords, maxMatrixSize);
                    IMarkovMatrix<string, double> matrix = stringMarkovMatrixLoaderFromText.LoadMatrix(inputFileStream, whiteListedWords, maxMatrixSize);
                    binaryStringMarkovMatrixSaver.SaveMatrix(matrix, outputFileStream);
                }
            }
        }

        private static void TestLoadPerformance(Bootstrap bootstrap)
        {
            IMarkovMatrixLoader<string, double> binaryStringMarkovMatrixLoader = bootstrap.BuildBinaryStringMarkovMatrixLoader();

            const string inputFile = "./english.word.matrix.with.interrogation.bin";

            for (int i = 0; i < 100; ++i)
            {
                using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open))
                {
                    IMarkovMatrix<string, double> matrix = binaryStringMarkovMatrixLoader.LoadMatrix(inputFileStream);
                }
            }
        }
    }
}
