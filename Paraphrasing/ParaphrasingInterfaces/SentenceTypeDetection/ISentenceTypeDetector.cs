﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paraphrasing
{
    public interface ISentenceTypeDetector
    {
        SentenceType GetSentenceType(string sentence);
    }
}
