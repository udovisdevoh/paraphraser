using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class BinaryStringMarkovMatrixSaver : IMarkovMatrixSaver<string, double>
    {
        public void SaveMatrix(IMarkovMatrix<string, double> markovMatrix, Stream outputStream)
        {
            #warning Todo implement
            #warning Todo add unit tests
            throw new NotImplementedException();
        }
    }
}
