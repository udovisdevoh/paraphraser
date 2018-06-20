﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetection
{
    public interface ILanguageDetector
    {
        string DetectLanguage(string text);
    }
}