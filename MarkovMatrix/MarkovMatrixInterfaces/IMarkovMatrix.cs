using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public interface IMarkovMatrix<T> : IEnumerable<KeyValuePair<Tuple<char, char>, T>>
    {
        int InputCount { get; }

        T GetSum(char fromChar);

        T GetOccurrence(char fromChar, char toChar);
    }
}
