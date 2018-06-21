﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringManipulation
{
    public static class WordExtractor
    {
        public static string[] GetWordsAndPunctuationTokens(string originalText)
        {
            List<string> wordsAndPunctuationTokens = new List<string>();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (char character in originalText)
            {
                if (StringAnalysis.IsPunctuationOrSpace(character))
                {
                    if (stringBuilder.Length > 0)
                    {
                        wordsAndPunctuationTokens.Add(stringBuilder.ToString());
                        stringBuilder.Clear();
                    }

                    wordsAndPunctuationTokens.Add(character.ToString());
                }   
                else
                {
                    stringBuilder.Append(character);
                }
            }

            if (stringBuilder.Length > 0)
            {
                wordsAndPunctuationTokens.Add(stringBuilder.ToString());
                stringBuilder.Clear();
            }

            char[] punctuation = originalText.Where(Char.IsPunctuation).Distinct().ToArray();
            string[] words = originalText.Split().Select(character => character.Trim(punctuation)).ToArray();

            return wordsAndPunctuationTokens.ToArray();
        }
    }
}
