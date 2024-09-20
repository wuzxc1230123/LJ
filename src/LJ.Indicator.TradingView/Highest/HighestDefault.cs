using LJ.Indicator.Highest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Indicator.Core.Highest
{
    public class HighestDefault : IndicatorBase<HighestCalculateDto, HighestResultDto>, IHighest
    {
        public int Length { get; set; }


        public void SetParameter(int length)
        {
            Length = length;
        }


        public override Task CalculateAsync(HighestCalculateDto calculate, HighestResultDto result, CancellationToken cancellationToken = default)
        {

            double highest = calculate.Source;

            for (int i = 0; i <= Length; i++)
            {
                var source =GetCalculateByIndex(i).Source;

                if (highest < source)
                {
                    highest = source;
                }

            }
            highest = IndicatorMath.ValueRound(highest);

            result.Highest = highest;

            return Task.CompletedTask;
        }


    }
}
