using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class NormalizedTextMarkovMatrixLoader : IMarkovMatrixLoader<char, double>
    {
        #region Members
        private IMarkovMatrixLoader<char, ulong> internalMarkovMatrixLoader;

        private IMarkovMatrixNormalizer<char> markovMatrixNormalizer;
        #endregion

        #region Constructors
        public NormalizedTextMarkovMatrixLoader(IMarkovMatrixLoader<char, ulong> internalMarkovMatrixLoader, IMarkovMatrixNormalizer<char> markovMatrixNormalizer)
        {
            this.internalMarkovMatrixLoader = internalMarkovMatrixLoader;
            this.markovMatrixNormalizer = markovMatrixNormalizer;
        }
        #endregion

        public IMarkovMatrix<char, double> LoadMatrix(Stream inputStream)
        {
            IMarkovMatrix<char, ulong> markovMatrix = this.internalMarkovMatrixLoader.LoadMatrix(inputStream);
            IMarkovMatrix<char, double> normalizedMatrix = this.markovMatrixNormalizer.Normalize(markovMatrix);
            return normalizedMatrix;
        }

        public IMarkovMatrix<char, double> LoadMatrix(string text)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;

            return this.LoadMatrix(stream);
        }
    }
}
