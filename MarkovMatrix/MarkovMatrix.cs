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
            uint twoCharSet = MatrixMathHelper.CombineChars(fromChar, toChar);
            this.IncrementOccurrence(twoCharSet);
        }

        private void IncrementOccurrence(uint twoCharSet)
        {
            T occurrence;

            if (!this.occurrenceCountMap.TryGetValue(twoCharSet, out occurrence))
            {
                occurrence = GenericNumberHelper.GetValue<T>(0);
            }
            occurrence = GenericNumberHelper.Add<T>(occurrence, 1);

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
    }
}