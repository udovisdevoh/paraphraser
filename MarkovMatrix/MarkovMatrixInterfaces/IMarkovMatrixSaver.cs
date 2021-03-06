﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovMatrices
{
    public interface IMarkovMatrixSaver<TKey, TValue>
    {
        void SaveMatrix(IMarkovMatrix<TKey, TValue> markovMatrix, Stream outputStream);
    }
}
