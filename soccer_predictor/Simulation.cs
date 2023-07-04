using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace soccer_predictor
{
    internal class Simulation
    {
        public static double HOME_ADV = 111.2;
        public static double DRAW_DIST = 24.4;
        public static double MOV_FACTOR = 1.7;
        public static double MOV_LIMIT = 1.3;
        public static double IMP_FACTOR = 1.2;
        public static double RECENCY = 42.2;

        public delegate int PredictorFunction(Team home, Team away, bool isNeutral);

        public static void TestParam(string name, ref double param, double min, double max, double step, List<Match> matches)
        {
            double bestScore = 0.0;
            double bestVal = min;
            for (double val = min; val <= max; val += step)
            {
                param = val;
                double score = Simulation.Run(matches, Algorithms.EloRating);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestVal = val;
                }
                //Console.WriteLine(name + ": " + val + " Score:" + score);
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
                af += MOV_FACTOR;
            }
            else if(mov == 3)
            {
                af += MOV_FACTOR + MOV_FACTOR * MOV_LIMIT;
            }
            else if(mov > 3)
            {
                af = MOV_FACTOR + MOV_FACTOR * MOV_LIMIT + MOV_FACTOR * MOV_LIMIT * MOV_LIMIT;
            }

            double mi = RECENCY;
            if (match.Event == "FIFA World Cup")
            {
                mi *= IMP_FACTOR;
            }
            else if (match.Event == "Friendly")
            {
                mi /= IMP_FACTOR;
            }

            double ratingsChange = mi * (ar - er) * af;
            home.EloRating += ratingsChange;
            away.EloRating -= ratingsChange;
        }

        public static List<Team>? teams;

        public static double Run(List <Match> matches, PredictorFunction predictor)
        {
            double sum = 0.0;
            int counter = 0;
            int correctWin = 0;
            int correctDraw = 0;
            int winsCalledDraws = 0;
            int drawsCalledWins = 0;
            int lossesCalledWins = 0;

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

                //Run prediction if it is a world cup match
                if (matches[i].Event == "FIFA World Cup")
                {
                    int prediction = predictor(homeTeam, awayTeam, matches[i].IsNeutral);
                    double score = 0;
                    string predictionText = "";

                    if (prediction == 0)
                    {
                        predictionText = "Draw";

                        if (matches[i].HomeScore == matches[i].AwayScore)
                        {
                            score = 1.0;
                            correctDraw++;
                        }
                        else
                        {
                            score = 0.3;
                            winsCalledDraws++;
                        }
                    }
                    else if (prediction > 0)
                    {
                        predictionText = matches[i].HomeTeam + " wins";
                        if (matches[i].HomeScore > matches[i].AwayScore)
                        {
                            score = 1.0;
                            correctWin++;
                        }
                        else if (matches[i].HomeScore == matches[i].AwayScore)
                        {
                            score = 0.3;
                            drawsCalledWins++;
                        }
                        else
                        {
                            score = 0;
                            lossesCalledWins++;
                        }
                    }
                    else
                    {
                        predictionText = matches[i].AwayTeam + " wins";
                        if (matches[i].HomeScore < matches[i].AwayScore)
                        {
                            score = 1.0;
                            correctWin++;
                        }
                        else if (matches[i].HomeScore == matches[i].AwayScore)
                        {
                            score = 0.3;
                            drawsCalledWins++;
                        }
                        else
                        {
                            score = 0;
                            lossesCalledWins++;
                        }
                    }
                    //Console.WriteLine(string.Format("{0} - {1} - {2} - {3} {4}", score, prediction, matches[i].Raw, homeTeam.EloRating, awayTeam.EloRating));
                    sum += score;
                    counter++;
                }

                ProcessMatchResults(matches[i], homeTeam, awayTeam);
            }

            Console.WriteLine(string.Format("Total Matches: {0}", counter));
            Console.WriteLine(string.Format("Correctly Predicted Win: {0}", correctWin));
            Console.WriteLine(string.Format("Correctly Predicted Draw: {0}", correctDraw));
            Console.WriteLine(string.Format("Predicted Draw Actual Win: {0}", winsCalledDraws));
            Console.WriteLine(string.Format("Predicted Win Actual Draw: {0}", drawsCalledWins));
            Console.WriteLine(string.Format("Predicted Win Actual Loss: {0}", lossesCalledWins));

            return sum / counter;
        }
    }
}
