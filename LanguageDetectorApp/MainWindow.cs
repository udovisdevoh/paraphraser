using LanguageDetection;
using MarkovMatrices;
using ParaphaserBootstrap;
using SpellChecking;
using StringManipulation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LanguageDetectorApp
{
    public partial class MainWindow : Form
    {
        private const string matricesDirectory = "./TextMatrices/";

        private const string spellCheckDictionaries = "./SpellCheckDictionaries/";

        private ICompositeLanguageDetector languageDetector;

        private Bootstrap bootstrap;

        public MainWindow()
        {
            this.bootstrap = new Bootstrap();

            ILanguageDetector languageDetectorByMarkovMatrix = this.bootstrap.BuildLanguageDetectorByMarkovMatrix(matricesDirectory);
            ILanguageDetector languageDetectorByDictionary = this.bootstrap.BuildLanguageDetectorByDictionary(spellCheckDictionaries);

            this.languageDetector = this.bootstrap.BuildCompositeLanguageDetector();
            this.languageDetector.AddLanguageDetector(languageDetectorByMarkovMatrix);
            this.languageDetector.AddLanguageDetector(languageDetectorByDictionary);

            InitializeComponent();
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            string text = this.textBoxInput.Text;

            text = text.ToLowerInvariant();
            text = StringFormatter.RemoveDoubleTabsSpacesAndEnters(text);
            text = StringFormatter.RemoveLigatures(text);
            //text = StringFormatter.RemovePunctuation(text);
            //text = StringFormatter.FormatInputText(text);

            KeyValuePair<string, double>[] languageProximities = this.languageDetector.GetLanguageProximities(text);

            StringBuilder languageProximitiesStringBuilder = new StringBuilder();

            foreach (KeyValuePair<string, double> languageProximity in languageProximities)
            {
                string languageName = languageProximity.Key;
                double proximity = languageProximity.Value;
                string formattedProximity = proximity.ToString("N2");
                languageProximitiesStringBuilder.AppendLine(string.Format("{0}: {1}", languageName, formattedProximity));
            }

            //string detectedLanguage = this.languageDetector.DetectLanguage(text);

            this.textBoxDetectedLanguage.Text = languageProximitiesStringBuilder.ToString();
        }
    }
}
