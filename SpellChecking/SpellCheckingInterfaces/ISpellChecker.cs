using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellChecking
{
    public interface ISpellChecker : IDisposable
    {
        string GetCorrectedText(string originalText, string replaceUnmatchedWordWith);

        string GetCorrectedWord(string wordOrPunctuationToken);

        string GetCorrectedWord(string wordOrPunctuationToken, out bool isMatched);

        bool ContainsWord(string word);

        int CountExistingWords(string[] words);
    }
}
