using MarkovMatrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetection
{
    public class LanguageDetector : ILanguageDetector
    {
        private Dictionary<string, IMarkovMatrix<float>> languages;

        public LanguageDetector()
        {
            this.languages = new Dictionary<string, IMarkovMatrix<float>>();
        }

        public void AddLanguage(string name, IMarkovMatrix<float> languageMatrix)
        {
            #warning Add unit tests
            name = StringManipulation.FormatLanguageName(name);
            this.languages.Add(name, languageMatrix);
        }

        public string DetectLanguage(string text)
        {
            #warning Implement
            #warning Add unit tests
            
            throw new NotImplementedException();
        }
    }
}
