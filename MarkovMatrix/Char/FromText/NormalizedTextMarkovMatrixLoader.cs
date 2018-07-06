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
            return this.LoadMatrix(inputStream, null);
        }

        public IMarkovMatrix<char, double> LoadMatrix(Stream inputStream, HashSet<char> optionalWhiteList)
        {
            IMarkovMatrix<char, ulong> markovMatrix = this.internalMarkovMatrixLoader.LoadMatrix(inputStream, optionalWhiteList);
            IMarkovMatrix<char, double> normalizedMatrix = this.markovMatrixNormalizer.Normalize(markovMatrix);
            return normalizedMatrix;
        }

        public IMarkovMatrix<char, double> LoadMatrix(string text)
        {
            return this.LoadMatrix(text, null);
        }

        public IMarkovMatrix<char, double> LoadMatrix(string text, HashSet<char> optionalWhiteList)
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
