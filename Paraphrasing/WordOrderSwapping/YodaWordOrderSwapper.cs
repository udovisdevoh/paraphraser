using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Paraphrasing
{
    public class YodaWordOrderSwapper : IWordOrderSwapper
    {
        private static HashSet<string> wordsToMoveToEnd = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "not", "do", "be"
        };

        private static HashSet<string> wordsToDeleteFromStart = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "and"
        };

        public string SwapWordOrder(string text, HashSet<string> wordsToSwap, HashSet<string> wordsToSkip, List<Regex> wordsRegexToSkipWhileSwapping, int offset)
        {
            text = this.SwapQuestionWord(text, wordsToSwap, wordsToSkip, offset);

            text = this.MoveWordsToEnd(text, wordsToMoveToEnd);

            text = this.DeleteWordsFromStart(text, wordsToDeleteFromStart);

            return text;
        }

        private string DeleteWordsFromStart(string text, HashSet<string> wordsToDeleteFromStart)
        {
            string[] words = WordExtractor.GetWordsAndPunctuationTokens(text, '\'');

            if (words.Length >= 2)
            {
                if (wordsToDeleteFromStart.Contains(words[0]))
                {
                    words[0] = string.Empty;
                    words[1] = string.Empty;
                }
            }

            StringBuilder stringBuilder = new StringBuilder();
            foreach (string word in words)
            {
                stringBuilder.Append(word);
            }

            return stringBuilder.ToString();
        }

        private string MoveWordsToEnd(string text, HashSet<string> wordsToMoveToEnd)
        {
            string[] words = WordExtractor.GetWordsAndPunctuationTokens(text, '\'');

            if (words.Length >= 2)
            {
                if (wordsToMoveToEnd.Contains(words[0]))
                {
                    string endingWord = words[0];
                    words[0] = string.Empty;
                    words[1] = string.Empty;

                    words[words.Length - 1] = words[words.Length - 1] + " " + endingWord;
                }
            }

            StringBuilder stringBuilder = new StringBuilder();
            foreach (string word in words)
            {
                stringBuilder.Append(word);
            }

            return stringBuilder.ToString();
        }

        public string SwapQuestionWord(string text, HashSet<string> wordsToSwap, HashSet<string> wordsToSkip, int offset)
        {
            const int maxSwapCount = 1;

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset must not be negative");
            }

            int swapCount = 0;
            int swappingPosition = -1;

            string[] words = WordExtractor.GetWordsAndPunctuationTokens(text, '\'');

            StringBuilder stringBuilderForBeginning = new StringBuilder();
            StringBuilder stringBuilderForEnding = new StringBuilder();
            for (int index = 0; index < words.Length; ++index)
            {
                bool isAllowSwappingByCount = swapCount < maxSwapCount;
                bool isAllowSwappingByWordsToSwap = wordsToSwap.Contains(words[index]);
                bool isAllowSwappingByWordIndex = index < words.Length - (offset * 2);

                if (isAllowSwappingByWordsToSwap && isAllowSwappingByWordIndex && isAllowSwappingByCount)
                {
                    string nextWord = words[index + (offset * 2)];
                    words[index + (offset * 2)] = words[index];
                    words[index] = nextWord;

                    if (wordsToSkip == null || (!wordsToSkip.Contains(nextWord)))
                    {
                        ++swapCount;
                        swappingPosition = index;
                    }
                }

                if (index > swappingPosition + 3)
                {
                    stringBuilderForBeginning.Append(" ");
                    stringBuilderForBeginning.Append(words[index]);
                }
                else
                {
                    stringBuilderForEnding.Append(" ");
                    stringBuilderForEnding.Append(words[index]);
                }
            }

            string firstPart = stringBuilderForBeginning.ToString().Trim();

            if (!string.IsNullOrWhiteSpace(firstPart))
            {
                text = StringFormatter.RemoveDoubleTabsSpacesAndEnters(firstPart + ", " + stringBuilderForEnding.ToString().Trim());
            }
            else
            {
                text = StringFormatter.RemoveDoubleTabsSpacesAndEnters(stringBuilderForEnding.ToString().Trim());
            }
            return text;
        }
    }
}
