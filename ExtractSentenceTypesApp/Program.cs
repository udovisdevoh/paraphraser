using StringManipulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractSentenceTypesApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program.ExtractInterrogativeSentences("./LanguageSamples/lyrics.en.txt", "./en.interrogative.2.txt", 2);
            Program.ExtractInterrogativeSentences("./LanguageSamples/lyrics.fr.txt", "./fr.interrogative.2.txt", 2);
        }

        public static void ExtractInterrogativeSentences(string inputFileName, string outputFileName, int repeatingWords)
        {
            string line = null;
            string previousLine = string.Empty;
            bool previousLineContainsQuestionMark = false;
            bool currentLineContainsQuestionMark = false;
            bool isWroteCurrentBatch = false;

            HashSet<string> ignoreList = new HashSet<string>();

            using (StreamReader streamReader = new StreamReader(inputFileName))
            {
                using (StreamWriter streamWrite = new StreamWriter(outputFileName))
                {
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        line = StringFormatter.FixApostrophe(line);
                        line = StringFormatter.RemovePunctuation(line, '&', '\'', '?');
                        line = StringFormatter.RemoveLigatures(line);
                        line = StringFormatter.RemoveDoubleTabsSpacesAndEnters(line);
                        line = line.Trim();

                        string startWordKey = Program.GetStartWordKey(line, repeatingWords);

                        if (startWordKey is null || ignoreList.Contains(startWordKey))
                        {
                            continue;
                        }

                        currentLineContainsQuestionMark = line.Contains('?');

                        if (isWroteCurrentBatch)
                        {
                            if (!StringAnalysis.ContainsSameFirstWords(line, previousLine, repeatingWords))
                            {
                                isWroteCurrentBatch = false;
                            }
                        }

                        if (!isWroteCurrentBatch)
                        {
                            if (currentLineContainsQuestionMark && previousLineContainsQuestionMark)
                            {
                                if (repeatingWords <= 0 || (!String.IsNullOrWhiteSpace(previousLine) && StringAnalysis.ContainsSameFirstWords(line, previousLine, repeatingWords)))
                                {
                                    streamWrite.WriteLine(line);
                                    ignoreList.Add(startWordKey);
                                    isWroteCurrentBatch = true;
                                }
                            }
                        }

                        previousLineContainsQuestionMark = currentLineContainsQuestionMark;
                        previousLine = line;
                    }
                }
            }
        }

        private static string GetStartWordKey(string line, int wordCount)
        {
            string[] words = WordExtractor.GetLowerInvariantWords(line);
            if (words.Length < wordCount)
            {
                return null;
            }
            StringBuilder stringBuilder = new StringBuilder();
            for (int wordIndex = 0; wordIndex < wordCount;++wordIndex)
            {
                stringBuilder.Append(words[wordIndex]);
                if (wordIndex != wordCount - 1)
                {
                    stringBuilder.Append(' ');
                }
            }
            return stringBuilder.ToString();
        }
    }
}
