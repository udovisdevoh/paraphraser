using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageDetection
{
    public abstract class LanguageDetector : ILanguageDetector
    {
        public abstract IEnumerable<string> GetLanguageList();

        public abstract KeyValuePair<string, double>[] GetLanguageProximities(string text);

        public virtual string DetectLanguage(string text)
        {
            return this.GetLanguageProximities(text)[0].Key;
        }

        public double GetLanguageDetectionScore(string text, string targetLanguage)
        {
            List<KeyValuePair<string, double>> proximities = this.GetLanguageProximities(text).ToList();

            //double sum = proximities.Select(keyValuePair => keyValuePair.Value).Sum();
            double targetLanguageProximiy = proximities.Where(keyValuePair => keyValuePair.Key == targetLanguage).Select(keyValuePair => keyValuePair.Value).First();

            double detectedLanguageProximity = proximities[0].Value;

            double score = targetLanguageProximiy - detectedLanguageProximity;

            if (score >= 0)
            {
                foreach (KeyValuePair<string, double> otherLanguageProximity in proximities)
                {
                    double otherLanguageProximityValue = otherLanguageProximity.Value;

                    if (otherLanguageProximityValue != targetLanguageProximiy)
                    {
                        double difference = targetLanguageProximiy - otherLanguageProximityValue;

                        score += difference;
                    }
                }
            }
            return score;
        }
    }
}
