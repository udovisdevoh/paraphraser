﻿using ParaphraserMath;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public class CharMarkovMatrix<TValue> : MarkovMatrix<char, TValue>
    {
        #region Constructors
        public CharMarkovMatrix() : base()
        {
        }
        #endregion

        public override uint CombineElements(char fromElement, char toElement)
        {
            return MatrixMathHelper.CombineChars(fromElement, toElement);
        }

        public override Tuple<char, char> SplitElements(uint key)
        {
            return MatrixMathHelper.SplitChars(key);
        }
    }
}