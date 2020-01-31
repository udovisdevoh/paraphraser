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
        private const int cardinality = 256;

        private const int defaultLongestDistanceValue = 10;

        private const int almostIdenticalDistance = 1;

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
                    this.letterDistances[from, to] = defaultLongestDistanceValue;
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
        }
        #endregion

        public int GetDistance(char letter1, char letter2)
        {
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
            this.letterDistances[letter1, letter2] = distance;
            this.letterDistances[letter2, letter1] = distance;
        }
    }
}
