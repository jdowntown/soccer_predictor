using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soccer_predictor
{
    internal class Parser
    {
        public void Parse()
        {
            string contents = File.ReadAllText("..\\..\\..\\results.csv");
            string[] lines = contents.Split('\n');
            for(int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(string.Format("{0}.{1}", i + 1, lines[i]));
            }
        }
    }
}
