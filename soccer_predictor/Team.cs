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
        public double WinRating;
        public double EloRating;
        public double EloMax;
        public string EloMaxDate;

        public Team(string name) 
        {
            Name = name;
            WinRating = 1000;
            EloRating = 1000;
            EloMax = 0;
        }

        public int CompareTo(Team other)
        {
            //return -1 * EloRating.CompareTo(other.EloRating);
            return -1 * EloMax.CompareTo(other.EloMax);
        }
    }
}
