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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LanguageDetectorApp
{
    public partial class MainWindow : Form
    {
        private const string matricesFolder = "./TextMatrices/";

        private const string spellCheckFolder = "./SpellCheckDictionaries/";

        private const string wordListsFolder = "./LanguageSamples/";

        private LanguageDetectionBackgroundWorker languageDetectionBackgroundWorker;

        private ICompositeLanguageDetector languageDetector;

        private Bootstrap bootstrap;

        public MainWindow()
        {
            #warning Remove low and/or "useless" languages

            this.bootstrap = new Bootstrap();

            ILanguageDetector languageDetectorByMarkovMatrix = this.bootstrap.BuildLanguageDetectorByMarkovMatrix(matricesFolder);
            //ILanguageDetector languageDetectorByDictionary = this.bootstrap.BuildLanguageDetectorByDictionary(spellCheckFolder);
            //ILanguageDetector languageDetectorByHash = this.bootstrap.BuildLanguageDetectorByHash(wordListsFolder);
            ILanguageDetector languageDetectorByLeastCorrection = this.bootstrap.BuildLanguageDetectorByLeastCorrection(spellCheckFolder);

            this.languageDetector = this.bootstrap.BuildCompositeLanguageDetector();
            this.languageDetector.AddLanguageDetector(languageDetectorByMarkovMatrix);
            //this.languageDetector.AddLanguageDetector(languageDetectorByDictionary);
            //this.languageDetector.AddLanguageDetector(languageDetectorByHash);
            this.languageDetector.AddLanguageDetector(languageDetectorByLeastCorrection);

            InitializeComponent();

            this.languageDetectionBackgroundWorker = new LanguageDetectionBackgroundWorker(languageDetector, this.textBoxDetectedLanguage);
            this.languageDetectionBackgroundWorker.Start();
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            string text = this.textBoxInput.Text;

            text = text.ToLowerInvariant();
            text = StringFormatter.RemoveDoubleTabsSpacesAndEnters(text);
            text = StringFormatter.RemoveLigatures(text);
            //text = StringFormatter.RemovePunctuation(text);
            //text = StringFormatter.FormatInputText(text);

            this.languageDetectionBackgroundWorker.NotifyText(text);
        }
    }
}
