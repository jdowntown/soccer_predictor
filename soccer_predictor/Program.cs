using soccer_predictor;

Parser parser = new Parser();
parser.Parse();

List<Match> matches = parser.mMatches;


/*double average = Simulation.Run(matches, Algorithms.Alphabetical);
Console.WriteLine("Alphabetical: " + average);

average = Simulation.Run(matches, Algorithms.WinRating);
Console.WriteLine("WinRating: " + average);
*/
double[] result = Simulation.Run(matches, Algorithms.EloRating);
double score = result[0] + result[1] + result[2];
Console.WriteLine("Default Sum: " + score + "  WC: " + result[0] + "  CC:" + result[1] + "  All:" + result[2] );

//Simulation.TestParam("HOME_ADV", ref Simulation.HOME_ADV, 95, 115, 0.05, matches);
//Simulation.TestParam("DRAW_DIST", ref Simulation.DRAW_DIST, 25, 35, 0.05, matches);
//Simulation.TestParam("MOV_2", ref Simulation.MOV_2, 0.4, 0.6, 0.001, matches);
//Simulation.TestParam("MOV_3", ref Simulation.MOV_3, 0.65, 0.85, 0.001, matches);
//Simulation.TestParam("MOV_4", ref Simulation.MOV_4, 0.95, 1.15, 0.001, matches);
//Simulation.TestParam("MOV_5", ref Simulation.MOV_5, 0.93, 1.13, 0.001, matches);
//Simulation.TestParam("RECENCY", ref Simulation.RECENCY, 35, 45, 0.01, matches);
//Simulation.TestParam("K_WC", ref Simulation.K_WC, 1.38, 1.58, 0.001, matches);
//Simulation.TestParam("K_WCQ", ref Simulation.K_WCQ, 0.9, 1.1, 0.001, matches);
//Simulation.TestParam("K_CC", ref Simulation.K_CC, 1.14, 1.34, 0.001, matches);
//Simulation.TestParam("K_CCQ", ref Simulation.K_CCQ, 0.9, 1.1, 0.001, matches);
//Simulation.TestParam("K_CFC", ref Simulation.K_CFC, 1.28, 1.48, 0.001, matches);
//Simulation.TestParam("K_NL", ref Simulation.K_NL, 0.88, 1.08, 0.001, matches);
//Simulation.TestParam("K_OT", ref Simulation.K_OT, 0.76, 0.96, 0.001, matches);
//Simulation.TestParam("K_F", ref Simulation.K_F, 0.56, 0.76, 0.001, matches);

//Printout teams by elo
/*Simulation.teams.Sort();

for(int i = 0; i < Simulation.teams.Count; i++)
{
    Console.WriteLine("" + (i+1) + ". " + Simulation.teams[i].Name + ": " + Simulation.teams[i].EloRating);
}*/

//Printout biggest upsets
matches.Sort();

for (int i = 0; i < 20; i++)
{
    Console.WriteLine("" + (i + 1) + ". " + matches[i].EloChange + " " + matches[i].Raw);
}