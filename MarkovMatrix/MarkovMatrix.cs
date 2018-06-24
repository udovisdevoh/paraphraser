using ParaphraserMath;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class MarkovMatrix<T> : IMarkovMatrix<T>
    {
        #region Members
        private Dictionary<uint, T> occurrenceCountMap;

        private Dictionary<char, T> sumsFromChars;
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
            if (!GenericNumberHelper.ValidateNumberType<T>())
            {
                throw new ArgumentException(string.Format("Unsupported type {0}. Use numeric types only.", typeof(T).FullName));
            }
            this.occurrenceCountMap = new Dictionary<uint, T>();
            this.sumsFromChars = new Dictionary<char, T>();
        }
        #endregion

        public T GetOccurrence(char fromChar, char toChar)
        {
            T occurrence;
            uint twoCharSet = MatrixMathHelper.CombineChars(fromChar, toChar);
            if (!this.occurrenceCountMap.TryGetValue(twoCharSet, out occurrence))
            {
                GenericNumberHelper.GetValue<T>(0);
            }
            return occurrence;
        }

        public void IncrementOccurrence(char fromChar, char toChar)
        {
            this.IncrementOccurrence(fromChar, toChar, GenericNumberHelper.GetValue<T>(1));
        }

        public void IncrementOccurrence(char fromChar, char toChar, T valueToAdd)
        {
            uint twoCharSet = MatrixMathHelper.CombineChars(fromChar, toChar);
            this.IncrementSum(fromChar, valueToAdd);
            this.IncrementOccurrence(twoCharSet, valueToAdd);
        }

        private void IncrementSum(char fromChar, T valueToAdd)
        {
            T sum;

            if (!this.sumsFromChars.TryGetValue(fromChar, out sum))
            {
                sum = GenericNumberHelper.GetValue<T>(0);
            }
            sum = GenericNumberHelper.Add<T>(sum, valueToAdd);

            this.sumsFromChars[fromChar] = sum;
        }

        private void IncrementOccurrence(uint twoCharSet, T valueToAdd)
        {
            T occurrence;

            if (!this.occurrenceCountMap.TryGetValue(twoCharSet, out occurrence))
            {
                occurrence = GenericNumberHelper.GetValue<T>(0);
            }
            occurrence = GenericNumberHelper.Add<T>(occurrence, valueToAdd);

            this.occurrenceCountMap[twoCharSet] = occurrence;
        }

        public IEnumerator<KeyValuePair<Tuple<char, char>, T>> GetEnumerator()
        {
            foreach (KeyValuePair<uint, T> twoCharsAndCount in this.occurrenceCountMap)
            {
                Tuple<char, char> chars = MatrixMathHelper.SplitChars(twoCharsAndCount.Key);
                yield return new KeyValuePair<Tuple<char, char>, T>(chars, twoCharsAndCount.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.occurrenceCountMap.GetEnumerator();
        }

        public T GetSum(char fromChar)
        {
            T sum;

            if (!this.sumsFromChars.TryGetValue(fromChar, out sum))
            {
                sum = GenericNumberHelper.GetValue<T>(0);
            }

            return sum;
        }
    }
}