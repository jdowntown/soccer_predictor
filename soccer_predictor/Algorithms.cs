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

        public static double[][] GoalRating(Team home, Team away, bool isNeutral)
        {
            double homeAtk = home.AtkElo;
            double homeDef = home.DefElo;
            double awayAtk = away.AtkElo;
            double awayDef = away.DefElo;

            if(!isNeutral)
            {
                homeAtk += Simulation.HOME_ADV;
                homeDef += Simulation.HOME_ADV;
            }

            double[][] pred = new double[2][];
            pred[0] = GoalPred(homeAtk, awayDef);
            pred[1] = GoalPred(awayAtk, homeDef);

            return pred;
        }

        public static double[] GoalPred(double atkElo, double defElo)
        {
            double er = 1 / (Math.Pow(10, -1 * (atkElo - defElo) / 400) + 1);
            double expectedVal = 1.37 * (er + 0.5);

            double[] retVal = new double[6];

            double sum = 0.0;

            //Calculate
            for(int i = 0; i < 6; i++)
            {
                retVal[i] = StandardDist(expectedVal, Simulation.STDEV, i);
                sum += retVal[i];
            }

            //Normalize
            for (int i = 0; i < 6; i++)
            {
                retVal[i] /= sum;
            }

            return retVal;
        }

        public static double StandardDist(double mean, double stdev, double val)
        {
            double exp = -1 * Math.Pow(val - mean, 2) / (2 * stdev * stdev);
            double prob = Math.Pow(Math.E, exp) / (stdev * Math.Pow(2 * Math.PI, 0.5));
            return prob;
        }
    }
}
