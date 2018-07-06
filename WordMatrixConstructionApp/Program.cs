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

            using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open))
            {
                using (FileStream outputFileStream = new FileStream(outputFile, FileMode.Create))
                {
                    IMarkovMatrix<string, double> matrix = stringMarkovMatrixLoaderFromText.LoadMatrix(inputFileStream);
                    binaryStringMarkovMatrixSaver.SaveMatrix(matrix, outputFileStream);
                }
            }
        }
    }
}
