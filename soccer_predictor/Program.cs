using soccer_predictor;

Parser parser = new Parser();
parser.Parse();

List<Match> matches = parser.mMatches;

for(int i = 0; i < matches.Count; i++)
{
    if (matches[i].Event == "FIFA World Cup")
    {
        if (matches[i].HomeTeam[0] == matches[i].AwayTeam[0])
        {
            Console.WriteLine("Draw - " + matches[i].Raw);
        }
        else if(matches[i].HomeTeam[0] < matches[i].AwayTeam[0])
        {
            Console.WriteLine(matches[i].HomeTeam + " wins - " + matches[i].Raw);
        }
        else
        {
            Console.WriteLine(matches[i].AwayTeam + " wins - " + matches[i].Raw);
        }
    }
}





