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
        public List<Team> mTeams = new List<Team>();

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

            for( int i = 0; i < mMatches.Count; i++) 
            {
                Team? homeTeam = mTeams.Find(x => x.Name == mMatches[i].HomeTeam);
                if(homeTeam == null) 
                {
                    homeTeam = new Team(mMatches[i].HomeTeam);
                    mTeams.Add(homeTeam);
                }

                Team? awayTeam = mTeams.Find(x => x.Name == mMatches[i].AwayTeam);
                if (awayTeam == null)
                {
                    awayTeam = new Team(mMatches[i].AwayTeam);
                    mTeams.Add(awayTeam);
                }

                if (mMatches[i].HomeScore == mMatches[i].AwayScore) 
                {
                    homeTeam.Ties++;
                    awayTeam.Ties++;
                }
                else if (mMatches[i].HomeScore > mMatches[i].AwayScore) 
                {
                    homeTeam.Wins++;
                    awayTeam.Losses++;
                }
                else
                {
                    homeTeam.Losses++;
                    awayTeam.Wins++;
                }

                homeTeam.GoalsFor += mMatches[i].HomeScore;
                homeTeam.GoalsAgainst += mMatches[i].AwayScore;
                awayTeam.GoalsFor += mMatches[i].AwayScore;
                awayTeam.GoalsAgainst += mMatches[i].HomeScore;
            }
            int counter = 0;
            for (int i = 0; i < mMatches.Count; i++)
            {
                if (mMatches[i].Event == "FIFA World Cup")
                {
                    Console.WriteLine(mMatches[i].Raw);
                    counter++;
                }
            }

            Console.WriteLine("Total WC Matches:" + counter);
        }
    }
}
