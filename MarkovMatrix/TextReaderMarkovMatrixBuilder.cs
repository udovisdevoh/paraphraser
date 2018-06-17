using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class TextReaderMarkovMatrixBuilder : IMarkovMatrixBuilder
    {
        #region Constructors
        public TextReaderMarkovMatrixBuilder(StreamReader streamReader)
        {
            #warning Implement
        }
        #endregion

        public IMarkovMatrix BuildMatrix()
        {
            #warning Implement
            throw new NotImplementedException();
        }
    }
}
