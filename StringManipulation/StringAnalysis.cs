﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringManipulation
{
    public static class StringAnalysis
    {
        #region Members
        private const string punctuationCharsString = "[](){}⟨⟩:;,،、‒–—―….⋯᠁ฯ!.‹›«»‐-?‘’“”\"/⧸⁄\\¿*+-@#÷×^";

        private static HashSet<char> punctuationChars;
        #endregion

        static StringAnalysis()
        {
            StringAnalysis.punctuationChars = new HashSet<char>(punctuationCharsString.ToCharArray());
        }

        public static bool IsPunctuationOrSpace(char character)
        {
            return character == ' ' || character == '\n' || character == '\t' || character == '\r' || Char.IsPunctuation(character) || punctuationChars.Contains(character);
        }

        public static string GetIdenticalWordRegardlessPunctuation(string sourceWord, List<string> otherWords)
        {
            string sourceWordWithoutPunctuation = StringFormatter.RemovePunctuation(sourceWord).ToLowerInvariant().Replace(" ", "");

            foreach (string otherWord in otherWords)
            {
                string otherWordWithoutPunctuation = StringFormatter.RemovePunctuation(otherWord).ToLowerInvariant().Replace(" ", "");
                if (otherWordWithoutPunctuation == sourceWordWithoutPunctuation)
                {
                    return otherWord;
                }
            }

            return null;
        }

        public static string GetMostSimilarWord(string sourceWord, List<string> otherWords)
        {
            string sourceWordLowerInvariant = sourceWord.ToLowerInvariant();

            int closestDistance = int.MaxValue;
            string closestWord = null;

            foreach (string otherWord in otherWords)
            {
                int distance = StringAnalysis.GetLevenshteinDistance(sourceWordLowerInvariant, otherWord.ToLowerInvariant()) * 1000 + otherWord.Length;
                if (closestWord == null || distance < closestDistance)
                {
                    closestWord = otherWord;
                    closestDistance = distance;
                }
            }

            return closestWord;
        }

        public static int GetLevenshteinDistance(string word1, string word2)
        {
            if (string.IsNullOrEmpty(word1))
            {
                if (!string.IsNullOrEmpty(word2))
                {
                    return word2.Length;
                }
                return 0;
            }

            if (string.IsNullOrEmpty(word2))
            {
                if (!string.IsNullOrEmpty(word1))
                {
                    return word1.Length;
                }
                return 0;
            }

            int cost;
            int[,] d = new int[word1.Length + 1, word2.Length + 1];
            int min1;
            int min2;
            int min3;

            for (Int32 i = 0; i <= d.GetUpperBound(0); i += 1)
            {
                d[i, 0] = i;
            }

            for (Int32 i = 0; i <= d.GetUpperBound(1); i += 1)
            {
                d[0, i] = i;
            }

            for (Int32 i = 1; i <= d.GetUpperBound(0); i += 1)
            {
                for (Int32 j = 1; j <= d.GetUpperBound(1); j += 1)
                {
                    cost = Convert.ToInt32(!(word1[i - 1] == word2[j - 1]));

                    min1 = d[i - 1, j] + 1;
                    min2 = d[i, j - 1] + 1;
                    min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }

            return d[d.GetUpperBound(0), d.GetUpperBound(1)];

        }
    }
}