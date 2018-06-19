using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public interface IMarkovMatrixNormalizer
    {
        IMarkovMatrix<double> Normalize(IMarkovMatrix<ulong> sourceMatrix);
    }
}
