using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paraphrasing
{
    public class EnglishInterrogativeToAffirmative : IEnglishInterrogativeToAffirmative
    {
        private static Dictionary<string, string> wordsToCorrect;

        private static Dictionary<string, string> oldEnglishWords;

        private static HashSet<string> interrogativeStartingWordListsToSwapWithNextWord;

        private static HashSet<string> interrogativeWordsToRemove;

        private static Dictionary<string, string> firstWordsToReplaceInterrogativeToAffirmative;

        static EnglishInterrogativeToAffirmative()
        {
            EnglishInterrogativeToAffirmative.wordsToCorrect = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                //{ "ain't", "isn't" },
                { "aint", "ain't" },
                { "cant", "can't" },
                { "d'ya", "do you" },
                { "d'you", "do you" },
                { "how'd", "how did" },
                { "how'm", "how am" },
                { "how's", "how is" },
                { "wha'", "what" },
                { "wha'cha", "what do you" },
                { "whacha", "what do you" },
                { "wha's", "what is" },
                { "what's", "what is" },
                { "whats", "what is" },
                { "what're", "what are" },
                { "what'll", "what will" },
                { "when's", "when is" },
                { "where'd", "where did" },
                { "where're", "where are" },
                { "where've", "where have" },
                { "who'd", "who would" },
                { "who'll", "who will" },
                { "who's", "who is" },
                { "whut", "what" },
                { "wont", "won't" }
            };

            EnglishInterrogativeToAffirmative.oldEnglishWords = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "y'all","you" },
                { "thou","you" },
                { "thy","your" },
                { "thine","your" },
                { "thee","you" },
                { "ye","you" },
                { "thyself","yourself" },
                { "canst", "can't" },
                { "didst", "did" },
                { "shall", "will" },
                { "shan't", "will not" },
                { "doest", "do" }
            };

            EnglishInterrogativeToAffirmative.firstWordsToReplaceInterrogativeToAffirmative = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "any", "there are" },
                { "anybody", "some people" }
            };

            EnglishInterrogativeToAffirmative.interrogativeWordsToRemove = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "ever", "wanna" };

            EnglishInterrogativeToAffirmative.interrogativeStartingWordListsToSwapWithNextWord = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "ain't", "aint", "am", "are",
                "aren't", "can", "can't", "canst", "cant", "could", "couldn't", "did", "didn't", "didst",
                "do", "does", "doesn't", "doest", "d'ya", "d'you", "got", "had", "has", "have", "haven't",
                "how", "how'd", "how's", "how'm", "is", "isn't", "may", "must", "shall", "should", "shouldn't",
                "wanna", "want", "was", "wasn't", "were", "wha'", "wha'cha", "weren't", "wha's", "what",
                "whatcha", "what'll", "what're", "whats", "when", "when's", "where", "where'd", "where're",
                "where've", "which", "who", "who'd", "who'll", "whom", "who's", "whose", "whut", "why", "will",
                "won't", "wont", "would", "wouldn't", "what's" };
        }

        public string Convert(string text)
        {
            text = StringFormatter.FixApostrophe(text);
            bool originallyEndsWithQuestionMark = text.Trim().EndsWith("?");
            text = StringFormatter.RemovePunctuation(text, '&', '\'', ',');
            text = StringFormatter.RemoveLigatures(text);
            text = StringFormatter.RemoveWords(text, EnglishInterrogativeToAffirmative.interrogativeWordsToRemove);
            text = text.Replace(" ,", ",");
            text = StringFormatter.RemoveDoubleTabsSpacesAndEnters(text);

            text = StringFormatter.ReplaceWords(text, EnglishInterrogativeToAffirmative.oldEnglishWords);
            text = StringFormatter.ReplaceWords(text, EnglishInterrogativeToAffirmative.wordsToCorrect);
            text = StringFormatter.SwapWordOrder(text, interrogativeStartingWordListsToSwapWithNextWord, 1, 1);

            text = StringFormatter.ReplaceWords(text, EnglishInterrogativeToAffirmative.firstWordsToReplaceInterrogativeToAffirmative, 0, 0);



            if (originallyEndsWithQuestionMark)
            {
                text += ".";
            }

            text = StringFormatter.UcFirst(text);

            return text;
        }
    }
}
