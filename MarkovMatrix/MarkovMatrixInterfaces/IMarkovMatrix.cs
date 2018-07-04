using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public interface IMarkovMatrix<TKey, TValue> : IEnumerable<KeyValuePair<Tuple<TKey, TKey>, TValue>>
    {
        int InputCount { get; }

        TValue GetSum(TKey fromChar);

        TValue GetOccurrence(TKey fromChar, TKey toChar);
    }
}
