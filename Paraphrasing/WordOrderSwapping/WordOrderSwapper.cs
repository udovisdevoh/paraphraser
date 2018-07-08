using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Paraphrasing
{
    public class WordOrderSwapper : IWordOrderSwapper
    {
        public string SwapWordOrder(string text, HashSet<string> wordsToSwap, HashSet<string> wordsToSkip, List<Regex> wordsRegexToSkipWhileSwapping, int offset)
        {
            #warning Add unit tests

            return StringFormatter.SwapWordOrder(text, wordsToSwap, wordsToSkip, wordsRegexToSkipWhileSwapping, offset, 1);
        }
    }
}
