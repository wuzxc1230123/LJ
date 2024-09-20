using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Indicator
{
   public interface IIndicator<TCalculate, TResult>
        where TCalculate : IndicatorDto, new()
        where TResult : IndicatorDto, new()
    {

        public int Count { get; }

        public Task LoadAsync(List<TCalculate> calculates, CancellationToken cancellationToken = default);

        public Task<TResult> AddOrUpdateAsync(TCalculate calculate, CancellationToken cancellationToken = default);


        public TCalculate GetCalculateByIndex(int index = 0);

        public TResult GetResultByIndex(int index = 0);
    }
}
