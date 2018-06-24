using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class NormalizedTextMarkovMatrixLoader : IMarkovMatrixLoader<double>
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

        public IMarkovMatrix<double> LoadMatrix(Stream inputStream)
        {
            IMarkovMatrix<ulong> markovMatrix = this.internalMarkovMatrixLoader.LoadMatrix(inputStream);
            IMarkovMatrix<double> normalizedMatrix = this.markovMatrixNormalizer.Normalize(markovMatrix);
            return normalizedMatrix;
        }

        public IMarkovMatrix<double> LoadMatrix(string text)
        {
            #warning Add unit tests

            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;

            return this.LoadMatrix(stream);
        }
    }
}
