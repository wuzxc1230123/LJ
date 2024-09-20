using LJ.Indicator.TR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Indicator.Core.TR
{
    public class TRDefault : IndicatorBase<TRCalculateDto, TRResultDto>, ITR
    {

        public override Task CalculateAsync(TRCalculateDto calculate, TRResultDto result, CancellationToken cancellationToken = default)
        {

            double tR;
            if (Count == 1)
            {
                tR = calculate.High - calculate.Low;

            }
            else
            {
                tR = IndicatorMath.Max(calculate.High - calculate.Low, Math.Abs(calculate.High - GetCalculateByIndex(1).Close), Math.Abs(calculate.High - GetCalculateByIndex(1).Close));
            }
            tR = IndicatorMath.ValueRound(tR);

            result.TR = tR;

            return Task.CompletedTask;
        }


    }
}
