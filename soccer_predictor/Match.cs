using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soccer_predictor
{
    internal class Match : IComparable<Match>
    {
        public string Raw;
        public string HomeTeam;
        public string AwayTeam;
        public int HomeScore;
        public int AwayScore;
        public bool IsNeutral;
        public MatchType Event;
        public double EloChange;

        public enum MatchType
        {
            WorldCup,
            WorldCupQual,
            ContCup,
            ContCupQual,
            ConfedCup,
            NationsLeague,
            Other,
            Friendly
        }

        //static List<string> Others = new List<string>();

        public Match(string input)
        {
            Raw = input;
            string[] fields = input.Split(',');
            HomeTeam = fields[1];
            AwayTeam = fields[2];
            HomeScore = int.Parse(fields[3]);
            AwayScore = int.Parse(fields[4]);
            IsNeutral = bool.Parse(fields[8]);
            EloChange = 0;

            string ev = fields[5];
            if(ev == "FIFA World Cup")
            {
                Event = MatchType.WorldCup;
            }
            else if (ev == "FIFA World Cup qualification")
            {
                Event = MatchType.WorldCupQual;
            }
            else if (ev == "Friendly")
            {
                Event = MatchType.Friendly;
            }
            else if (ev == "UEFA Euro" ||
                ev == "African Cup of Nations" ||
                ev == "CONCACAF Championship" ||
                ev == "Copa América" ||
                ev == "Gold Cup" ||
                ev == "AFC Asian Cup")
            {
                Event = MatchType.ContCup;
            }
            else if (ev == "UEFA Euro qualification" ||
                ev == "African Cup of Nations qualification" ||
                ev == "AFC Asian Cup qualification" ||
                ev == "CONCACAF Championship qualification" ||
                ev == "Gold Cup qualification" ||
                ev == "Copa América qualification" )
            {
                Event = MatchType.ContCupQual;
            }
            else if (ev == "UEFA Nations League" ||
                ev == "CONCACAF Nations League" ||
                ev == "Oceania Nations Cup qualification" ||
                ev == "African Nations Championship qualification" ||
                ev == "CONCACAF Nations League qualification" ||
                ev == "African Nations Championship")
            {
                Event = MatchType.NationsLeague;
            }
            else if (ev == "Confederations Cup")
            {
                Event = MatchType.ConfedCup;
            }
            else
            {
                /*string? found = Others.Find(x => x == ev);
                if(found == null)
                {
                    Others.Add(ev);
                    Console.WriteLine(ev);
                }*/
                Event = MatchType.Other;
            }
        }

        public int CompareTo(Match other)
        {
            return -1 * EloChange.CompareTo(other.EloChange);
        }
    }
}
