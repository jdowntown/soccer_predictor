using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soccer_predictor
{
    internal class Match
    {
        public string Raw;
        public string HomeTeam;
        public string AwayTeam;
        public int HomeScore;
        public int AwayScore;
        
        public Match(string input)
        {
            Raw = input;
            string[] fields = input.Split(',');
            HomeTeam = fields[1];
            AwayTeam = fields[2];
            HomeScore = int.Parse(fields[3]);
            AwayScore = int.Parse(fields[4]);
        }
    }
}
