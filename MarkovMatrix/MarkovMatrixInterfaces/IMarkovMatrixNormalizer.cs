using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public interface IMarkovMatrixNormalizer<TKey>
    {
        IMarkovMatrix<TKey, double> Normalize(IMarkovMatrix<TKey, ulong> sourceMatrix);
    }
}
