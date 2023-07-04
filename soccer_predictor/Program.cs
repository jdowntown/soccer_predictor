using soccer_predictor;

Parser parser = new Parser();
parser.Parse();

List<Match> matches = parser.mMatches;

double average = Simulation.Run(matches, Algorithms.Alphabetical);
Console.WriteLine("Alphabetical: " + average);

average = Simulation.Run(matches, Algorithms.WinRating);
Console.WriteLine("WinRating: " + average);

average = Simulation.Run(matches, Algorithms.EloRating);
Console.WriteLine("EloRating: " + average);

//Simulation.teams.Sort();

//for(int i = 0; i < Simulation.teams.Count; i++)
//{
//    Console.WriteLine(Simulation.teams[i].Name + " - " + Simulation.teams[i].EloRating);
//}