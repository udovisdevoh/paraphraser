using ParaphraserMath;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public abstract class MarkovMatrix<TKey, TValue> : IMarkovMatrix<TKey, TValue>
    {
        #region Members
        private Dictionary<uint, TValue> occurrenceCountMap;

        private Dictionary<TKey, TValue> sumsFromSource;
        #endregion

        #region Properties
        public int InputCount
        {
            get
            {
                return occurrenceCountMap.Count;
            }
        }

        public abstract Dictionary<TKey, ushort> ValueMap { get; }

        public abstract Dictionary<ushort, TKey> ReverseValueMap { get; }
        #endregion

        #region Constructors
        public MarkovMatrix()
        {
            if (!GenericNumberHelper.ValidateNumberType<TValue>())
            {
                throw new ArgumentException(string.Format("Unsupported type {0}. Use numeric types only.", typeof(TValue).FullName));
            }
            this.occurrenceCountMap = new Dictionary<uint, TValue>();
            this.sumsFromSource = new Dictionary<TKey, TValue>();
        }
        #endregion

        public abstract uint CombineElements(TKey fromElement, TKey toElement);
        
        public abstract Tuple<TKey, TKey> SplitElements(uint key);

        public TValue GetOccurrence(TKey fromElement, TKey toElement)
        {
            TValue occurrence;
            uint twoElementSet = this.CombineElements(fromElement, toElement);
            if (!this.occurrenceCountMap.TryGetValue(twoElementSet, out occurrence))
            {
                GenericNumberHelper.GetValue<TValue>(0);
            }
            return occurrence;
        }

        public void IncrementOccurrence(TKey fromElement, TKey toElement)
        {
            this.IncrementOccurrence(fromElement, toElement, GenericNumberHelper.GetValue<TValue>(1));
        }

        public void IncrementOccurrence(TKey fromElement, TKey toElement, TValue valueToAdd)
        {
            fromElement = this.FormatElement(fromElement);
            toElement = this.FormatElement(toElement);

            uint twoElementSet = this.CombineElements(fromElement, toElement);
            this.IncrementSum(fromElement, valueToAdd);
            this.IncrementOccurrence(twoElementSet, valueToAdd);
        }

        protected virtual TKey FormatElement(TKey element)
        {
            return element;
        }

        private void IncrementSum(TKey fromElement, TValue valueToAdd)
        {
            TValue sum;

            if (!this.sumsFromSource.TryGetValue(fromElement, out sum))
            {
                sum = GenericNumberHelper.GetValue<TValue>(0);
            }
            sum = GenericNumberHelper.Add<TValue>(sum, valueToAdd);

            this.sumsFromSource[fromElement] = sum;
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

        public IEnumerator<KeyValuePair<Tuple<TKey, TKey>, TValue>> GetEnumerator()
        {
            foreach (KeyValuePair<uint, TValue> twoCharsAndCount in this.occurrenceCountMap)
            {
                Tuple<TKey, TKey> elements = this.SplitElements(twoCharsAndCount.Key);
                yield return new KeyValuePair<Tuple<TKey, TKey>, TValue>(elements, twoCharsAndCount.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.occurrenceCountMap.GetEnumerator();
        }

        public TValue GetSum(TKey fromChar)
        {
            TValue sum;

            if (!this.sumsFromSource.TryGetValue(fromChar, out sum))
            {
                sum = GenericNumberHelper.GetValue<TValue>(0);
            }

            return sum;
        }
    }
}
