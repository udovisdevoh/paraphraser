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
        public string SwapWordOrder(string text, HashSet<string> wordsToSwap, int offset)
        {
            return StringFormatter.SwapWordOrder(text, wordsToSwap, offset, 1);
        }
    }
}
