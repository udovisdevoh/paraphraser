﻿using LanguageDetection;
using MarkovMatrices;
using ParaphaserBootstrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LanguageDetectorApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Bootstrap bootstrap = new Bootstrap();

            ILanguageDetector languageDetector = bootstrap.BuildLanguageDetector();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}