using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class MarkovMatrix<T> : IMarkovMatrix<T>
    {
        #region Members
        private Dictionary<uint, T> occurenceCountMap;
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

        #region Constructors
        public MarkovMatrix()
        {
            if (!GenericNumberHelper.ValidateNumberType<T>())
            {
                throw new ArgumentException(string.Format("Unsupported type {0}. Use numeric types only.", typeof(T).FullName));
            }
            this.occurenceCountMap = new Dictionary<uint, T>();
        }
        #endregion

        public T GetOccurence(char fromChar, char toChar)
        {
            T occurence;
            uint twoCharSet = this.CombineChars(fromChar, toChar);
            if (!this.occurenceCountMap.TryGetValue(twoCharSet, out occurence))
            {
                GenericNumberHelper.GetValue<T>(0);
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
            T occurence;

            if (!this.occurenceCountMap.TryGetValue(twoCharSet, out occurence))
            {
                occurence = GenericNumberHelper.GetValue<T>(0);
            }
            occurence = GenericNumberHelper.Add<T>(occurence, 1);

            this.occurenceCountMap[twoCharSet] = occurence;
        }
    }
}