using LJ.Thread;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Host.Web.Thread
{
    /// <summary>
    /// 基于当前HttpContext的<see cref="IServiceScope"/>的异步任务取消标识
    /// </summary>
    public class HttpContextCancellationTokenProvider(IHttpContextAccessor httpContextAccessor) : ICancellationTokenProvider
    {

        /// <summary>
        /// 获取 异步任务取消标识
        /// </summary>
        public CancellationToken Token { get; } = httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;
    }
}
