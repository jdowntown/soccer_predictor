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
            Console.WriteLine(contents);
        }
    }
}
