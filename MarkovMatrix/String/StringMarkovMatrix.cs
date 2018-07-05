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
        private Dictionary<string, ushort> wordDictionary;

        private List<string> wordIds;
        #endregion

        #region Constructors
        public StringMarkovMatrix() : base()
        {
            this.wordDictionary = new Dictionary<string, ushort>(StringComparer.OrdinalIgnoreCase);
            this.wordIds = new List<string>();
        }

        #endregion

        #region Properties
        public override Dictionary<string, ushort> ValueMap
        {
            get
            {
                return wordDictionary;
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
            lock (this.wordIds)
            {
                lock (this.wordDictionary)
                {
                    return this.wordIds[wordId];
                }
            }
        }

        public ushort GetWordId(string word)
        {
            word = FormatElement(word);

            ushort wordId;
            lock (this.wordIds)
            {
                lock (this.wordDictionary)
                {
                    if (!this.wordDictionary.TryGetValue(word, out wordId))
                    {
                        if (wordIds.Count >= (int)ushort.MaxValue)
                        {
                            throw new Exception("Matrix cannot have more than {0} words");
                        }

                        wordId = (ushort)this.wordIds.Count;
                        this.wordDictionary.Add(word, wordId);
                        this.wordIds.Add(word);
                    }
                }
            }
            return wordId;
        }
    }
}
