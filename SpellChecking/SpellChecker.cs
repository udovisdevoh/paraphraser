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
        private Language language;

        private Hunspell hunspell;
        #endregion

        #region Constructors
        public SpellChecker(Language language)
        {
            this.language = language;

            if (this.language == Language.English)
            {
                this.hunspell = new Hunspell("en.aff", "en.dic");
            }
            else if (this.language == Language.French)
            {
                this.hunspell = new Hunspell("fr.aff", "fr.dic");
            }
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
