using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Indicator.Core
{
    public class IndicatorMath
    {
        public const int ParameterLength= 1;

        public const int ValueLength = 9;

        public  static double ParameterRound(double val)
        {
            return Math.Round(val, ParameterLength);
        }

        public static double ValueRound(double val)
        {
            return Math.Round(val, ValueLength);
        }

        public static double Max(params double[] vals)
        {
            return vals.Max();
        }
        
    }
}
