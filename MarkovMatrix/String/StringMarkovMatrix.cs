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
        private Dictionary<string, short> wordDictionary;
        #endregion

        #region Constructors
        public StringMarkovMatrix(Dictionary<string, short> wordDictionary) : base()
        {
            this.wordDictionary = wordDictionary;
        }

        #endregion

        public override uint CombineElements(string fromElement, string toElement)
        {
            #warning Implement
            #warning Add unit tests

            throw new NotImplementedException();
        }

        public override Tuple<string, string> SplitElements(uint key)
        {
            #warning Implement
            #warning Add unit tests

            throw new NotImplementedException();
        }
    }
}
