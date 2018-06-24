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
        private ILanguageDetector languageDetector;

        public MainWindow()
        {
            const string matricesDirectory = "./Matrices/";
            Bootstrap bootstrap = new Bootstrap();
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

            text = StringFormatter.FormatInputText(StringFormatter.RemovePunctuation(text.ToLowerInvariant()));

            string detectedLanguage = this.languageDetector.DetectLanguage(text);

            this.textBoxDetectedLanguage.Text = detectedLanguage;
        }
    }
}
