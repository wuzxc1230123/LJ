using LJ.Indicator.Change;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Indicator.Core.Change
{
    public class ChangeDefault : IndicatorBase<ChangeCalculateDto, ChangeResultDto>, IChange
    {
        public int Length { get; set; }


        public void SetParameter(int length)
        {
            Length = length;
        }


        public override Task CalculateAsync(ChangeCalculateDto calculate, ChangeResultDto result, CancellationToken cancellationToken = default)
        {

            var change =0.0;

            if (Count > Length)
            {
                var source = GetCalculateByIndex(Length).Source;
                change = calculate.Source - source;
            }
            change = IndicatorMath.ValueRound(change);

            result.Change = change;

            return Task.CompletedTask;
        }


    }
}
