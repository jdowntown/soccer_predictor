using soccer_predictor;

Parser parser = new Parser();
parser.Parse();

List<Match> matches = parser.mMatches;

/*double average = Simulation.Run(matches, Algorithms.Alphabetical);
Console.WriteLine("Alphabetical: " + average);

average = Simulation.Run(matches, Algorithms.WinRating);
Console.WriteLine("WinRating: " + average);

average = Simulation.Run(matches, Algorithms.EloRating);
Console.WriteLine("EloRating: " + average);*/

Simulation.TestParam("HOME_ADV", ref Simulation.HOME_ADV, 50, 150, 0.2, matches);
Simulation.TestParam("DRAW_DIST", ref Simulation.DRAW_DIST, 10, 30, 0.2, matches);
Simulation.TestParam("MOV_FACTOR", ref Simulation.MOV_FACTOR, 0.5, 2.5, 0.02, matches);
Simulation.TestParam("MOV_LIMIT", ref Simulation.MOV_LIMIT, 0.5, 2.5, 0.02, matches);
Simulation.TestParam("IMP_FACTOR", ref Simulation.IMP_FACTOR, 0.5, 2.5, 0.02, matches);
Simulation.TestParam("RECENCY", ref Simulation.RECENCY, 30, 60, 0.2, matches);

/*Simulation.teams.Sort();

for(int i = 0; i < Simulation.teams.Count; i++)
{
    Console.WriteLine(Simulation.teams[i].Name + " - " + Simulation.teams[i].EloRating);
}*/