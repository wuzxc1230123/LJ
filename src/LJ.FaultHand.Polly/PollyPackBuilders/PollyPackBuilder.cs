using Microsoft.Extensions.DependencyInjection;
using Polly.Retry;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.FaultHand.Polly.PollyPackBuilders
{
    public class PollyPackBuilder(IServiceCollection serviceCollection) : IPollyPackBuilder
    {
        private readonly IServiceCollection _serviceCollection = serviceCollection;

        public IPollyPackBuilder AddPipeline(string key, int maxRetryAttempts = 5, int delaySeconds = 2)
        {

            _serviceCollection.AddResiliencePipeline(key, builder =>
            {
                builder
                    .AddRetry(new RetryStrategyOptions() 
                    {
                      MaxRetryAttempts= maxRetryAttempts,
                      Delay= TimeSpan.FromSeconds(1)
                    })
                    .AddTimeout(TimeSpan.FromSeconds(10));
            });

            return this;
        }
    }
}
