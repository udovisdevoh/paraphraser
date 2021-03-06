﻿using LanguageDetection;
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
        private string targetLanguage;

        private LanguageDetectionState source;

        private LanguageDetectionState destination;

        private bool isTimeOut = false;

        private ILanguageDetector languageDetector;

        private TextMarkovMatrixLoader matrixLoader;

        private IMarkovMatrixNormalizer<char> markovMatrixConverter;

        private ILetterDistanceEvaluator letterDistanceEvaluator;
        #endregion

        #region Constructors
        public LanguageDetectionPathFindingQuery(LanguageDetectionState source,
            LanguageDetectionState destination,
            ILanguageDetector languageDetector,
            TextMarkovMatrixLoader matrixLoader,
            IMarkovMatrixNormalizer<char> markovMatrixConverter,
            ILetterDistanceEvaluator letterDistanceEvaluator,
            string targetLanguage)
        {
            this.targetLanguage = targetLanguage;
            this.source = source;
            this.destination = destination;
            this.languageDetector = languageDetector;
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
            if (source.CurrentLanguageDetectionScore < destination.CurrentLanguageDetectionScore)
            {
                if (state.CurrentLanguageDetectionScore > destination.CurrentLanguageDetectionScore)
                {
                    return 0.0f;
                }
            }
            else
            {
                if (state.CurrentLanguageDetectionScore < destination.CurrentLanguageDetectionScore)
                {
                    return 0.0f;
                }
            }

            return (float)Math.Abs(state.CurrentLanguageDetectionScore - destination.CurrentLanguageDetectionScore) * 10.0f;
        }

        public void PopulateAdjacentStatesTempList(PathNode<LanguageDetectionState> node, List<AdjacentState<LanguageDetectionState>> adjacentStates)
        {
            string sourceText = node.State.CurrentText;
            this.PopulateLetterReplaceAdjacentStates(adjacentStates, sourceText);
            //this.PopulateLetterSwapAdjacentStates(adjacentStates, sourceText);
        }

        private void PopulateLetterSwapAdjacentStates(List<AdjacentState<LanguageDetectionState>> adjacentStates, string sourceText)
        {
            int letterIndex2;
            for (int letterIndex1 = 0; letterIndex1 < sourceText.Length - 1; ++letterIndex1)
            {
                letterIndex2 = letterIndex1 + 1;

                char letter1 = char.ToLowerInvariant(sourceText[letterIndex1]);
                char letter2 = char.ToLowerInvariant(sourceText[letterIndex2]);

                if (letter1 != letter2 && char.IsLetter(letter1) && char.IsLetter(letter2))
                {
                    char[] letters = sourceText.ToCharArray();
                    letters[letterIndex1] = letter2;
                    letters[letterIndex2] = letter1;

                    string modifiedText = new string(letters);

                    float movementCost = letterDistanceEvaluator.LetterSwapCost;
                    double currentLanguageDetectionScore = this.languageDetector.GetLanguageDetectionScore(modifiedText, this.targetLanguage);

                    LanguageDetectionState adjacentLanguagDetectionState = new LanguageDetectionState(modifiedText, currentLanguageDetectionScore);
                    AdjacentState<LanguageDetectionState> adjacentState = new AdjacentState<LanguageDetectionState>(adjacentLanguagDetectionState, movementCost);
                    adjacentStates.Add(adjacentState);
                }
            }
        }

        private void PopulateLetterReplaceAdjacentStates(List<AdjacentState<LanguageDetectionState>> adjacentStates, string sourceText)
        {
            for (int letterIndex = 0; letterIndex < sourceText.Length; ++letterIndex)
            {
                char currentLetter = sourceText[letterIndex];

                foreach (KeyValuePair<char, int> replacementLetterAndDistance in this.letterDistanceEvaluator.GetReplacementLetters(currentLetter))
                {
                    char replacementLetter = replacementLetterAndDistance.Key;
                    char[] letters = sourceText.ToCharArray();
                    letters[letterIndex] = replacementLetter;
                    string modifiedText = new string(letters);

                    float movementCost = (float)replacementLetterAndDistance.Value;
                    double currentLanguageDetectionScore = this.languageDetector.GetLanguageDetectionScore(modifiedText, this.targetLanguage);

                    LanguageDetectionState adjacentLanguagDetectionState = new LanguageDetectionState(modifiedText, currentLanguageDetectionScore);
                    AdjacentState<LanguageDetectionState> adjacentState = new AdjacentState<LanguageDetectionState>(adjacentLanguagDetectionState, movementCost);
                    adjacentStates.Add(adjacentState);
                }
            }
        }
    }
}
