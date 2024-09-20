using LJ.Indicator.Lowest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Indicator.Core.Lowest
{
    public class LowestDefault : IndicatorBase<LowestCalculateDto, LowestResultDto>, ILowest
    {
        public int Length { get; set; }


        public void SetParameter(int length)
        {
            Length = length;
        }


        public override Task CalculateAsync(LowestCalculateDto calculate, LowestResultDto result, CancellationToken cancellationToken = default)
        {

            double lowest = calculate.Source;

            for (int i = 0; i <= Length; i++)
            {
                var source = GetCalculateByIndex(i).Source;

                if (lowest > source)
                {
                    lowest = source;
                }
            }

            lowest = IndicatorMath.ValueRound(lowest);

            result.Lowest = lowest;

            return Task.CompletedTask;
        }


    }
}
