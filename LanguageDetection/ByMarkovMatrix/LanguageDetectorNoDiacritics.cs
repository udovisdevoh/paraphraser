using MarkovMatrices;
using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetection
{
    public class LanguageDetectorNoDiacritics : LanguageDetectorByMarkovMatrix
    {
        private IMarkovMatrixTransformer markovMatrixTransformer;

        public LanguageDetectorNoDiacritics(IMarkovMatrixLoader<double> languageDetectionMatrixLoader, IMarkovMatrixTransformer markovMatrixTransformer) : base(languageDetectionMatrixLoader)
        {
            this.markovMatrixTransformer = markovMatrixTransformer;
        }

        public override string TransformInput(string text)
        {
            return StringFormatter.RemoveDiacritics(text);
        }

        public override IMarkovMatrix<double> TransformMatrix(IMarkovMatrix<double> matrix)
        {
            return this.markovMatrixTransformer.Transform(matrix);
        }
    }
}
