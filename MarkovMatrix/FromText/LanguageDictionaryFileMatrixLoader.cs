using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices.FromText
{
    public class LanguageDictionaryFileMatrixLoader<T> : TextMarkovMatrixLoader<T>
        where T : struct
    {
        public override string PerformLineTransformations(string line, int lineNumber)
        {
            line = line.Trim();
            line = line.ToLowerInvariant();

            if (lineNumber == 0)
            {
                return string.Empty;
            }

            if (StringAnalysis.StartsWithNumber(line))
            {
                return string.Empty;
            }
            else if (StringAnalysis.StartsWithPunctuationOrSpace(line) && line[0] != '\'')
            {
                return string.Empty;
            }
            else if (line.EndsWith(".dic"))
            {
                return string.Empty;
            }
            else if (line.StartsWith("version:"))
            {
                return string.Empty;
            }

            line = StringFormatter.SplitBefore(line, '/');

            return line;
        }
    }
}
