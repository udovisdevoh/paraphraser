using LanguageDetection;
using MarkovMatrices;
using ParaphaserBootstrap;
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

        private ILanguageDetector languageDetector;

        private Bootstrap bootstrap;

        public MainWindow()
        {
            this.bootstrap = new Bootstrap();

            this.languageDetector = this.bootstrap.BuildLanguageDetectorByMarkovMatrixBasedOnTextFiles(wordListsFolder);

            InitializeComponent();

            this.languageDetectionBackgroundWorker = new LanguageDetectionBackgroundWorker(languageDetector, this.textBoxDetectedLanguage);
            this.languageDetectionBackgroundWorker.Start();
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            string text = this.textBoxInput.Text;

            text = StringFormatter.RemoveDoubleTabsSpacesAndEnters(text);
            text = StringFormatter.RemoveLigatures(text);
            //text = StringFormatter.RemoveDiacritics(text);
            text = text.ToLowerInvariant();

            this.languageDetectionBackgroundWorker.NotifyText(text);
        }
    }
}
