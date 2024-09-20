using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.FaultHand
{
    /// <summary>
    /// 故障处理
    /// </summary>
    public interface IFaultHand
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> ExecuteAsync<T>(string key,Func<CancellationToken, Task<T>> callback, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ExecuteAsync(string key, Func<CancellationToken, Task> callback, CancellationToken cancellationToken = default);
    }
}
