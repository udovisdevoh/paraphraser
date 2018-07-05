using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class BinaryStringMarkovMatrixLoader : IMarkovMatrixLoader<string, double>
    {
        public IMarkovMatrix<string, double> LoadMatrix(Stream inputStream)
        {
            #warning Todo implement
            #warning Todo add unit tests
            throw new NotImplementedException();
        }

        public IMarkovMatrix<string, double> LoadMatrix(string text)
        {
            #warning Todo add unit tests
            throw new NotSupportedException();
        }
    }
}
