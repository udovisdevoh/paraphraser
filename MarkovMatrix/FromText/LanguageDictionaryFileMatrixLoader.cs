using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class LanguageDictionaryFileMatrixLoader : TextMarkovMatrixLoader
    {
        private bool isRemoveDiacritics = false;

        public LanguageDictionaryFileMatrixLoader(bool isRemoveDiacritics)
        {
            this.isRemoveDiacritics = isRemoveDiacritics;
        }

        public override string PerformLineTransformations(string line)
        {
            line = line.Trim();
            line = line.ToLowerInvariant();

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
            else if (RomanNumerals.IsRomanNumeral(line))
            {
                return string.Empty;
            }

            if (this.isRemoveDiacritics)
            {
                line = StringFormatter.RemoveDiacritics(line);
            }

            line = StringFormatter.SplitBefore(line, '/');
            line = StringFormatter.SplitBefore(line, '\t');
            line = StringFormatter.SplitBefore(line, ':');

            return line;
        }
    }
}
