﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonologicalTransformations
{
    public class LetterReplacer
    {
        #region Members
        private ILetterDistanceEvaluator letterDistanceEvaluator;
        #endregion

        #region Constructors
        public LetterReplacer(ILetterDistanceEvaluator letterDistanceEvaluator)
        {
            this.letterDistanceEvaluator = letterDistanceEvaluator;
        }
        #endregion

        public IEnumerable<KeyValuePair<string, int>> GetSingleLetterReplacements(string inputString)
        {
            int position = 0;
            foreach (char letter in inputString)
            {
                foreach (KeyValuePair<string, int> replacement in this.GetSingleLetterReplacements(inputString, letter, position))
                {
                    yield return replacement;
                }
                ++position;
            }
        }

        private IEnumerable<KeyValuePair<string, int>> GetSingleLetterReplacements(string inputString, char letter, int position)
        {
            foreach (KeyValuePair<char, int> replacement in this.GetSingleLetterReplacements(letter))
            {
                char[] charArray = inputString.ToCharArray();
                charArray[position] = replacement.Key;
                string newString = charArray.ToString();
                yield return new KeyValuePair<string, int>(newString, replacement.Value);
            }
        }

        public IEnumerable<KeyValuePair<char, int>> GetSingleLetterReplacements(char letter)
        {
            foreach (KeyValuePair<char, int> replacementLetterAndDistance in letterDistanceEvaluator.GetReplacementLetters(letter))
            {
                char replacementLetter = replacementLetterAndDistance.Key;
                int distance = replacementLetterAndDistance.Value;
                if (letter != replacementLetter)
                {
                    yield return new KeyValuePair<char, int>(replacementLetter, distance);
                }
            }
        }
    }
}