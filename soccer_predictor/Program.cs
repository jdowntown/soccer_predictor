using soccer_predictor;

Console.Write("Enter Number:");
string input = Console.ReadLine();
Console.Write("Enter 2nd Number:");
string input2 = Console.ReadLine();
int val = int.Parse(input);
int val2 = int.Parse(input2);
int powered = Calc.Power(val, val2);
Console.WriteLine("Result: " + powered);
