using ParaphraserMath;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class MarkovMatrix<TValue> : IMarkovMatrix<char, TValue>
    {
        #region Members
        private Dictionary<uint, TValue> occurrenceCountMap;

        private Dictionary<char, TValue> sumsFromChars;
        #endregion

        #region Properties
        public int InputCount
        {
            get
            {
                return occurrenceCountMap.Count;
            }
        }
        #endregion

        #region Constructors
        public MarkovMatrix()
        {
            if (!GenericNumberHelper.ValidateNumberType<TValue>())
            {
                throw new ArgumentException(string.Format("Unsupported type {0}. Use numeric types only.", typeof(TValue).FullName));
            }
            this.occurrenceCountMap = new Dictionary<uint, TValue>();
            this.sumsFromChars = new Dictionary<char, TValue>();
        }
        #endregion

        public TValue GetOccurrence(char fromChar, char toChar)
        {
            TValue occurrence;
            uint twoCharSet = MatrixMathHelper.CombineChars(fromChar, toChar);
            if (!this.occurrenceCountMap.TryGetValue(twoCharSet, out occurrence))
            {
                GenericNumberHelper.GetValue<TValue>(0);
            }
            return occurrence;
        }

        public void IncrementOccurrence(char fromChar, char toChar)
        {
            this.IncrementOccurrence(fromChar, toChar, GenericNumberHelper.GetValue<TValue>(1));
        }

        public void IncrementOccurrence(char fromChar, char toChar, TValue valueToAdd)
        {
            uint twoCharSet = MatrixMathHelper.CombineChars(fromChar, toChar);
            this.IncrementSum(fromChar, valueToAdd);
            this.IncrementOccurrence(twoCharSet, valueToAdd);
        }

        private void IncrementSum(char fromChar, TValue valueToAdd)
        {
            TValue sum;

            if (!this.sumsFromChars.TryGetValue(fromChar, out sum))
            {
                sum = GenericNumberHelper.GetValue<TValue>(0);
            }
            sum = GenericNumberHelper.Add<TValue>(sum, valueToAdd);

            this.sumsFromChars[fromChar] = sum;
        }

        private void IncrementOccurrence(uint twoCharSet, TValue valueToAdd)
        {
            TValue occurrence;

            if (!this.occurrenceCountMap.TryGetValue(twoCharSet, out occurrence))
            {
                occurrence = GenericNumberHelper.GetValue<TValue>(0);
            }
            occurrence = GenericNumberHelper.Add<TValue>(occurrence, valueToAdd);

            this.occurrenceCountMap[twoCharSet] = occurrence;
        }

        public IEnumerator<KeyValuePair<Tuple<char, char>, TValue>> GetEnumerator()
        {
            foreach (KeyValuePair<uint, TValue> twoCharsAndCount in this.occurrenceCountMap)
            {
                Tuple<char, char> chars = MatrixMathHelper.SplitChars(twoCharsAndCount.Key);
                yield return new KeyValuePair<Tuple<char, char>, TValue>(chars, twoCharsAndCount.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.occurrenceCountMap.GetEnumerator();
        }

        public TValue GetSum(char fromChar)
        {
            TValue sum;

            if (!this.sumsFromChars.TryGetValue(fromChar, out sum))
            {
                sum = GenericNumberHelper.GetValue<TValue>(0);
            }

            return sum;
        }
    }
}