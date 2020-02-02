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
            string targetLanguage = "English";
            const string textInput = "Text summarization aims to extract essential information from a piece of text and transform it into a concise version.";
            //const string textInput = "Ceci est une poule, je suis une banane. Gros jambon à l'école.";
            //const string textInput = "Ceci est une poule.";
            //const string textInput = "rickstend verlact make torfield mory.";
            LanguageDetector languageDetector = bootstrap.BuildLanguageDetectorByMarkovMatrixBasedOnTextFiles("./TextSamples/");

            KeyValuePair<string, double>[] languageProximities = languageDetector.GetLanguageProximities(textInput);



            /*
            string detectedLanguage = languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).First().Key;
            string secondMatchLanguage = languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).ToArray()[1].Key;
            targetLanguage = secondMatchLanguage;

            double targetLanguageDetectionScore = languageDetector.GetLanguageDetectionScore(textInput, targetLanguage);
            double detectedLanguageDectectionScore = languageDetector.GetLanguageDetectionScore(textInput, detectedLanguage);

            LanguageDetectionState sourceNode = new LanguageDetectionState(textInput, targetLanguageDetectionScore);
            LanguageDetectionState destinationNode = new LanguageDetectionState(textInput, detectedLanguageDectectionScore);
            */

            string detectedLanguage = languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).First().Key;
            string otherMatchLanguage = languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).ToArray()[1].Key;

            targetLanguage = detectedLanguage;
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
