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
        public string Convert(string text)
        {
            string[] words = WordExtractor.GetWordsAndPunctuationTokens(text.ToLowerInvariant(), '\'');

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
                        continue;
                    }
                }

                words[index] = this.TryReplaceWord(words[index]);
            }

            #warning Implement
            #warning Add unit tests
            throw new NotImplementedException();
        }

        public string TryReplaceWord(string word)
        {
            #warning Implement
            #warning Add unit tests

            throw new NotImplementedException();
        }

        public bool TryReplaceWordPair(string word1, string word2, out Tuple<string, string> replacedWordPair)
        {
            #warning Implement
            #warning Add unit tests

            replacedWordPair = null;
            if (word1 == "i")
            {
                if (word2 == "am")
                {
                    replacedWordPair = new Tuple<string, string>("you", "are");
                    return true;
                }
            }
            return false;
        }
    }
}
