using System.Collections.Generic;

namespace LJ.Indicator.Core
{
    public abstract class IndicatorBase<TCalculate, TResult>: IIndicator<TCalculate, TResult>
        where TCalculate : IndicatorDto, new()
        where TResult : IndicatorDto, new()
    {


        private readonly List<TCalculate> _calculates;
        private readonly List<TResult> _results;


        public int Count => _calculates.Count;

        public IndicatorBase()
        {
            _calculates = [];
            _results = [];
        }

        public async Task LoadAsync(List<TCalculate> calculates, CancellationToken cancellationToken = default)
        {
            foreach (var calculate in calculates)
            {
              await  AddOrUpdateAsync(calculate, cancellationToken);
            }
        }

        public async Task<TResult> AddOrUpdateAsync(TCalculate calculate, CancellationToken cancellationToken = default)
        {
            if (Count > 0)
            {
                var lastIndicatorCalculate = _calculates.Last();

                if (lastIndicatorCalculate.StartTime == calculate.StartTime)
                {
                    _calculates.RemoveAt(Count - 1);
                    _results.RemoveAt(Count - 1);
                }
            }


            var result = new TResult()
            {
                StartTime = calculate.StartTime,
            };

            _calculates.Add(calculate);
            _results.Add(result);

            await CalculateAsync(calculate, result, cancellationToken);


            if (Count > IndicatorConst.CalculateLegnth)
            {
                _calculates.RemoveAt(0);
                _results.RemoveAt(0);
            }
            return result;
        }

        public abstract Task CalculateAsync(TCalculate calculate, TResult result, CancellationToken cancellationToken = default);

        public TCalculate GetCalculateByIndex(int index = 0)
        {
           return _calculates[^(index+1)];
        }

        public TResult GetResultByIndex(int index = 0)
        {
            return _results[^(index+1)];
        }
    }
}
