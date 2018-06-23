using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public interface IMarkovMatrixSaver<T>
    {
        void SaveMatrix(IMarkovMatrix<T> markovMatrix, Stream outputStream);
    }
}
