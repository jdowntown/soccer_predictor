using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soccer_predictor
{
    internal class Simulation
    {
        public delegate int PredictorFunction(Team home, Team away, bool isNeutral);

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
                rd += 100;
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
            double ratingsChange = 40 * (ar - er);
            home.EloRating += ratingsChange;
            away.EloRating -= ratingsChange;
        }

        public static List<Team>? teams;

        public static double Run(List <Match> matches, PredictorFunction predictor)
        {
            double sum = 0.0;
            int counter = 0;

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
                    //Console.WriteLine(string.Format("{0} - {1} - {2} - {3} {4}", score, prediction, matches[i].Raw, homeTeam.WinRating, awayTeam.WinRating));
                    sum += score;
                    counter++;
                }

                ProcessMatchResults(matches[i], homeTeam, awayTeam);
            }
            return sum / counter;
        }
    }
}
