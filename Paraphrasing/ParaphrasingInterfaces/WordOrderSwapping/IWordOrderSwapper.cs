using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Paraphrasing
{
    public interface IWordOrderSwapper
    {
        string SwapWordOrder(string text, HashSet<string> wordsToSwap, HashSet<string> wordsToSkip, List<Regex> wordsRegexToSkipWhileSwapping, int offset);
    }
}
