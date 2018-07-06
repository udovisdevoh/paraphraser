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
        #region Members
        private static Dictionary<string, string> wordsToCorrect;

        private static Dictionary<string, string> oldEnglishWords;

        private static HashSet<string> interrogativeStartingWordListsToSwapWithNextWord;

        private static HashSet<string> interrogativeWordsToRemove;

        private static HashSet<string> interrogativeFirstWordsToRemove;

        private static Dictionary<string, string> interrogativeFirstWordsToReplace;

        private static Dictionary<string, string> firstWordsToReplaceInterrogativeToAffirmative;

        private IWordOrderSwapper wordOrderSwapper;
        #endregion

        #region Constructors
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
                { "wha'cha", "what are you" },
                { "whacha", "what are you" },
                { "whatcha", "what are you" },
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
                { "u","you" },
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
                { "anybody", "somebody" }
            };

            EnglishInterrogativeToAffirmative.interrogativeWordsToRemove = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
                "ever", "wanna", "how", "why" };

            EnglishInterrogativeToAffirmative.interrogativeFirstWordsToReplace = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {  "want", "do you want" },
                {  "how's", "how is" }
            };

            EnglishInterrogativeToAffirmative.interrogativeFirstWordsToRemove = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
                "what", "where", "who", "whom" };

            EnglishInterrogativeToAffirmative.interrogativeStartingWordListsToSwapWithNextWord = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
                "ain't", "aint", "am", "are",
                "aren't", "can", "can't", "canst", "cant", "could", "couldn't", "did", "didn't", "didst",
                "do", "does", "doesn't", "doest", "d'ya", "d'you", "got", "had", "has", "have", "haven't",
                "how", "how'd", "how's", "how'm", "is", "isn't", "may", "must", "shall", "should", "shouldn't",
                "wanna", "want", "was", "wasn't", "were", "wha'", "wha'cha", "weren't", "wha's", "what",
                "whatcha", "what'll", "what're", "whats", "when", "when's", "where", "where'd", "where're",
                "where've", "which", "who", "who'd", "who'll", "whom", "who's", "whose", "whut", "why", "will",
                "won't", "wont", "would", "wouldn't", "what's" };
        }
        
        public EnglishInterrogativeToAffirmative(IWordOrderSwapper wordOrderSwapper)
        {
            this.wordOrderSwapper = wordOrderSwapper;
        }
        #endregion

        public string Convert(string text)
        {
            text = StringFormatter.FixApostrophe(text);
            bool originallyEndsWithQuestionMark = text.Trim().EndsWith("?");
            text = StringFormatter.RemovePunctuation(text, '&', '\'', ',');
            text = StringFormatter.RemoveLigatures(text);
            text = StringFormatter.ReplaceWords(text, EnglishInterrogativeToAffirmative.oldEnglishWords);
            text = StringFormatter.ReplaceWords(text, EnglishInterrogativeToAffirmative.wordsToCorrect);

            text = StringFormatter.ReplaceWords(text, EnglishInterrogativeToAffirmative.interrogativeFirstWordsToReplace, 0, 0);

            text = StringFormatter.RemoveWords(text, EnglishInterrogativeToAffirmative.interrogativeWordsToRemove);
            text = StringFormatter.RemoveWords(text, EnglishInterrogativeToAffirmative.interrogativeFirstWordsToRemove, 0);
            text = text.Replace(" ,", ",");
            text = StringFormatter.RemoveDoubleTabsSpacesAndEnters(text);

            text = this.wordOrderSwapper.SwapWordOrder(text, interrogativeStartingWordListsToSwapWithNextWord, 1);

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
