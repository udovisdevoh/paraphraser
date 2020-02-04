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
            Bootstrap bootstrap = new Bootstrap();

            const int maxNodeCount = int.MaxValue;
            const string textInput = "Text summarization aims to extract essential information from a piece of text and transform it into a concise version.";
            LanguageDetector languageDetector = bootstrap.BuildLanguageDetectorByMarkovMatrixBasedOnTextFiles("./TextSamples/");

            KeyValuePair<string, double>[] languageProximities = languageDetector.GetLanguageProximities(textInput);



            string detectedLanguage = languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).First().Key;
            string otherMatchLanguage = languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).Last().Key;

            string targetLanguage = detectedLanguage;
            double firstLanguageDectectionScore = languageDetector.GetLanguageDetectionScore(textInput, detectedLanguage);
            double otherLanguageDectectionScore = languageDetector.GetLanguageDetectionScore(textInput, otherMatchLanguage);

            LanguageDetectionState sourceNode = new LanguageDetectionState(textInput, firstLanguageDectectionScore);
            LanguageDetectionState destinationNode = new LanguageDetectionState(textInput, otherLanguageDectectionScore);

            TextMarkovMatrixLoader matrixLoader = new TextMarkovMatrixLoader();
            ILetterDistanceEvaluator letterDistanceEvaluator = new LetterDistanceEvaluator();

            //List<KeyValuePair<char, int>> letters = letterDistanceEvaluator.GetReplacementLetters('à').ToList();

            Pathfinder<LanguageDetectionState> pathfinder = new Pathfinder<LanguageDetectionState>(maxNodeCount);
            IMarkovMatrixNormalizer<char> markovMatrixNormalizer = new MarkovMatrixNormalizer();
            LanguageDetectionPathFindingQuery languageDetectionPathFindingQuery = new LanguageDetectionPathFindingQuery(sourceNode,
                destinationNode,
                languageDetector,
                matrixLoader,
                markovMatrixNormalizer,
                letterDistanceEvaluator,
                targetLanguage);

            LanguageDetectionState[] path = pathfinder.Find(languageDetectionPathFindingQuery);
        }
    }
}
