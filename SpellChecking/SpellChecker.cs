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
        public SpellChecker(string language, string dictionariesFolder)
        {
            this.language = language;

            this.hunspell = new Hunspell(dictionariesFolder + "/" + language + ".aff", "Dictionaries/" + language + ".dic");
        }
        #endregion

        public string GetCorrectedText(string originalText)
        {
            #warning Implement
            #warning Add unit tests

            string[] words = WordExtractor.GetWords(originalText);

            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this.hunspell.Dispose();
        }
    }
}
