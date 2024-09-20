using LJ.Indicator.RMA;
using LJ.Indicator.SMA;

namespace LJ.Indicator.Core.RMA
{
    public class RMADefault(ISMA sMA) : IndicatorBase<RMACalculateDto, RMAResultDto>, IRMA
    {
        public int Length { get; set; }
        public double Alpha { get; set; }
        public ISMA SMA => sMA;

        public void SetParameter(int length)
        {
            Length = length;
            Alpha= IndicatorMath.ParameterRound(1/ length);
            SMA.SetParameter(length);
        }


        public override async Task CalculateAsync(RMACalculateDto calculate, RMAResultDto result, CancellationToken cancellationToken = default)
        {

            double rMA;
            if (Count ==1)
            {
                var sMAResult=await  SMA.AddOrUpdateAsync(new  SMACalculateDto()
                {
                    StartTime = calculate.StartTime,
                    Source = calculate.Source
                }, cancellationToken);
                rMA = sMAResult.SMA;
            }
            else
            {
                rMA = Alpha * calculate.Source + (1 - Alpha) * GetResultByIndex(1).RMA;

            }
            rMA = IndicatorMath.ValueRound(rMA);

            result.RMA = rMA;

        }


    }
}
