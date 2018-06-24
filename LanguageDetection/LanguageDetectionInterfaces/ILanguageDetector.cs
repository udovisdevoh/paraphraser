using MarkovMatrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetection
{
    public interface ILanguageDetector
    {
        void AddLanguage(string name, IMarkovMatrix<double> languageMatrix);

        string DetectLanguage(string text);

        KeyValuePair<string, double>[] GetLanguageProximities(string text);
    }
}
