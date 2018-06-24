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
            const string sampleTextDirectory = "./LanguageSamples/";
            const string outputMatrixDirectory = "./";

            Bootstrap bootstrap = new Bootstrap();

            ILanguageMatrixBuilder languageMatrixBuilder = bootstrap.BuildLanguageMatrixBuilder();
            languageMatrixBuilder.BuildMissingLanguageMatrices(sampleTextDirectory, outputMatrixDirectory);

            Console.WriteLine("Finished creating missing language matrices");
            Console.ReadKey();
        }
    }
}
