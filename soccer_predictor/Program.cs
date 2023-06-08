using soccer_predictor;

Console.Write("Enter Number:");
string input = Console.ReadLine();
int val = int.Parse(input);
int squared = Calc.Square(val);
Console.WriteLine("Result: " + squared);
