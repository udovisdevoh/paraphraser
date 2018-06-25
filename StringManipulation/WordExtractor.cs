using System;
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

        public static string[] GetLowerInvariantWords(string text)
        {
            #warning Add unit tests

            text = text.ToLowerInvariant();
            string[] wordsAndPunctuationAndSpace = WordExtractor.GetWordsAndPunctuationTokens(text);

            List<string> wordsOnly = new List<string>();

            foreach (string word in wordsAndPunctuationAndSpace)
            {
                if (word.Length > 1 || !StringAnalysis.IsPunctuationOrSpace(word[0]))
                {
                    wordsOnly.Add(word);
                }
            }
            return wordsOnly.ToArray();
        }
    }
}
