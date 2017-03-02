using FuzzyString;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchy
{
    public abstract class BaseParser
    {
        protected readonly List<string> phrases = new List<string>();        

        public abstract void Setup();

        public string Parse(string text)
        {
            text = text.ToLower();
            Tuple<int, string> best = new Tuple<int, string>(int.MaxValue, string.Empty);
            foreach (string s in this.phrases)
            {
                if (text.JaccardDistance(s) < 0.15)
                {
                    Trace.TraceInformation("Found exact match using Jaccard: " + s);
                    return s;
                }

                int dist = text.HammingDistance(s);
                if (dist < best.Item1)
                {
                    best = new Tuple<int, string>(dist, s);
                }
            }

            return best.Item2;           
        }

        public string FuzzyMatch(string text)
        {
            text = text.ToLower();
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            Tuple<double, string> best = new Tuple<double, string>(1, string.Empty);
            foreach (string s in this.phrases)
            {
                string ps = s.Substring(0, text.Length > s.Length ? s.Length : text.Length);
                double dist = text.JaccardDistance(s);
                if (dist < best.Item1)
                {
                    best = new Tuple<double, string>(dist, s);
                }
            }

            return best.Item2;
        }
    }
}
