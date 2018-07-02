﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringManipulation
{
    public static class StringFormatter
    {
        public static string FormatLanguageName(string languageName)
        {
            languageName = StringFormatter.FixApostrophe(languageName);
            languageName = StringFormatter.RemoveDoubleTabsSpacesAndEnters(languageName);
            languageName = StringFormatter.UcFirst(languageName);

            return languageName;
        }

        public static string FormatInputText(string text)
        {
            text = StringFormatter.FixApostrophe(text);
            text = StringFormatter.RemovePunctuation(text, '&', '\'');
            text = StringFormatter.RemoveLigatures(text);
            text = StringFormatter.RemoveDoubleTabsSpacesAndEnters(text);
            text = StringFormatter.UcFirst(text);

            return text;
        }

        public static string ReplaceWords(string text, Dictionary<string, string> wordsToReplace)
        {
            #warning Add unit tests
            return StringFormatter.ReplaceWords(text, wordsToReplace, -1, -1);
        }

        public static string ReplaceWords(string text, Dictionary<string, string> wordsToReplace, int firstIndex, int lastIndex)
        {
            #warning Add unit tests

            string[] words = WordExtractor.GetWordsAndPunctuationTokens(text, '\'');

            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < words.Length;++index)
            {
                string word = words[index];
                if (firstIndex == -1 || (index * 2) >= firstIndex)
                {
                    if (lastIndex == -1 || (index * 2) <= lastIndex)
                    {                      
                        string replacedWord;
                        if (wordsToReplace.TryGetValue(word, out replacedWord))
                        {
                            word = replacedWord;
                        }
                    }
                }
                stringBuilder.Append(word);
            }

            return stringBuilder.ToString();
        }

        public static string SplitBefore(string line, char character)
        {
            int indexOfCharacter = line.IndexOf(character);

            if (indexOfCharacter != -1)
            {
                return line.Substring(0, indexOfCharacter).Trim();
            }

            return line;
        }

        public static string RemovePunctuation(string text, params char[] exclusion)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char character in text)
            {
                if (StringAnalysis.IsPunctuationOrSpace(character) && !exclusion.Contains(character))
                {
                    stringBuilder.Append(' ');
                }
                else
                {
                    stringBuilder.Append(character);
                }
            }

            return stringBuilder.ToString();
        }

        public static string SwapWordOrder(string text, HashSet<string> wordsToSwap, int offset, int maxSwapCount)
        {
            #warning Add unit tests

            int swapCount = 0;

            string[] words = WordExtractor.GetWordsAndPunctuationTokens(text, '\'');

            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < words.Length; ++index)
            {
                if (swapCount < maxSwapCount && wordsToSwap.Contains(words[index]) && index < words.Length - (offset * 2))
                {
                    string nextWord = words[index + (offset * 2)];
                    words[index + (offset * 2)] = words[index];
                    words[index] = nextWord;
                    ++swapCount;
                }

                stringBuilder.Append(words[index]);
            }

            return stringBuilder.ToString();
        }

        public static string RemoveWords(string text, HashSet<string> wordsToRemove)
        {
            #warning Add unit tests

            string[] words = WordExtractor.GetWordsAndPunctuationTokens(text, '\'');

            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < words.Length; ++index)
            {
                if (wordsToRemove.Contains(words[index]))
                {
                    words[index] = string.Empty;
                }

                stringBuilder.Append(words[index]);
            }

            return stringBuilder.ToString();
        }

        public static char RemoveDiacritics(char letter)
        {
            return RemoveDiacritics(letter.ToString())[0];
        }

        public static string RemoveDiacritics(string text)
        {
            text = text.Replace('Ș', 'S');
            text = text.Replace('ș', 's');
            text = text.Replace('Ț', 'T');
            text = text.Replace('ț', 't');
            text = text.Replace('ẞ', 'S');
            text = text.Replace('ß', 's');            

            byte[] tempBytes;
            tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(text);
            string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);

            return asciiStr;
        }

        public static string RemoveLigatures(string text)
        {
            text = text.Replace("œ", "oe");
            text = text.Replace("Œ", "OE");
            text = text.Replace("æ", "ae");
            text = text.Replace("Æ", "AE");
            return text;
        }

        public static string FixApostrophe(string text)
        {
            return text.Replace('’', '\'');
        }

        public static string RemoveDoubleTabsSpacesAndEnters(string text)
        {
            text = text.Replace('\r', ' ');
            text = text.Replace('\t', ' ');
            text = text.Replace('\n', ' ');

            while (text.Contains("  "))
            {
                text = text.Replace("  ", " ");
            }

            text = text.Trim();

            return text;
        }

        public static string UcFirst(string text)
        {
            if (text.Length > 0)
            {
                text = text.Substring(0, 1).ToUpperInvariant() + text.Substring(1).ToLowerInvariant();
            }

            return text;
        }
    }
}
