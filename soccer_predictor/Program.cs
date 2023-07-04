using soccer_predictor;

Parser parser = new Parser();
parser.Parse();

List<Match> matches = parser.mMatches;

double average = Simulation.Run(matches, Algorithms.Alphabetical);

Console.WriteLine("Match average: " + average);
