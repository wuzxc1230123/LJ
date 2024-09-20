using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Thread
{
    /// <summary>
    /// 异步任务取消标识提供器
    /// </summary>
    public interface ICancellationTokenProvider
    {
        /// <summary>
        /// 获取 异步任务取消标识
        /// </summary>
        CancellationToken Token { get; }
    }
}