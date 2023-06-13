using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace soccer_predictor
{
    internal class Parser
    {
        public List<Match> mMatches = new List<Match>();
        public void Parse()
        {
            string contents = File.ReadAllText("..\\..\\..\\results.csv");
            string[] lines = contents.Split('\n');

            //Parse all the matches
            for(int i = 1; i < lines.Length; i++)
            {
                if (lines[i].Length < 5)
                {
                    continue;
                }
                mMatches.Add(new Match(lines[i]));
            }

            int maxHomeScore = 0;
            for( int i = 0; i < mMatches.Count; i++) 
            {
                if (mMatches[i].HomeScore > maxHomeScore) 
                {
                    maxHomeScore = mMatches[i].HomeScore;
                }
            }

            for (int i = 0; i < mMatches.Count; i++)
            {
                if (mMatches[i].HomeScore == maxHomeScore)
                {
                    Console.WriteLine(mMatches[i].Raw);
                }
            }

        }
    }
}
