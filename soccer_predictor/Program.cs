using soccer_predictor;

Parser parser = new Parser();
parser.Parse();

List<Match> matches = parser.mMatches;

double average = Simulation.Run(matches, Algorithms.Alphabetical);
Console.WriteLine("Alphabetical: " + average);

average = Simulation.Run(matches, Algorithms.WinRating);
Console.WriteLine("WinRating: " + average);
