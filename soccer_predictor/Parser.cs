using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soccer_predictor
{
    internal class Parser
    {
        public void Parse()
        {
            string contents = File.ReadAllText("..\\..\\..\\results.csv");
            string[] lines = contents.Split('\n');
            for(int i = 1; i < lines.Length; i++)
            {
                if (lines[i].Length < 5)
                {
                    continue;
                }
                Match match = new Match(lines[i]);
                Console.WriteLine(string.Format("{0} {1} - {2} {3}", match.HomeTeam, match.HomeScore, match.AwayTeam, match.AwayScore));
            }
        }
    }
}
