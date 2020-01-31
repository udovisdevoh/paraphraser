using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonologicalTransformations
{
    public class LetterDistanceEvaluator : ILetterDistanceEvaluator
    {
        #region Constants
        private const int cardinality = 512;

        private const int nonLetterDistance = 1000;

        private const int defaultLongestDistanceValue = 10;

        private const int almostIdenticalDistance = 1;

        private const int diacriticsChangeDistance = 2;

        private const int differentVoicingDistance = 2;

        private const int ethnicalVariance = 3;

        private const int differentVoicingMissingLetterDistance = 3;

        private const int closelyRelatedVowelDistance = 2;
        #endregion

        #region Members
        private int[,] letterDistances;
        #endregion

        #region Constructors
        public LetterDistanceEvaluator()
        {
            this.letterDistances = new int[cardinality, cardinality];

            for (int from = 0; from < cardinality; ++from)
            {
                for (int to = 0; to < cardinality; ++to)
                {
                    this.letterDistances[from, to] = nonLetterDistance;
                    this.letterDistances[to, from] = nonLetterDistance;
                }
            }

            for (int from = 0; from < cardinality; ++from)
            {
                for (int to = 0; to < cardinality; ++to)
                {
                    if (Char.IsLetter((char)from) && Char.IsLetter((char)to))
                    {
                        if (Char.ToLower((char)from) == Char.ToLower((char)to))
                        {
                            this.letterDistances[from, to] = 0;
                        }
                        else
                        {
                            this.SetCustomDistance((char)from, (char)to, defaultLongestDistanceValue);
                        }
                    }
                }
            }

            this.SetCustomDistance('C', 'K', almostIdenticalDistance);
            this.SetCustomDistance('C', 'Q', almostIdenticalDistance);
            this.SetCustomDistance('K', 'Q', almostIdenticalDistance);
            this.SetCustomDistance('V', 'W', almostIdenticalDistance);
            this.SetCustomDistance('I', 'Y', almostIdenticalDistance);
            this.SetCustomDistance('U', 'W', almostIdenticalDistance);

            this.SetCustomDistance('B', 'P', differentVoicingDistance);
            this.SetCustomDistance('C', 'G', differentVoicingDistance);
            this.SetCustomDistance('K', 'G', differentVoicingDistance);
            this.SetCustomDistance('Q', 'G', differentVoicingDistance);
            this.SetCustomDistance('D', 'T', differentVoicingDistance);
            this.SetCustomDistance('F', 'V', differentVoicingDistance);
            this.SetCustomDistance('S', 'Z', differentVoicingDistance);

            this.SetCustomDistance('J', 'H', differentVoicingMissingLetterDistance);
            this.SetCustomDistance('G', 'H', differentVoicingMissingLetterDistance);
            this.SetCustomDistance('J', 'C', differentVoicingMissingLetterDistance);
            this.SetCustomDistance('G', 'C', differentVoicingMissingLetterDistance);

            this.SetCustomDistance('L', 'R', ethnicalVariance);
            this.SetCustomDistance('J', 'Y', ethnicalVariance);
            this.SetCustomDistance('J', 'I', ethnicalVariance);

            this.SetCustomDistance('A', 'O', closelyRelatedVowelDistance);
            this.SetCustomDistance('A', 'E', closelyRelatedVowelDistance);
            this.SetCustomDistance('O', 'U', closelyRelatedVowelDistance);
            this.SetCustomDistance('U', 'I', closelyRelatedVowelDistance);
            this.SetCustomDistance('E', 'I', closelyRelatedVowelDistance);

            /*
            for (int characterIndex = 0; characterIndex < cardinality;++characterIndex)
            {
                char character = (char)characterIndex;
                char characterWithoutDiacritics = StringFormatter.RemoveDiacritics(character);

                if (character != characterWithoutDiacritics && (Char.IsLetter(characterWithoutDiacritics) || Char.IsLetter(character)))
                {
                    this.SetCustomDistance(character, characterWithoutDiacritics, diacriticsChangeDistance);
                }
            }
            */
            for (int characterIndex1 = 0; characterIndex1 < cardinality; ++characterIndex1)
            {
                char character1 = (char)characterIndex1;
                char character1WithoutDiacritics = StringFormatter.RemoveDiacritics(character1);
                if (Char.IsLetter(character1WithoutDiacritics))
                {
                    for (int characterIndex2 = 0; characterIndex2 < cardinality; ++characterIndex2)
                    {
                        char character2 = (char)characterIndex2;

                        if (character1 != character2)
                        {
                            char character2WithoutDiacritics = StringFormatter.RemoveDiacritics(character2);
                            if (character1WithoutDiacritics == character2WithoutDiacritics)
                            {
                                if (Char.IsLetter(character2WithoutDiacritics))
                                {
                                    this.SetCustomDistance(character1, character2, diacriticsChangeDistance);
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        public int GetDistance(char letter1, char letter2)
        {
            letter1 = Char.ToLower(letter1);
            letter2 = Char.ToLower(letter2);

            if (letter1 == letter2)
            {
                return 0;
            }

            int index1 = (int)letter1;
            int index2 = (int)letter2;

            if (index1 < 0 || letter2 < 0 || letter1 >= cardinality || letter2 >= cardinality)
            {
                return defaultLongestDistanceValue;
            }

            return this.letterDistances[letter1, letter2];
        }

        private void SetCustomDistance(char letter1, char letter2, int distance)
        {
            letter1 = Char.ToLower(letter1);
            letter2 = Char.ToLower(letter2);

            if (letter1 < cardinality && letter2 < cardinality)
            {
                this.letterDistances[letter1, letter2] = distance;
                this.letterDistances[letter2, letter1] = distance;
            }

            char letter1Upper = Char.ToUpper(letter1);
            char letter2Upper = Char.ToUpper(letter2);

            if (letter1Upper < cardinality && letter2 < cardinality)
            {
                this.letterDistances[letter1Upper, letter2] = distance;
            }

            if (letter2Upper < cardinality && letter1 < cardinality)
            {
                this.letterDistances[letter2Upper, letter1] = distance;
            }
        }

        public IEnumerable<char> GetReplacementLetters(char sourceLetter)
        {
            int sourceLetterIndex = (int)sourceLetter;
            for (int index = 0; index < cardinality;++index)
            {
                char destinationLetter = (char)index;
                int distance = this.letterDistances[sourceLetterIndex, index];
                if (distance != nonLetterDistance && sourceLetter != destinationLetter)
                {
                    yield return destinationLetter;
                }
            }
        }
    }
}
