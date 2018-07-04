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
        private IMarkovMatrixTransformer<char> markovMatrixTransformer;

        public LanguageDetectorNoDiacritics(IMarkovMatrixLoader<char, double> languageDetectionMatrixLoader, IMarkovMatrixTransformer<char> markovMatrixTransformer) : base(languageDetectionMatrixLoader)
        {
            this.markovMatrixTransformer = markovMatrixTransformer;
        }

        public override string TransformInput(string text)
        {
            return StringFormatter.RemoveDiacritics(text);
        }

        public override IMarkovMatrix<char, double> TransformMatrix(IMarkovMatrix<char, double> matrix)
        {
            return this.markovMatrixTransformer.Transform(matrix);
        }
    }
}
