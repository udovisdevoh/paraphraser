﻿using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paraphrasing
{
    public class EnglishSentenceTypeDetector : SentenceTypeDetector
    {
        private static HashSet<string> interrogativeStartingWordLists = new HashSet<string>() { "ain't", "aint", "am", "any", "anybody", "are",
            "aren't", "can", "can't", "canst", "cant", "could", "couldn't", "did", "didn't", "didst",
            "do", "does", "doesn't", "doest", "d'ya", "d'you", "got", "had", "has", "have", "haven't",
            "how", "how'd", "how's", "how'm", "is", "isn't", "may", "must", "shall", "should", "shouldn't",
            "wanna", "want", "was", "wasn't", "were", "wha'", "wha'cha", "weren't", "wha's", "what",
            "whatcha", "what'll", "what're", "whats", "when", "when's", "where", "where'd", "where're",
            "where've", "which", "who", "who'd", "who'll", "whom", "who's", "whose", "whut", "why", "will",
            "won't", "wont", "would", "wouldn't", "what's" };

        public override SentenceType GetSentenceType(string sentence)
        {
            if (sentence.Contains('?'))
            {
                return SentenceType.Interrogative;
            }

            string[] words = WordExtractor.GetLowerInvariantWords(sentence, '\'');

            if (this.FirstWordInterrogative(words))
            {
                return SentenceType.Interrogative;
            }

            return SentenceType.Affirmative;
        }

        private bool FirstWordInterrogative(string[] words)
        {
            if (words.Length > 0 && EnglishSentenceTypeDetector.interrogativeStartingWordLists.Contains(words[0]))
            {
                return true;
            }
            return false;
        }
    }
}
