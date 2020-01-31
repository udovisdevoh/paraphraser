using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonologicalTransformations
{
    public interface ILetterDistanceEvaluator
    {
        int GetDistance(char letter1, char letter2);

        IEnumerable<KeyValuePair<char, int>> GetReplacementLetters(char letter);
    }
}
