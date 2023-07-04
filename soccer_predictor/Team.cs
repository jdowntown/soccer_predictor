using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soccer_predictor
{
    internal class Team : IComparable<Team>
    {
        public string Name;
        public int Wins;
        public int Losses;
        public int Ties;
        public int GoalsFor;
        public int GoalsAgainst;

        public Team(string name) 
        {
            Name = name;
        }

        public double WinPercent()
        {
            double score = 1.0 * Wins + 0.5 * Ties;
            double average = score / (Wins + Ties + Losses);
            return average;
        }

        public int CompareTo(Team other) 
        {
            return Name.CompareTo(other.Name);
            //if(Name > other.Name)
            //{
            //    return -1;
            //}
            
            //return 1;
        }
    }
}
