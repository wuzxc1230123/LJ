using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.FaultHand.Polly.PollyPackBuilders
{
    /// <summary>
    /// PollyPack 构建器
    /// </summary>
    public interface IPollyPackBuilder
    {
        /// <summary>
        /// 添加管道
        /// </summary>
        /// <param name="key"></param>
        /// <param name="maxRetryAttempts">额外重试次数</param>
        /// <param name="delaySeconds">间隔秒</param>
        IPollyPackBuilder AddPipeline(string key, int maxRetryAttempts = 5, int delaySeconds = 2);
    }
}
