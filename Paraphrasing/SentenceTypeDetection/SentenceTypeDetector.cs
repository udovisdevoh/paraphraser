using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paraphrasing
{
    public abstract class SentenceTypeDetector : ISentenceTypeDetector
    {
        public abstract SentenceType GetSentenceType(string sentence);
    }
}
