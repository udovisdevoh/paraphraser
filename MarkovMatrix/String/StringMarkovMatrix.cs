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
        private Dictionary<string, ushort> valueMap;

        private Dictionary<ushort, string> reverseValueMap;
        #endregion

        #region Constructors
        public StringMarkovMatrix() : base()
        {
            this.valueMap = new Dictionary<string, ushort>(StringComparer.OrdinalIgnoreCase);
            this.reverseValueMap = new Dictionary<ushort, string>();
        }

        #endregion

        #region Properties
        public override Dictionary<string, ushort> ValueMap
        {
            get
            {
                return valueMap;
            }
        }

        public override Dictionary<ushort, string> ReverseValueMap
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

        public override uint CombineElements(string fromWord, string toWord)
        {
            fromWord = FormatElement(fromWord);
            toWord = FormatElement(toWord);

            ushort fromWordId = this.GetWordId(fromWord);
            ushort toWordId = this.GetWordId(toWord);

            return MatrixMathHelper.CombineUShorts(fromWordId, toWordId);
        }

        public override Tuple<string, string> SplitElements(uint combinedWords)
        {
            Tuple<ushort, ushort> wordIds = MatrixMathHelper.SplitUShorts(combinedWords);

            string fromWord = this.GetWord(wordIds.Item1);
            string toWord = this.GetWord(wordIds.Item2);

            return new Tuple<string, string>(fromWord, toWord);
        }

        public string GetWord(ushort wordId)
        {
            lock (this.reverseValueMap)
            {
                lock (this.valueMap)
                {
                    return this.reverseValueMap[wordId];
                }
            }
        }

        public ushort GetWordId(string word)
        {
            word = FormatElement(word);

            ushort wordId;
            lock (this.reverseValueMap)
            {
                lock (this.valueMap)
                {
                    if (!this.valueMap.TryGetValue(word, out wordId))
                    {
                        if (reverseValueMap.Count >= (int)ushort.MaxValue)
                        {
                            throw new Exception("Matrix cannot have more than {0} words");
                        }

                        wordId = (ushort)this.reverseValueMap.Count;
                        this.valueMap.Add(word, wordId);
                        this.reverseValueMap.Add(wordId, word);
                    }
                }
            }
            return wordId;
        }
    }
}
