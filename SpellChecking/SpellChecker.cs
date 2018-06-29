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
        private const int maxCacheSize = 10000;

        private string language;

        private Hunspell hunspell;

        private Dictionary<string, Tuple<string, bool>> wordCorrectionCache = new Dictionary<string, Tuple<string, bool>>();
        #endregion

        #region Constructors
        public SpellChecker(string dictionariesFolder, string language)
        {
            this.language = language;

            this.hunspell = new Hunspell(dictionariesFolder + "/" + language + ".aff", dictionariesFolder + "/" + language + ".dic");
        }
        #endregion

        public string GetCorrectedText(string originalText, string replaceUnmatchedWordWith)
        {
            #warning Add unit tests for replaceUnmatchedWordWith

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
                    bool isMatched;
                    correctedWord = this.GetCorrectedWord(wordOrPunctuationToken, out isMatched);
                    if (!isMatched && replaceUnmatchedWordWith != null)
                    {
                        correctedWord = replaceUnmatchedWordWith;
                    }
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
            bool isMatched;
            return this.GetCorrectedWord(wordOrPunctuationToken, out isMatched);
        }

        public string GetCorrectedWord(string wordOrPunctuationToken, out bool isMatched)
        {
            Tuple<string, bool> correctedWordAndMatchInfo;
            #warning Add unit tests for cache
            string correctedWord;
            if (this.wordCorrectionCache.Count > maxCacheSize)
            {
                this.wordCorrectionCache.Clear();
            }
            if (!this.wordCorrectionCache.TryGetValue(wordOrPunctuationToken, out correctedWordAndMatchInfo))
            {
                correctedWord = this.RenderCorrectedWord(wordOrPunctuationToken, out isMatched);
                correctedWordAndMatchInfo = new Tuple<string, bool>(correctedWord, isMatched);

                this.wordCorrectionCache.Add(wordOrPunctuationToken, correctedWordAndMatchInfo);
            }
            isMatched = correctedWordAndMatchInfo.Item2;
            return correctedWordAndMatchInfo.Item1;
        }

        private string RenderCorrectedWord(string wordOrPunctuationToken, out bool isMatched)
        {
            if (wordOrPunctuationToken.Length <= 1 && StringAnalysis.IsPunctuationOrSpace(wordOrPunctuationToken[0]))
            {
                isMatched = true;
                return wordOrPunctuationToken;
            }

            if (hunspell.Spell(wordOrPunctuationToken))
            {
                isMatched = true;
                return wordOrPunctuationToken;
            }
            else
            {
                List<string> suggestions = hunspell.Suggest(wordOrPunctuationToken);

                if (suggestions.Count > 0)
                {
                    isMatched = true;
                    string identicalWordegardlessPunctuation = StringAnalysis.GetIdenticalWordRegardlessPunctuation(wordOrPunctuationToken, suggestions);

                    if (identicalWordegardlessPunctuation != null)
                    {
                        return identicalWordegardlessPunctuation;
                    }

                    string mostSimilarWord = StringAnalysis.GetMostSimilarWord(wordOrPunctuationToken, suggestions);

                    if (mostSimilarWord != null)
                    {
                        if (string.IsNullOrWhiteSpace(mostSimilarWord))
                        {
                            isMatched = false;
                        }
                        return mostSimilarWord;
                    }

                    return suggestions[0];
                }
            }

            isMatched = false;

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
