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
        public static double HOME_ADV = 100;
        public static double DRAW_DIST = 30;
        public static double MOV_2 = 0.5;
        public static double MOV_3 = 0.75;
        public static double MOV_4 = 0.875;
        public static double MOV_5 = 1;
        public static double RECENCY = 40;
        public static double K_WC = 1.5;
        public static double K_CC = 1.25;
        public static double K_CFC = 1.25;
        public static double K_WCQ = 1.0;
        public static double K_CCQ = 1.0;
        public static double K_NL = 1.0;
        public static double K_OT = 0.75;
        public static double K_F = 0.50;

        public delegate int PredictorFunction(Team home, Team away, bool isNeutral);

        public static void TestParam(string name, ref double param, double min, double max, double step, List<Match> matches)
        {
            double bestScore = 0.0;
            double bestVal = min;
            for (double val = min; val <= max; val += step)
            {
                param = val;
                double[] result = Simulation.Run(matches, Algorithms.EloRating);
                double score = result[0] + result[1] + result[2];
                if (score > bestScore)
                {
                    bestScore = score;
                    bestVal = val;
                }
                //Console.WriteLine(name + ": " + val + " Score:" + score);
                Console.WriteLine(name + ": " + val + " WC: " + result[0] + "   CC:" + result[1] + "   All:" + result[2]);

            }
            Console.WriteLine("Best " + name + ": "  + bestVal + " Score:" + bestScore);
            param = bestVal;
        }

        public static void ProcessMatchResults(Match match, Team home, Team away)
        {
            //Update win rating
            if(match.HomeScore > match.AwayScore)
            {
                home.WinRating++;
                away.WinRating--;
            }
            else if (match.HomeScore < match.AwayScore)
            {
                home.WinRating--;
                away.WinRating++;
            }

            //Update elo rating
            double rd = home.EloRating - away.EloRating;
            if(match.IsNeutral == false)
            {
                rd += HOME_ADV;
            }
            double er = 1 / (Math.Pow(10, -1 * rd / 400) + 1);
            double ar = 0.0;
            if(match.HomeScore > match.AwayScore)
            {
                ar = 1.0;
            }
            else if(match.HomeScore == match.AwayScore)
            {
                ar = 0.5;
            }
            double mov = Math.Abs(match.HomeScore - match.AwayScore);
            double af = 1.0;
            if(mov == 2)
            {
                af += MOV_2;
            }
            else if(mov == 3)
            {
                af += MOV_3;
            }
            else if(mov == 4)
            {
                af += MOV_4;
            }
            else if (mov > 4)
            {
                af += MOV_5;
            }

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

            double ratingsChange = mi * (ar - er) * af;
            home.EloRating += ratingsChange;
            away.EloRating -= ratingsChange;
        }

        public static List<Team>? teams;

        public static double[] Run(List <Match> matches, PredictorFunction predictor)
        {
            double WCsum = 0.0;
            int WCcounter = 0;
            double CCsum = 0.0;
            int CCcounter = 0;
            double Allsum = 0.0;
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

                    int prediction = predictor(homeTeam, awayTeam, matches[i].IsNeutral);
                    double score = 0;
                    string predictionText = "";

                    if (prediction == 0)
                    {
                        predictionText = "Draw";

                        if (matches[i].HomeScore == matches[i].AwayScore)
                        {
                            score = 1.0;
                        }
                        else
                        {
                            score = 0.3;
                        }
                    }
                    else if (prediction > 0)
                    {
                        predictionText = matches[i].HomeTeam + " wins";
                        if (matches[i].HomeScore > matches[i].AwayScore)
                        {
                            score = 1.0;
                        }
                        else if (matches[i].HomeScore == matches[i].AwayScore)
                        {
                            score = 0.3;
                        }
                        else
                        {
                            score = 0;
                        }
                    }
                    else
                    {
                        predictionText = matches[i].AwayTeam + " wins";
                        if (matches[i].HomeScore < matches[i].AwayScore)
                        {
                            score = 1.0;
                        }
                        else if (matches[i].HomeScore == matches[i].AwayScore)
                        {
                            score = 0.3;
                        }
                        else
                        {
                            score = 0;
                        }
                    }
                //Console.WriteLine(string.Format("{0} - {1} - {2} - {3} {4}", score, prediction, matches[i].Raw, homeTeam.EloRating, awayTeam.EloRating));

                if (matches[i].Event == Match.MatchType.WorldCup)
                {
                    WCsum += score;
                    WCcounter++;
                }
                if (matches[i].Event == Match.MatchType.WorldCup || matches[i].Event == Match.MatchType.ContCup)
                {
                    CCsum += score;
                    CCcounter++;
                }
                Allsum += score;
                Allcounter++;


                ProcessMatchResults(matches[i], homeTeam, awayTeam);
            }

            double[] result = new double[3];
            result[0] = WCsum / WCcounter;
            result[1] = CCsum / CCcounter;
            result[2] = Allsum / Allcounter;

            return result;
        }
    }
}
