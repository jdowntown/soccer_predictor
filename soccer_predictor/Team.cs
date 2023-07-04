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
        public double WinRating;

        public Team(string name) 
        {
            Name = name;
            WinRating = 1000;
        }
    }
}
