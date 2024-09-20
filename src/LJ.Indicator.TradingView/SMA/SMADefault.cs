using LJ.Indicator.SMA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Indicator.Core.SMA
{
    public class SMADefault : IndicatorBase<SMACalculateDto, SMAResultDto>, ISMA
    {
        public int Length { get; set; }


        public void SetParameter(int length)
        {
            Length = length;
        }


        public override Task CalculateAsync(SMACalculateDto calculate, SMAResultDto result, CancellationToken cancellationToken = default)
        {

            var sma =0.0;

            for (int i = 0; i < Length; i++)
            {
                sma += GetCalculateByIndex(i).Source / Length;
            }

            sma = IndicatorMath.ValueRound(sma);

            result.SMA = sma;

            return Task.CompletedTask;
        }


    }
}
