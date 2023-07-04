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
Console.WriteLine("WC: " + result[0] + "   CC:" + result[1] + "   All:" + result[2] );

Simulation.TestParam("HOME_ADV", ref Simulation.HOME_ADV, 80, 120, 1.0, matches);
//Simulation.TestParam("DRAW_DIST", ref Simulation.DRAW_DIST, 10, 30, 0.1, matches);
//Simulation.TestParam("MOV_2", ref Simulation.MOV_2, 0, 1.5, 0.01, matches);
//Simulation.TestParam("MOV_3", ref Simulation.MOV_3, 0, 1.5, 0.01, matches);
//Simulation.TestParam("MOV_4", ref Simulation.MOV_4, 0, 1.5, 0.01, matches);
//Simulation.TestParam("MOV_5", ref Simulation.MOV_5, 0, 1.5, 0.01, matches);
//Simulation.TestParam("RECENCY", ref Simulation.RECENCY, 30, 60, 0.02, matches);
//Simulation.TestParam("K_WC", ref Simulation.K_WC, 0.5, 1.5, 0.01, matches);
//Simulation.TestParam("K_WCQ", ref Simulation.K_WCQ, 0.5, 1.5, 0.01, matches);
//Simulation.TestParam("K_CC", ref Simulation.K_CC, 0.5, 1.5, 0.01, matches);
//Simulation.TestParam("K_CCQ", ref Simulation.K_CCQ, 0.5, 1.5, 0.01, matches);
//Simulation.TestParam("K_CFC", ref Simulation.K_CFC, 0.5, 1.5, 0.01, matches);
//Simulation.TestParam("K_OT", ref Simulation.K_OT, 0.5, 1.5, 0.01, matches);
//Simulation.TestParam("K_F", ref Simulation.K_F, 0.5, 1.5, 0.01, matches);

/*Simulation.teams.Sort();

for(int i = 0; i < Simulation.teams.Count; i++)
{
    Console.WriteLine("" + (i+1) + ". " + Simulation.teams[i].Name + ": " + Simulation.teams[i].EloRating);
}*/