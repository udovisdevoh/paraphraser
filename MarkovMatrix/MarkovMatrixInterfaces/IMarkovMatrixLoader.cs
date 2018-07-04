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

        IMarkovMatrix<TKey, TValue> LoadMatrix(string text);
    }
}
