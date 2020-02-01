using LanguageDetection;
using MarkovMatrices;
using ParaphraserMath;
using PhonologicalTransformations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public class LanguageDetectionPathFindingQuery : IPathfindingQuery<LanguageDetectionState>
    {
        #region Members
        private LanguageDetectionState source;

        private LanguageDetectionState destination;

        private bool isTimeOut = false;

        private IMarkovMatrix<char, double> targetLanguageMatrix;

        private TextMarkovMatrixLoader matrixLoader;

        private IMarkovMatrixNormalizer<char> markovMatrixConverter;

        private ILetterDistanceEvaluator letterDistanceEvaluator;
        #endregion

        #region Constructors
        public LanguageDetectionPathFindingQuery(LanguageDetectionState source,
            LanguageDetectionState destination,
            IMarkovMatrix<char, double> targetLanguageMatrix,
            TextMarkovMatrixLoader matrixLoader,
            IMarkovMatrixNormalizer<char> markovMatrixConverter,
            ILetterDistanceEvaluator letterDistanceEvaluator)
        {
            this.source = source;
            this.destination = destination;
            this.targetLanguageMatrix = targetLanguageMatrix;
            this.matrixLoader = matrixLoader;
            this.markovMatrixConverter = markovMatrixConverter;
            this.letterDistanceEvaluator = letterDistanceEvaluator;
        }
        #endregion

        #region Properties
        public LanguageDetectionState Source
        {
            get { return this.source; }
            set { this.source = value; }
        }

        public LanguageDetectionState Destination
        {
            get { return this.destination; }
            set { this.destination = value; }
        }

        public bool IsTimeOut
        {
            get { return this.isTimeOut; }
            set { this.isTimeOut = value; }
        }
        #endregion

        public float EstimateCostToDestination(LanguageDetectionState state)
        {
            if (state.CurrentLanguageDetectionScore > destination.CurrentLanguageDetectionScore)
            {
                return 0.0f;
            }

            return (float)Math.Abs(state.CurrentLanguageDetectionScore - destination.CurrentLanguageDetectionScore);
        }

        public void PopulateAdjacentStatesTempList(PathNode<LanguageDetectionState> node, List<AdjacentState<LanguageDetectionState>> adjacentStates)
        {
            string sourceText = node.State.CurrentText;

            for (int letterIndex = 0; letterIndex < sourceText.Length;++letterIndex)
            {
                char currentLetter = sourceText[letterIndex];

                foreach (KeyValuePair<char, int> replacementLetterAndDistance in this.letterDistanceEvaluator.GetReplacementLetters(currentLetter))
                {
                    char replacementLetter = replacementLetterAndDistance.Key;
                    char[] letters = sourceText.ToCharArray();
                    letters[letterIndex] = replacementLetter;
                    string modifiedText = new string(letters);

                    float movementCost = (float)replacementLetterAndDistance.Value;
                    double languageProximity = this.GetLanguageProximity(modifiedText);

                    LanguageDetectionState adjacentLanguagDetectionState = new LanguageDetectionState(modifiedText, languageProximity);
                    AdjacentState<LanguageDetectionState> adjacentState = new AdjacentState<LanguageDetectionState>(adjacentLanguagDetectionState, movementCost);
                    adjacentStates.Add(adjacentState);
                }
            }
        }

        public double GetLanguageProximity(string text)
        {
            MemoryStream memoryStream = MemoryStreamBuilder.BuildMemoryStreamFromText(text);
            IMarkovMatrix<char, ulong> inputMatrixLong = this.matrixLoader.LoadMatrix(memoryStream);
            IMarkovMatrix<char, double> inputMatrixDouble = markovMatrixConverter.Normalize(inputMatrixLong);

            List<KeyValuePair<string, double>> languageProximities = new List<KeyValuePair<string, double>>();
            
            double proximity = MatrixMathHelper.GetDotProduct(inputMatrixDouble, this.targetLanguageMatrix);

            return proximity;
        }
    }
}
