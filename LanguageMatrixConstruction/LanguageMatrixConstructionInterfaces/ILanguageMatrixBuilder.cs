using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageMatrixConstruction
{
    public interface ILanguageMatrixBuilder
    {
        void BuildMissingLanguageMatrices(string inputDirectory, string outputDirectory);
    }
}