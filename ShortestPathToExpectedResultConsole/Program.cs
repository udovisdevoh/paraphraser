using LanguageDetection;
using MarkovMatrices;
using ParaphaserBootstrap;
using PathFinding;
using PhonologicalTransformations;
using StringManipulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPathToExpectedResultConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<KeyValuePair<char, int>> letters = letterDistanceEvaluator.GetReplacementLetters('Q').OrderBy(keyValuePair => keyValuePair.Value).ToList();

            Bootstrap bootstrap = new Bootstrap();

            const int maxNodeCount = int.MaxValue;
            const string targetLanguage = "English";
            //const string textInput = "Text summarization aims to extract essential information from a piece of text and transform it into a concise version.";
            const string textInput = "Ceci est une poule, je suis une banane. Gros jambon à l'école.";
            //const string textInput = "Ceci est une poule.";
            //const string textInput = "rickstend verlact make torfield mory.";
            LanguageDetectorByMarkovMatrix languageDetector = bootstrap.BuildLanguageDetectorByMarkovMatrixBasedOnTextFiles("./TextSamples/");

            KeyValuePair<string, double>[] languageProximities = languageDetector.GetLanguageProximities(textInput);

            string detectedLanguage = languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).First().Key;
            double targetLanguageProximity = languageProximities.Where(keyValuePair => keyValuePair.Key == targetLanguage).First().Value;
            double detectedLanguageProximity = languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).First().Value;
            

            LanguageDetectionState sourceNode = new LanguageDetectionState(textInput, targetLanguageProximity);
            LanguageDetectionState destinationNode = new LanguageDetectionState(textInput, detectedLanguageProximity);
            TextMarkovMatrixLoader matrixLoader = new TextMarkovMatrixLoader();
            ILetterDistanceEvaluator letterDistanceEvaluator = new LetterDistanceEvaluator();

            Pathfinder<LanguageDetectionState> pathfinder = new Pathfinder<LanguageDetectionState>(maxNodeCount);
            IMarkovMatrixNormalizer<char> markovMatrixNormalizer = new MarkovMatrixNormalizer();
            LanguageDetectionPathFindingQuery languageDetectionPathFindingQuery = new LanguageDetectionPathFindingQuery(sourceNode,
                destinationNode,
                languageDetector.GetLanguageMatrix(targetLanguage),
                matrixLoader,
                markovMatrixNormalizer,
                letterDistanceEvaluator);

            LanguageDetectionState[] path = pathfinder.Find(languageDetectionPathFindingQuery);
        }
    }
}
