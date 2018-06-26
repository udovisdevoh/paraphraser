using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetection
{
    public interface ICompositeLanguageDetector : ILanguageDetector
    {
        void AddLanguageDetector(ILanguageDetector languageDetector);
    }
}
