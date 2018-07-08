using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paraphrasing
{
    public class YodaWordOrderSwapper : IWordOrderSwapper
    {
        public string SwapWordOrder(string text, HashSet<string> wordsToSwap, HashSet<string> wordsToSkip, int offset)
        {
            #warning Implement
            #warning Add unit tests

            const int maxSwapCount = 1;

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset must not be negative");
            }

            int swapCount = 0;
            int swappingPosition = -1;
            bool hasFoundSwapping = false;

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

                    if (wordsToSkip == null || !wordsToSkip.Contains(nextWord))
                    {
                        ++swapCount;
                        swappingPosition = index;
                    }
                }

                if (index > swappingPosition + 3)
                {
                    stringBuilderForBeginning.Append(words[index]);
                }
                else
                {
                    stringBuilderForEnding.Append(words[index]);
                }
            }

            return stringBuilderForBeginning.ToString().Trim() + ", " + stringBuilderForEnding.ToString().Trim();
        }
    }
}
