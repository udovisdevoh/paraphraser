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

        Dictionary<TKey, uint> ValueMap { get; }

        TValue GetSum(TKey fromElement);

        TValue GetOccurrence(TKey fromElement, TKey toElement);

        ulong CombineElements(TKey fromElement, TKey toElement);
    }
}
