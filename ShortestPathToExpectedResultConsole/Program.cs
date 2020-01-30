﻿using LanguageDetection;
using ParaphaserBootstrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPathToExpectedResultConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const string textInput = "Text summarization aims to extract essential information from a piece of text and transform it into a concise version. Existing unsupervised abstractive summarization models use recurrent neural networks framework and ignore abundant unlabeled corpora resources. In order to address these issues, we propose TED, a transformer-based unsupervised summarization system with pretraining on large-scale data. We first leverage the lead bias in news articles to pretrain the model on large-scale corpora. Then, we finetune TED on target domains through theme modeling and a denoising autoencoder to enhance the quality of summaries. Notably, TED outperforms all unsupervised abstractive baselines on NYT, CNN/DM and English Gigaword datasets with various document styles. Further analysis shows that the summaries generated by TED are abstractive and containing even higher proportions of novel tokens than those from supervised models.";
            //const string textInput = "Ceci est une poule, je suis une banane. Gros jambon à l'école.";
            //const string textInput = "Ceci est une poule.";
            Bootstrap bootstrap = new Bootstrap();
            ILanguageDetector languageDetector = bootstrap.BuildLanguageDetectorByMarkovMatrix("./TextMatrices/");

            KeyValuePair<string, double>[] languageProximities = languageDetector.GetLanguageProximities(textInput);
            //languageDetector.DetectLanguage("Zarf");
        }
    }
}
