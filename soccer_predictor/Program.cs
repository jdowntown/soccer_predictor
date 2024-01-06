using soccer_predictor;

Parser parser = new Parser();
parser.Parse();

List<Match> matches = parser.mMatches;


//Simulation.TestParam("HOME_ADV", ref Simulation.HOME_ADV, 95, 115, 0.05, matches);
//Simulation.TestParam("RECENCY", ref Simulation.RECENCY, 35, 45, 0.01, matches);
//Simulation.TestParam("K_WC", ref Simulation.K_WC, 1.38, 1.58, 0.001, matches);
//Simulation.TestParam("K_WCQ", ref Simulation.K_WCQ, 0.9, 1.1, 0.001, matches);
//Simulation.TestParam("K_CC", ref Simulation.K_CC, 1.14, 1.34, 0.001, matches);
//Simulation.TestParam("K_CCQ", ref Simulation.K_CCQ, 0.9, 1.1, 0.001, matches);
//Simulation.TestParam("K_CFC", ref Simulation.K_CFC, 1.28, 1.48, 0.001, matches);
//Simulation.TestParam("K_NL", ref Simulation.K_NL, 0.88, 1.08, 0.001, matches);
//Simulation.TestParam("K_OT", ref Simulation.K_OT, 0.76, 0.96, 0.001, matches);
//Simulation.TestParam("K_F", ref Simulation.K_F, 0.56, 0.76, 0.001, matches);
//Simulation.TestParam("STDEV", ref Simulation.STDEV, 0.5, 1.5, 0.1, matches);


double score = Simulation.Run(matches, Algorithms.GoalRating);
//Console.WriteLine("Default Sum: " + score );

//Printout teams by elo
//Simulation.teams.Sort();

//for(int i = 0; i < Simulation.teams.Count; i++)
//{
//    Console.WriteLine("" + (i+1) + ". " + Simulation.teams[i].Name + ": " + Simulation.teams[i].AtkElo + " " + Simulation.teams[i].DefElo);
//}
