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

        Dictionary<TKey, ushort> ValueMap { get; }

        TValue GetSum(TKey fromElement);

        TValue GetOccurrence(TKey fromElement, TKey toElement);

        uint CombineElements(TKey fromElement, TKey toElement);
    }
}
