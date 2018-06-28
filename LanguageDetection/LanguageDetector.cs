using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetection
{
    public abstract class LanguageDetector : ILanguageDetector
    {
        public abstract IEnumerable<string> GetLanguageList();

        public abstract KeyValuePair<string, double>[] GetLanguageProximities(string text);

        public virtual string DetectLanguage(string text)
        {
            return this.GetLanguageProximities(text)[0].Key;
        }
    }
}
