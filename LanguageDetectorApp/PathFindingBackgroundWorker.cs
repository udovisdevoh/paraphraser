using LanguageDetection;
using MarkovMatrices;
using PathFinding;
using PhonologicalTransformations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LanguageDetectorApp
{
    public class PathFindingBackgroundWorker
    {
        #region Constants
        private const int maxNodeCount = int.MaxValue;
        #endregion

        #region Members
        private ILanguageDetector languageDetector;

        private RichTextBox richTextBoxPathFindingOutput;

        private TextBox textBox;

        Pathfinder<LanguageDetectionState> pathfinder;

        private string currentText = null;

        private string lastCalculatedText = null;

        private bool isNeedRecalculation = false;

        private object isNeedRecalculationLock = new object();

        private object currentTextChangeLock = new object();

        private object abortLock = new object();
        #endregion

        public PathFindingBackgroundWorker(ILanguageDetector languageDetector, TextBox textBox, RichTextBox richTextBoxPathFindingOutput)
        {
            this.languageDetector = languageDetector;
            this.textBox = textBox;
            this.richTextBoxPathFindingOutput = richTextBoxPathFindingOutput;
            this.pathfinder = new Pathfinder<LanguageDetectionState>(maxNodeCount);
        }

        public void Start()
        {
            Thread thread = new Thread(PathFindingWorker);
            thread.IsBackground = true;
            thread.Start();
        }

        private void PathFindingWorker()
        {
            while (true)
            {
                bool isRecalculate;
                lock (this.isNeedRecalculationLock)
                {
                    isRecalculate = this.isNeedRecalculation;
                }
                if (isRecalculate)
                {
                    this.Recalculate();
                }
                lock (this.isNeedRecalculationLock)
                {
                    if (this.lastCalculatedText == this.currentText)
                    {
                        this.isNeedRecalculation = false;
                    }
                }
                Thread.Sleep(250);
            }
        }

        public void NotifyText(string newText)
        {
            lock (this.currentTextChangeLock)
            {
                if (newText != this.currentText)
                {
                    this.currentText = newText;
                    this.NotifyTextChange();
                }
            }
        }

        private void NotifyTextChange()
        {
            lock (this.isNeedRecalculationLock)
            {
                this.isNeedRecalculation = true;
            }
        }

        private void Recalculate()
        {
            string pathFindingText = this.currentText;
            lock (abortLock)
            {
                this.pathfinder.IsNeedToAbortNow = true;
            }

            this.textBox.BeginInvoke((Action)(() =>
            {
                this.richTextBoxPathFindingOutput.Text = $"Processing \"{this.currentText}\"...";
            }));

            string detectedLanguage;
            string otherMatchLanguage;

            LanguageDetectionState[] path = this.FindPath(pathFindingText, out detectedLanguage, out otherMatchLanguage);

            if (path.Length <= 0)
            {
                return;
            }

            string lastNodeText = path.Last().CurrentText;

            int[][] coloredPositions = this.GetColoredPositions(pathFindingText, path);

            this.textBox.BeginInvoke((Action)(() =>
            {
                this.richTextBoxPathFindingOutput.Text = detectedLanguage + ":\r\n" + pathFindingText + "\r\n" + otherMatchLanguage + ":\r\n" + lastNodeText;
                foreach (int[] currentColorPositions in coloredPositions)
                {
                    foreach (int currentColorPosition in currentColorPositions)
                    {
                        this.richTextBoxPathFindingOutput.Select(currentColorPosition + detectedLanguage.Length + 2, 1);
                        this.richTextBoxPathFindingOutput.SelectionColor = Color.Green;
                    }
                }
            }));

            this.lastCalculatedText = pathFindingText;
        }

        private int[][] GetColoredPositions(string sourceText, LanguageDetectionState[] path)
        {
            int[][] coloredPositions = new int[path.Length][];

            char[] sourceLetters = sourceText.ToCharArray();

            List<int> tempPosition = new List<int>();

            int pathDepthIndex = 0;
            foreach (LanguageDetectionState languageDetectionState in path)
            {
                string currentText = languageDetectionState.CurrentText;

                char[] currentLetters = currentText.ToCharArray();

                tempPosition.Clear();
                int letterIndex = 0;
                foreach (char sourceLetter in sourceLetters)
                {
                    char currentLetter = currentLetters[letterIndex];

                    if (sourceLetter != currentLetter)
                    {
                        tempPosition.Add(letterIndex);
                    }

                    ++letterIndex;
                }

                coloredPositions[pathDepthIndex] = tempPosition.ToArray();

                ++pathDepthIndex;
            }

            return coloredPositions;
        }

        private LanguageDetectionState[] FindPath(string currentText, out string detectedLanguage, out string otherMatchLanguage)
        {
            TextMarkovMatrixLoader matrixLoader = new TextMarkovMatrixLoader();
            ILetterDistanceEvaluator letterDistanceEvaluator = new LetterDistanceEvaluator();

            KeyValuePair<string, double>[] languageProximities = languageDetector.GetLanguageProximities(currentText);

            detectedLanguage = languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).First().Key;
            otherMatchLanguage = languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).Last().Key;
            //string otherMatchLanguage = languageProximities.OrderByDescending(keyValuePair => keyValuePair.Value).ToArray()[1].Key;

            double firstLanguageDectectionScore = languageDetector.GetLanguageDetectionScore(currentText, detectedLanguage);
            double otherLanguageDectectionScore = languageDetector.GetLanguageDetectionScore(currentText, otherMatchLanguage);

            LanguageDetectionState sourceNode = new LanguageDetectionState(currentText, firstLanguageDectectionScore);
            LanguageDetectionState destinationNode = new LanguageDetectionState(currentText, otherLanguageDectectionScore);

            IMarkovMatrixNormalizer<char> markovMatrixNormalizer = new MarkovMatrixNormalizer();
            LanguageDetectionPathFindingQuery languageDetectionPathFindingQuery = new LanguageDetectionPathFindingQuery(sourceNode,
                destinationNode,
                this.languageDetector,
                matrixLoader,
                markovMatrixNormalizer,
                letterDistanceEvaluator,
                detectedLanguage);

            this.pathfinder = new Pathfinder<LanguageDetectionState>(maxNodeCount);
            LanguageDetectionState[] path = this.pathfinder.Find(languageDetectionPathFindingQuery);

            return path;
        }
    }
}
