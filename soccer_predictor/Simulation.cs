using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace soccer_predictor
{
    internal class Simulation
    {
        public delegate double[][] PredictorFunction(Team home, Team away, bool isNeutral);

        //0	0.299	3.3444816054
        //1	0.322	3.1055900621
        //2	0.203	4.9261083744
        //3	0.095	10.5263157895
        //4	0.043	23.2558139535
        //5	0.037	27.027027027
        //Expected value: 1.37
        public static double[] POINTS = { 3.3444816054, 3.1055900621, 4.9261083744, 10.5263157895, 23.2558139535, 27.027027027 };
        public static double HOME_ADV = 100.50;
        public static double DRAW_DIST = 30.25;
        public static double RECENCY = 40.06;
        public static double K_WC = 1.444;
        public static double K_WCQ = 0.999;
        public static double K_CC = 1.255;
        public static double K_CCQ = 0.996;
        public static double K_CFC = 1.374;
        public static double K_NL = 0.981;
        public static double K_OT = 0.860;
        public static double K_F = 0.661;
        public static double STDEV = 1.0;

        public static void TestParam(string name, ref double param, double min, double max, double step, List<Match> matches)
        {
            double bestScore = 0.0;
            double bestVal = min;
            double startVal = param;
            for (double val = min; val <= max; val += step)
            {
                param = val;
                double score = Simulation.Run(matches, Algorithms.GoalRating);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestVal = val;
                }
                Console.WriteLine("Score:" + score + " " + name + ": " + val);
            }
            Console.WriteLine("***Score:" + bestScore + " Best " + name + ": "  + bestVal);
            param = startVal;
        }

        public static void ProcessMatchResults(Match match, Team home, Team away)
        {
            double homeAtk = home.AtkElo;
            double homeDef = home.DefElo;
            double awayAtk = away.AtkElo;
            double awayDef = away.DefElo;

            if (!match.IsNeutral)
            {
                homeAtk += HOME_ADV;
                homeDef += HOME_ADV;
            }

            double homeAtkDiff = RatingsDiff(homeAtk, awayDef, match.HomeScore, match);
            double awayAtkDiff = RatingsDiff(awayAtk, homeDef, match.AwayScore, match);
            home.AtkElo += homeAtkDiff;
            away.DefElo -= homeAtkDiff;
            away.AtkElo += awayAtkDiff;
            home.DefElo -= awayAtkDiff;
        }

        public static double RatingsDiff(double eloAtk, double eloDef, int actual, Match match)
        {
            //Update elo rating
            double[] pred = Algorithms.GoalPred(eloAtk, eloDef);

            double expGoals = pred[1] + 2 * pred[2] + 3 * pred[3] + 4 * pred[4] + 5 * pred[5];

            double mi = RECENCY;
            switch(match.Event)
            {
                case Match.MatchType.WorldCup:
                    mi *= K_WC; break;
                case Match.MatchType.WorldCupQual:
                    mi *= K_WCQ; break;
                case Match.MatchType.ContCup:
                    mi *= K_CC; break;
                case Match.MatchType.ContCupQual:
                    mi *= K_CCQ; break;
                case Match.MatchType.ConfedCup:
                    mi *= K_CFC; break;
                case Match.MatchType.Friendly:
                    mi *= K_F; break;
                case Match.MatchType.NationsLeague:
                    mi *= K_NL; break;
                case Match.MatchType.Other:
                    mi *= K_OT; break;
            }

            double delta = mi * (actual - expGoals);

            Console.WriteLine(string.Format("  Atk:{0:0.0} Def:{1:0.0} Exp:{2:0.00} Actual:{3} Delta:{4:0.00}", eloAtk, eloDef, expGoals, actual, delta));
            Console.WriteLine(string.Format("    Goal: 0:{0:0.000} 1:{1:0.000} 2:{2:0.000} 3:{3:0.000} 4:{4:0.000} 5+:{5:0.000}", pred[0], pred[1], pred[2], pred[3], pred[4], pred[5]));

            return delta;
        }

        public static List<Team>? teams;

        public static double Run(List <Match> matches, PredictorFunction predictor)
        {
            double[] WCsum = new double[6];
            int WCcounter = 0;
            double[] CCsum = new double[6];
            int CCcounter = 0;
            double[] Allsum = new double[6];
            int Allcounter = 0;

            teams = new List<Team>();

            for (int i = 0; i < matches.Count; i++)
            {
                //First find or add the teams playing
                Team? homeTeam = teams.Find(x => x.Name == matches[i].HomeTeam);
                if (homeTeam == null)
                {
                    homeTeam = new Team(matches[i].HomeTeam);
                    teams.Add(homeTeam);
                }

                Team? awayTeam = teams.Find(x => x.Name == matches[i].AwayTeam);
                if (awayTeam == null)
                {
                    awayTeam = new Team(matches[i].AwayTeam);
                    teams.Add(awayTeam);
                }

                double[][] pred = predictor(homeTeam, awayTeam, matches[i].IsNeutral);

                Console.WriteLine("Match: " + matches[i].Raw);
                //Console.WriteLine(string.Format("  Home: 0:{0:0.000} 1:{1:0.000} 2:{2:0.000} 3:{3:0.000} 4:{4:0.000} 5+:{5:0.000}", pred[0][0], pred[0][1], pred[0][2], pred[0][3], pred[0][4], pred[0][5]));
                //Console.WriteLine(string.Format("  Away: 0:{0:0.000} 1:{1:0.000} 2:{2:0.000} 3:{3:0.000} 4:{4:0.000} 5+:{5:0.000}", pred[1][0], pred[1][1], pred[1][2], pred[1][3], pred[1][4], pred[1][5]));

                int homeScore = matches[i].HomeScore;
                if(homeScore > 5)
                {
                    homeScore = 5;
                }
                int awayScore = matches[i].AwayScore;
                if (awayScore > 5)
                {
                    awayScore = 5;
                }

                if (matches[i].Event == Match.MatchType.WorldCup)
                {
                    WCsum[homeScore] += pred[0][homeScore] * POINTS[homeScore];
                    WCsum[awayScore] += pred[1][awayScore] * POINTS[awayScore];
                    WCcounter++;
                }
                if (matches[i].Event == Match.MatchType.WorldCup || matches[i].Event == Match.MatchType.ContCup)
                {
                    CCsum[homeScore] += pred[0][homeScore] * POINTS[homeScore];
                    CCsum[awayScore] += pred[1][awayScore] * POINTS[awayScore];
                    CCcounter++;
                }
                Allsum[homeScore] += pred[0][homeScore] * POINTS[homeScore];
                Allsum[awayScore] += pred[1][awayScore] * POINTS[awayScore];
                Allcounter++;

                ProcessMatchResults(matches[i], homeTeam, awayTeam);
            }

            double result = 0;
            for( int i = 0; i < 6; i++)
            {
                double WCval = WCsum[i] / 2.0 / WCcounter;
                double CCval = CCsum[i] / 2.0 / CCcounter;
                double Allval = Allsum[i] / 2.0 / Allcounter;
                double sum = (WCval + CCval + Allval) / 3;
                result += sum;
                //Console.WriteLine(string.Format("Score for {0} goals: {1:0.000}", i, sum));
            }

            return result;
        }
    }
}
