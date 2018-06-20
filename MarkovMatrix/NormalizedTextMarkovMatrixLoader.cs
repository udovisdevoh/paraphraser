using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class NormalizedTextMarkovMatrixLoader : IMarkovMatrixLoader<float>
    {
        #region Members
        private IMarkovMatrixLoader<ulong> internalMarkovMatrixLoader;

        private IMarkovMatrixNormalizer markovMatrixNormalizer;
        #endregion

        #region Constructors
        public NormalizedTextMarkovMatrixLoader(IMarkovMatrixLoader<ulong> internalMarkovMatrixLoader, IMarkovMatrixNormalizer markovMatrixNormalizer)
        {
            this.internalMarkovMatrixLoader = internalMarkovMatrixLoader;
            this.markovMatrixNormalizer = markovMatrixNormalizer;
        }
        #endregion

        public IMarkovMatrix<float> LoadMatrix(Stream inputStream)
        {
            #warning Add unit tests
            IMarkovMatrix<ulong> markovMatrix = this.internalMarkovMatrixLoader.LoadMatrix(inputStream);
            IMarkovMatrix<float> normalizedMatrix = this.markovMatrixNormalizer.Normalize(markovMatrix);
            return normalizedMatrix;
        }
    }
}
