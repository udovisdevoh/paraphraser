using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public interface IMarkovMatrixLoader<TKey, TValue>
        where TValue : struct
    {
        IMarkovMatrix<TKey, TValue> LoadMatrix(Stream inputStream);

        IMarkovMatrix<TKey, TValue> LoadMatrix(Stream inputStream, int maxSize);

        IMarkovMatrix<TKey, TValue> LoadMatrix(Stream inputStream, HashSet<TKey> optionalWhiteList);

        IMarkovMatrix<TKey, TValue> LoadMatrix(Stream inputStream, HashSet<TKey> optionalWhiteList, int maxSize);

        IMarkovMatrix<TKey, TValue> LoadMatrix(string text);

        IMarkovMatrix<TKey, TValue> LoadMatrix(string text, HashSet<TKey> optionalWhiteList);
    }
}
