using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paraphrasing
{
    public class FrenchSentenceTypeDetector : SentenceTypeDetector
    {
        public override SentenceType GetSentenceType(string sentence)
        {
            #warning Implement
            #warning Add unit tests

            if (sentence.Contains('?'))
            {
                return SentenceType.Interrogative;
            }

            return SentenceType.Affirmative;
        }
    }
}
