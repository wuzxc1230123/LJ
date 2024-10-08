﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Api.Mvc
{
    /// <summary>
    /// Http 拓展类
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// 获取 Action 特性
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static TAttribute? GetMetadata<TAttribute>(this HttpContext httpContext)
            where TAttribute : class
        {
            return httpContext.GetEndpoint()?.Metadata?.GetMetadata<TAttribute>();
        }
        /// <summary>
        /// 判断是否是 WebSocket 请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsWebSocketRequest(this HttpContext context)
        {
            return context.WebSockets.IsWebSocketRequest || context.Request.Path == "/ws";
        }
    }
}
