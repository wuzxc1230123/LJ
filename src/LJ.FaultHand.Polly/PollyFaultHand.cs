
using Polly;
using Polly.Registry;

namespace LJ.FaultHand.Polly
{
    public class PollyFaultHand(ResiliencePipelineProvider<string> pipelineProvider) : IFaultHand
    {
        private readonly ResiliencePipelineProvider<string> _pipelineProvider=pipelineProvider;
        public async Task<T> ExecuteAsync<T>(string key, Func<CancellationToken, Task<T>> callback, CancellationToken cancellationToken = default)
        {
            var pipeline = _pipelineProvider.GetPipeline(key);
            return await pipeline.ExecuteAsync(async token =>
            {
                return await callback(token);
            }, cancellationToken);
        }

        public async Task ExecuteAsync(string key, Func<CancellationToken, Task> callback, CancellationToken cancellationToken = default)
        {
            var pipeline = _pipelineProvider.GetPipeline(key);
            await pipeline.ExecuteAsync(async token =>
            {
                await callback(token);
            }, cancellationToken);
        }
    }
}
