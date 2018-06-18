using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class MarkovMatrix : IMarkovMatrix
    {
        #region Members
        private Dictionary<uint, uint> occurenceCountMap = new Dictionary<uint, uint>();

        private Dictionary<uint, float> probabilityMap = new Dictionary<uint, float>();
        #endregion

        #region Properties
        public int InputCount
        {
            get
            {
                return occurenceCountMap.Count;
            }
        }
        #endregion
        
        public uint GetOccurence(char fromChar, char toChar)
        {
            uint occurence;
            uint twoCharSet = this.CombineChars(fromChar, toChar);
            if (!this.occurenceCountMap.TryGetValue(twoCharSet, out occurence))
            {
                return 0;
            }
            return occurence;
        }

        public void IncrementOccurence(char fromChar, char toChar)
        {
            uint twoCharSet = this.CombineChars(fromChar, toChar);
            this.IncrementOccurence(twoCharSet);
        }

        public uint CombineChars(char fromChar, char toChar)
        {
            return ((uint)fromChar * 65536) + (uint)toChar;
        }

        private void IncrementOccurence(uint twoCharSet)
        {
            uint occurence;

            if (!this.occurenceCountMap.TryGetValue(twoCharSet, out occurence))
            {
                occurence = 0;
            }
            ++occurence;

            this.occurenceCountMap[twoCharSet] = occurence;
        }
    }
}