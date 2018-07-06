using ParaphraserMath;
using StringManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class StringMarkovMatrix<TValue> : MarkovMatrix<string, TValue>
    {
        #region Members
        private Dictionary<string, uint> valueMap;

        private Dictionary<uint, string> reverseValueMap;
        #endregion

        #region Constructors
        public StringMarkovMatrix() : base()
        {
            this.valueMap = new Dictionary<string, uint>(StringComparer.OrdinalIgnoreCase);
            this.reverseValueMap = new Dictionary<uint, string>();
        }

        #endregion

        #region Properties
        public override Dictionary<string, uint> ValueMap
        {
            get
            {
                return valueMap;
            }
        }

        public override Dictionary<uint, string> ReverseValueMap
        {
            get
            {
                return reverseValueMap;
            }
        }
        #endregion

        protected override string FormatElement(string element)
        {
            return StringFormatter.RemoveDoubleTabsSpacesAndEnters(element).ToLowerInvariant();
        }

        public override ulong CombineElements(string fromWord, string toWord)
        {
            fromWord = FormatElement(fromWord);
            toWord = FormatElement(toWord);

            uint fromWordId = this.GetWordId(fromWord);
            uint toWordId = this.GetWordId(toWord);

            return MatrixMathHelper.CombineUInts(fromWordId, toWordId);
        }

        public override Tuple<string, string> SplitElements(ulong combinedWords)
        {
            Tuple<uint, uint> wordIds = MatrixMathHelper.SplitUInts(combinedWords);

            string fromWord = this.GetWord(wordIds.Item1);
            string toWord = this.GetWord(wordIds.Item2);

            return new Tuple<string, string>(fromWord, toWord);
        }

        public string GetWord(uint wordId)
        {
            lock (this.reverseValueMap)
            {
                lock (this.valueMap)
                {
                    return this.reverseValueMap[wordId];
                }
            }
        }

        public uint GetWordId(string word)
        {
            word = FormatElement(word);

            uint wordId;
            lock (this.reverseValueMap)
            {
                lock (this.valueMap)
                {
                    if (!this.valueMap.TryGetValue(word, out wordId))
                    {
                        wordId = (uint)this.reverseValueMap.Count;
                        this.valueMap.Add(word, wordId);
                        this.reverseValueMap.Add(wordId, word);
                    }
                }
            }
            return wordId;
        }
    }
}
