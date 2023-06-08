using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soccer_predictor
{
    internal class Calc
    {
        public static int Power(int baseVal, int expVal)
        {
            int result = 1;
            for( int i = 0; i < expVal; i++ ) 
            {
                result *= baseVal;
            }
            return result;
        }

        public static int Superpower(int x, int y)
        {
            return Power(x, y) + Power(y, x);
        }
    }
}
