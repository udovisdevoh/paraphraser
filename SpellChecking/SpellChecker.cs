using NHunspell;
using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellChecking
{
    public class SpellChecker : ISpellChecker
    {
        #region Members
        private string language;

        private Hunspell hunspell;
        #endregion

        #region Constructors
        public SpellChecker(string dictionariesFolder, string language)
        {
            this.language = language;

            this.hunspell = new Hunspell(dictionariesFolder + "/" + language + ".aff", dictionariesFolder + "/" + language + ".dic");
        }
        #endregion

        public string GetCorrectedText(string originalText)
        {
            string[] wordsAndPunctuationTokens = WordExtractor.GetWordsAndPunctuationTokens(originalText);

            StringBuilder stringBuilder = new StringBuilder();
            foreach (string wordOrPunctuationToken in wordsAndPunctuationTokens)
            {
                bool isWord = true;
                if (wordOrPunctuationToken.Length == 1)
                {
                    char character = wordOrPunctuationToken[0];

                    if (StringAnalysis.IsPunctuationOrSpace(character))
                    {
                        isWord = false;
                    }
                }

                string correctedWord;

                if (isWord)
                {
                    correctedWord = this.GetCorrectedWord(wordOrPunctuationToken);
                }
                else
                {
                    correctedWord = wordOrPunctuationToken;
                }

                stringBuilder.Append(correctedWord);
            }

            return stringBuilder.ToString();
        }

        public string GetCorrectedWord(string wordOrPunctuationToken)
        {
            if (wordOrPunctuationToken.Length <= 1 && StringAnalysis.IsPunctuationOrSpace(wordOrPunctuationToken[0]))
            {
                return wordOrPunctuationToken;
            }

            if (!hunspell.Spell(wordOrPunctuationToken))
            {
                List<string> suggestions = hunspell.Suggest(wordOrPunctuationToken);

                if (suggestions.Count > 0)
                {
                    string identicalWordegardlessPunctuation = StringAnalysis.GetIdenticalWordRegardlessPunctuation(wordOrPunctuationToken, suggestions);

                    if (identicalWordegardlessPunctuation != null)
                    {
                        return identicalWordegardlessPunctuation;
                    }

                    string mostSimilarWord = StringAnalysis.GetMostSimilarWord(wordOrPunctuationToken, suggestions);

                    if (mostSimilarWord != null)
                    {
                        return mostSimilarWord;
                    }

                    return suggestions[0];
                }
            }

            return wordOrPunctuationToken;
        }

        public void Dispose()
        {
            this.hunspell.Dispose();
        }

        public bool ContainsWord(string word)
        {
            word = word.ToLowerInvariant().Trim();
            string wordWithoutLigatures = StringFormatter.RemoveLigatures(word);

            if (word == wordWithoutLigatures)
            {
                return this.hunspell.Spell(word);
            }
            else
            {
                return this.hunspell.Spell(word) || this.hunspell.Spell(wordWithoutLigatures);
            }
        }

        public int CountExistingWords(string[] words)
        {
            int existingWords = 0;
            foreach (string word in words)
            {
                if (this.ContainsWord(word))
                {
                    ++existingWords;
                }
            }

            return existingWords;
        }
    }
}
