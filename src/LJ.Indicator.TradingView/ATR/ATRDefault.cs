using LJ.Indicator.ATR;
using LJ.Indicator.RMA;
using LJ.Indicator.SMA;
using LJ.Indicator.TR;

namespace LJ.Indicator.Core.ATR
{
    public class ATRDefault(ITR tR,IRMA rMA) : IndicatorBase<ATRCalculateDto, ATRResultDto>, IATR
    {
        public int Length { get; set; }
        public ITR TR => tR;

        public IRMA RMA => rMA;

        public void SetParameter(int length)
        {
            Length = length;
            rMA.SetParameter(length);
        }


        public override async Task CalculateAsync(ATRCalculateDto calculate, ATRResultDto result, CancellationToken cancellationToken = default)
        {

            var tRResult = await TR.AddOrUpdateAsync(new  TRCalculateDto()
            {
                StartTime = calculate.StartTime,
                Close=calculate.Close ,
                High=calculate.High ,
                Low=calculate.Low ,
            }, cancellationToken);


            var rMAResult = await RMA.AddOrUpdateAsync(new  RMACalculateDto()
            {
                StartTime = calculate.StartTime,
                Source = tRResult.TR
            }, cancellationToken);


            result.ATR = rMAResult.RMA;

        }


    }
}
