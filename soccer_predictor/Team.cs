using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soccer_predictor
{
    internal class Team
    {
        public string Name;
        public int Wins;
        public int Losses;
        public int Ties;

        public Team(string name) 
        {
            Name = name;
        }
    }
}
