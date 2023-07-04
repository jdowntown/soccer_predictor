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

//This simulates the possible home advantage values
/*for (double val = 0; val < 200; val += 1.0)
{
    Simulation.HOME_ADV = val;
    double score = Simulation.Run(matches, Algorithms.EloRating);
    Console.WriteLine("Home Adv: " + val + " Score:" + score);
}*/

//This simulates the possible drawing distance values
/*for (double val = 0; val < 200; val += 1.0)
{
    Simulation.DRAW_DIST = val;
    double score = Simulation.Run(matches, Algorithms.EloRating);
    Console.WriteLine("Draw Dist: " + val + " Score:" + score);
}*/

//This simulates the margin of victory values
/*for (double val = 0; val <= 3.0; val += 0.1)
{
    Simulation.MOV_FACTOR = val;
    double score = Simulation.Run(matches, Algorithms.EloRating);
    Console.WriteLine("MOV factor: " + val + " Score:" + score);
}*/

//This simulates the margin of victory limit
/*for (double val = 0; val <= 3.0; val += 0.1)
{
    Simulation.MOV_LIMIT = val;
    double score = Simulation.Run(matches, Algorithms.EloRating);
    Console.WriteLine("MOV limit: " + val + " Score:" + score);
}*/

//This simulates the importance factor limit
/*for (double val = 1.0; val <= 3.0; val += 0.1)
{
    Simulation.IMP_FACTOR = val;
    double score = Simulation.Run(matches, Algorithms.EloRating);
    Console.WriteLine("Imp factor: " + val + " Score:" + score);
}*/

//This simulates the recency
/*for (double val = 1.0; val <= 100.0; val += 1.0)
{
    Simulation.RECENCY = val;
    double score = Simulation.Run(matches, Algorithms.EloRating);
    Console.WriteLine("Recency: " + val + " Score:" + score);
}*/


/*Simulation.teams.Sort();

for(int i = 0; i < Simulation.teams.Count; i++)
{
    Console.WriteLine(Simulation.teams[i].Name + " - " + Simulation.teams[i].EloRating);
}*/