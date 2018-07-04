using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public interface IMarkovMatrixTransformer<TKey>
    {
        IMarkovMatrix<TKey, double> Transform(IMarkovMatrix<TKey, double> matrix);
    }
}
