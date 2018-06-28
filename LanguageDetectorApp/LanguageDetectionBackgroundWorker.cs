using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LanguageDetection;

namespace LanguageDetectorApp
{
    internal class LanguageDetectionBackgroundWorker
    {
        private ILanguageDetector languageDetector;

        private TextBox textBox;

        private object currentTextChangeLock = new object();

        private object isNeedRecalculationLock = new object();

        private string currentText = null;

        private bool isNeedRecalculation = false;

        public LanguageDetectionBackgroundWorker(ICompositeLanguageDetector languageDetector, TextBox textBox)
        {
            this.languageDetector = languageDetector;
            this.textBox = textBox;
        }

        public void Start()
        {
            Thread thread = new Thread(LanguageDectionWorker);
            thread.IsBackground = true;
            thread.Start();
        }

        private void LanguageDectionWorker()
        {
            while (true)
            {
                bool isRecalculate;
                lock (this.isNeedRecalculationLock)
                {
                    isRecalculate = this.isNeedRecalculation;
                }
                if (isRecalculate)
                {
                    this.Recalculate();
                }
                lock (this.isNeedRecalculationLock)
                {
                    this.isNeedRecalculation = false;
                }
                Thread.Sleep(16);
            }
        }

        public void NotifyText(string newText)
        {
            lock (this.currentTextChangeLock)
            {
                if (newText != this.currentText)
                {
                    this.currentText = newText;
                    this.NotifyTextChange();
                }
            }
        }

        private void NotifyTextChange()
        {
            lock (this.isNeedRecalculationLock)
            {
                this.isNeedRecalculation = true;
            }
        }

        private void Recalculate()
        {
            KeyValuePair<string, double>[] languageProximities = this.languageDetector.GetLanguageProximities(this.currentText);

            StringBuilder languageProximitiesStringBuilder = new StringBuilder();

            foreach (KeyValuePair<string, double> languageProximity in languageProximities)
            {
                string languageName = languageProximity.Key;
                double proximity = languageProximity.Value;
                string formattedProximity = proximity.ToString("N2");
                languageProximitiesStringBuilder.AppendLine(string.Format("{0}: {1}", languageName, formattedProximity));
            }

            //string detectedLanguage = this.languageDetector.DetectLanguage(text);
            this.textBox.BeginInvoke((Action)(() =>
            {
                this.textBox.Text = languageProximitiesStringBuilder.ToString();
            }));
        }
    }
}
