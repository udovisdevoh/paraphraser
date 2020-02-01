using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    [Serializable]
    [DebuggerDisplay("{currentText} (Score: {currentLanguageDetectionScore})")]
    public class LanguageDetectionState
    {
        #region Members
        private string currentText;

        private double currentLanguageDetectionScore;
        #endregion

        #region Constructors
        public LanguageDetectionState(string currentText, double currentLanguageDetectionScore)
        {
            this.currentText = currentText;
            this.currentLanguageDetectionScore = currentLanguageDetectionScore;
        }
        #endregion

        #region Properties
        public string CurrentText
        {
            get { return this.currentText; }
        }

        public double CurrentLanguageDetectionScore
        {
            get { return this.currentLanguageDetectionScore; }
        }
        #endregion
    }
}
