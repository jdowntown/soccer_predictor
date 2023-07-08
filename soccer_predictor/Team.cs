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
        public double AtkElo;
        public double DefElo;


        public Team(string name) 
        {
            Name = name;
            AtkElo = 1000;
            DefElo = 1000;
        }

        public int CompareTo(Team other)
        {
            if(AtkElo + DefElo > other.AtkElo + other.DefElo)
            {
                return -1;
            }
            else if (AtkElo + DefElo == other.AtkElo + other.DefElo)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
