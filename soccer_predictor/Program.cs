using soccer_predictor;

Parser parser = new Parser();
parser.Parse();

List<Match> matches = parser.mMatches;

double sum = 0.0;
int counter = 0;

for(int i = 0; i < matches.Count; i++)
{
    double score = 0;
    string prediction = "";
    if (matches[i].Event == "FIFA World Cup")
    {
        if (matches[i].HomeTeam[0] == matches[i].AwayTeam[0])
        {
            prediction = "Draw";

            if(matches[i].HomeScore == matches[i].AwayScore)
            {
                score = 1.0;
            }
            else
            {
                score = 0.3;
            }
        }
        else if(matches[i].HomeTeam[0] < matches[i].AwayTeam[0])
        {
            prediction = matches[i].HomeTeam + " wins";
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
            prediction = matches[i].AwayTeam + " wins";
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
        //Console.WriteLine(string.Format("{0} - {1} - {2}", score, prediction, matches[i].Raw));
        sum += score;
        counter++;
    }

}

double average = sum / counter;
Console.WriteLine("Match average: " + average);
