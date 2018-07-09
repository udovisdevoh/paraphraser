using LanguageDetection;
using ParaphaserBootstrap;
using Paraphrasing;
using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatConsoleApp
{
    public static class Program
    {
        private const string botName = "Bot";

        public static void Main(string[] args)
        {
            Console.WriteLine("Loading...");

            Bootstrap bootstrap = new Bootstrap();

            ISentenceTypeDetector sentenceTypeDetector = bootstrap.BuildSentenceTypeDetector();
            IEnglishInterrogativeToAffirmative englishInterrogativeToAffirmative = bootstrap.BuildEnglishInterrogativeToAffirmative();
            IFirstSecondPersonInverter firstSecondPersonInverter = bootstrap.BuildFirstSecondPersonInverter();

            Console.WriteLine(string.Format("{0}: Hi!", botName));

            while (true)
            {
                string input = Console.ReadLine();

                if (sentenceTypeDetector.GetSentenceType(input) == SentenceType.Interrogative)
                {
                    input = englishInterrogativeToAffirmative.Convert(input);
                }

                string output = firstSecondPersonInverter.Convert(input);

                Console.WriteLine("{0}: {1}", botName, output);
            }
        }
    }
}
