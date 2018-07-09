using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paraphrasing
{
    public class FirstSecondPersonInverter : IFirstSecondPersonInverter
    {
        private static HashSet<string> prepositons = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
            "aboard", "about", "above", "across", "cross", "after", "against", "'gainst", "gainst", "along",
            "alongst", "'long", "alongside", "amid", "amidst", "midst", "among", "amongst", "'mong", "mong",
            "'mongst", "mongst", "of", "apud", "around", "'round", "round", "as", "astride", "at",
            "atop", "ontop", "before", "afore", "tofore", "b4", "behind", "ahind", "below", "ablow", "allow",
            "beneath", "'neath", "neath", "beside", "besides", "between", "atween", "beyond", "ayond", "by",
            "chez", "circa", "despite", "spite", "down", "during", "except", "for", "4", "from", "in",
            "inside", "into", "less", "like", "minus", "near", "nearer", "anear", "notwithstanding", "of",
            "off", "onto", "opposite", "out", "outen", "outside", "over", "pace", "past", "per", "plus",
            "post", "pre", "pro", "qua", "sans", "save", "sauf", "short", "since", "sithence", "than",
            "through", "thru", "throughout", "thruout", "till", "to", "2", "toward", "towards", "under",
            "underneath", "unlike", "until", "'till", "unto", "up", "upon", "pon", "'pon", "upside",
            "versus", "vs", "via", "vice", "vis-à-vis", "vis-a-vis", "with", "within", "without", "worth",
            "on"
        };

        public string Convert(string text)
        {
            string[] words = WordExtractor.GetWordsAndPunctuationTokens(text.ToLowerInvariant(), '\'', '-');

            for (int index = 0; index < words.Length; ++index)
            {
                string currentWord = words[index];

                if (StringAnalysis.IsPunctuationOrSpace(currentWord))
                {
                    continue;
                }

                if (index + 2 < words.Length)
                {
                    string nextWord = words[index + 2];

                    Tuple<string, string> replacedWordPair;
                    if (this.TryReplaceWordPair(currentWord, nextWord, out replacedWordPair))
                    {
                        words[index] = replacedWordPair.Item1;
                        words[index + 2] = replacedWordPair.Item2;

                        if (string.IsNullOrEmpty(words[index + 2])) // second word is removed, we remove space after it
                        {
                            words[index + 1] = string.Empty;
                        }

                        index += 2; // Skip next word
                        continue;
                    }
                }

                words[index] = this.TryReplaceWord(words[index]);
            }

            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < words.Length; ++index)
            {
                stringBuilder.Append(words[index]);
            }

            return stringBuilder.ToString();
        }

        private string TryReplaceWord(string word)
        {
            if (word == "i" || word == "me" || word == "we")
            {
                return "you";
            }
            else if (word == "us")
            {
                return "you guys";
            }
            else if (word == "my" || word == "our")
            {
                return "your";
            }
            else if (word == "mine" || word == "ours")
            {
                return "yours";
            }
            else if (word == "myself")
            {
                return "yourself";
            }
            else if (word == "ourselves")
            {
                return "yourselves";
            }
            else if (word == "i'm" || word == "we're")
            {
                return "you're";
            }
            else if (word == "you" || word == "thou" || word == "ye")
            {
                return "i";
            }
            else if (word == "you're")
            {
                return "i'm";
            }
            else if (word == "thee")
            {
                return "me";
            }
            else if (word == "your" || word == "thy")
            {
                return "my";
            }
            else if (word == "yours" || word == "thine")
            {
                return "mine";
            }
            else if (word == "yourself" || word == "thyself")
            {
                return "myself";
            }
            else if (word == "yourselves")
            {
                return "ourselves";
            }
            else if (word == "y'all" || word == "y'all's")
            {
                return "us";
            }

            return word;
        }

        private bool TryReplaceWordPair(string word1, string word2, out Tuple<string, string> replacedWordPair)
        {
            replacedWordPair = null;
            if (word1 == "i")
            {
                if (word2 == "am")
                {
                    replacedWordPair = new Tuple<string, string>("you", "are");
                    return true;
                }
                else if (word2 == "was")
                {
                    replacedWordPair = new Tuple<string, string>("you", "were");
                    return true;
                }
            }

            if (word1 == "you")
            {
                if (word2 == "guys" || word2 == "all" || word2 == "people")
                {
                    replacedWordPair = new Tuple<string, string>("us", string.Empty);
                    return true;
                }
                else if (word2 == "are")
                {
                    replacedWordPair = new Tuple<string, string>("i", "am");
                    return true;
                }
                else if (word2 == "were")
                {
                    replacedWordPair = new Tuple<string, string>("i", "was");
                    return true;
                }
            }

            if (word2 == "you")
            {
                if (FirstSecondPersonInverter.prepositons.Contains(word1))
                {
                    replacedWordPair = new Tuple<string, string>(word1, "me");
                    return true;
                }
            }

            return false;
        }
    }
}
