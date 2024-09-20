using LJ.Indicator.EMA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Indicator.Core.EMA
{
    public class EMADefault : IndicatorBase<EMACalculateDto, EMAResultDto>, IEMA
    {
        public int Length { get; set; }


        public void SetParameter(int length)
        {
            Length = length;
        }


        public override Task CalculateAsync(EMACalculateDto calculate, EMAResultDto result, CancellationToken cancellationToken = default)
        {

            var ema =0.0;

            if (Count > Length)
            {
                var source = GetCalculateByIndex(Length ).Source;
                ema = calculate.Source - source;
            }
            ema = IndicatorMath.ValueRound(ema);

            result.EMA = ema;

            return Task.CompletedTask;
        }


    }
}
