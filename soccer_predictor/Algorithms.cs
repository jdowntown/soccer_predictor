using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soccer_predictor
{
    class Algorithms
    {
        public static int Alphabetical(Team a, Team b)
        {
            if (a.Name[0] == b.Name[0])
            {
                return 0;
            }
            else if (a.Name[0] < b.Name[0])
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public static int WinRating(Team a, Team b)
        {
            return a.WinRating.CompareTo(b.WinRating);
        }


    }
}
