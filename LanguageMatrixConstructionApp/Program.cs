using MarkovMatrices;
using ParaphaserBootstrap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Resolution;

namespace LanguageMatrixConstruction
{
    public static class Program
    {
        public static void Main()
        {
            Bootstrap bootstrap = new Bootstrap();

            const string sampleTextDirectory = "./LanguageSamplesText/";
            //const string sampleTextDirectory = "./LanguageSamplesDict/";
            const string outputMatrixDirectory = "./";

            ILanguageMatrixBuilder languageMatrixBuilder = bootstrap.BuildLanguageMatrixBuilder();
            languageMatrixBuilder.BuildMissingLanguageMatrices(sampleTextDirectory, outputMatrixDirectory);

            Console.WriteLine("Finished creating missing language matrices");
            Console.ReadKey();
        }
    }
}
