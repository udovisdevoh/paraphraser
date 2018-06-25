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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LanguageDetectorApp
{
    public partial class MainWindow : Form
    {
        private LanguageDetector languageDetector;

        public MainWindow()
        {
            const string matricesDirectory = "./TextMatrices/";
            Bootstrap bootstrap = new Bootstrap();
            //this.languageDetector = bootstrap.BuildCompositeLanguageDetector();
            this.languageDetector = bootstrap.BuildLanguageDetector();

            IMarkovMatrixLoader<double> binaryMarkovMatrixLoader = bootstrap.BuildBinaryMarkovMatrixLoader();

            string[] matrixFiles = Directory.EnumerateFiles(matricesDirectory, "*.bin").Select(file => Path.GetFileName(file)).ToArray();

            foreach (string matrixFile in matrixFiles)
            {
                string languageName = StringFormatter.FormatLanguageName(matrixFile.Substring(0, matrixFile.LastIndexOf('.')));

                IMarkovMatrix<double> matrix;
                using (FileStream fileStream = File.Open(matricesDirectory + matrixFile, FileMode.Open))
                {
                    matrix = binaryMarkovMatrixLoader.LoadMatrix(fileStream);
                }

                this.languageDetector.AddLanguage(languageName, matrix);
            }

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
