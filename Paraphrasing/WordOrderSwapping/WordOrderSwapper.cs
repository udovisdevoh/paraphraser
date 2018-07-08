using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paraphrasing
{
    public class WordOrderSwapper : IWordOrderSwapper
    {
        public string SwapWordOrder(string text, HashSet<string> wordsToSwap, HashSet<string> wordsToSkip, int offset)
        {
            #warning Add unit tests

            return StringFormatter.SwapWordOrder(text, wordsToSwap, wordsToSkip, offset, 1);
        }
    }
}
