using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soccer_predictor
{
    class Algorithms
    {
        public static int Alphabetical(Team home, Team away, bool isNeutral)
        {
            if (home.Name[0] == away.Name[0])
            {
                return 0;
            }
            else if (home.Name[0] < away.Name[0])
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public static int WinRating(Team home, Team away, bool isNeutral)
        {
            return home.WinRating.CompareTo(away.WinRating);
        }

        public static int EloRating(Team home, Team away, bool isNeutral)
        {
            double homeRating = home.EloRating;
            double awayRating = away.EloRating;
            if(isNeutral == false)
            {
                homeRating += Simulation.HOME_ADV;
            }

            if(homeRating > awayRating + Simulation.DRAW_DIST)
            {
                return 1;
            }
            else if(homeRating + Simulation.DRAW_DIST < awayRating)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
