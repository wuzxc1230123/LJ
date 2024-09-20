using LJ.Api.Mvc.Friendly.Handlers;
using LJ.Api.Mvc.UnifyResult;
using LJ.Api.Mvc.UnifyResult.Providers;
using LJ.Extensions;
using LJ.Friendly;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LJ.Api.Mvc.Friendly.Filters
{

    /// <summary>
    /// 友好异常拦截器
    /// </summary>
    public sealed class FriendlyExceptionFilter : IAsyncExceptionFilter
    {
        /// <summary>
        /// 异常拦截
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {

            // 排除 WebSocket 请求处理
            if (context.HttpContext.IsWebSocketRequest()) return;

            // 如果异常在其他地方被标记了处理，那么这里不再处理
            if (context.ExceptionHandled) return;

            // 标记验证异常已被处理
            context.ExceptionHandled = true;

            // 只记录未知异常
            if (context.Exception is not FriendlyException friendlyException)
            {
                // 解析异常处理服务，实现自定义异常额外操作，如记录日志等
                var globalExceptionHandler = context.HttpContext.RequestServices.GetService<IGlobalExceptionHandler>();
                if (globalExceptionHandler != null)
                {
                    await globalExceptionHandler.OnExceptionAsync(context);
                }
                context.Result = new JsonResult(context.Exception.Message)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
                var logger = context.HttpContext.RequestServices.GetLogger<FriendlyExceptionFilter>();
                logger.LogError("ExceptionFilter ：{Message}", context.Exception.Message);
                return;
            }
            var localizer = context.HttpContext.RequestServices.GetLocalizer(friendlyException.ErrorMessageResourceType);
            var error = friendlyException.Arguments == null ? localizer[friendlyException.ErrorMessageResourceName].Value : localizer[friendlyException.ErrorMessageResourceName, friendlyException.Arguments].Value;
            if (UnifyContext.CheckHttpContextNonUnify(context.HttpContext))
            {
                // 返回 JsonResult
                context.Result = new JsonResult(error)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else
            {

                var unifyResultProvider = context.HttpContext.RequestServices.GetRequiredService<IUnifyResultProvider>();
                context.Result = unifyResultProvider.OnFriendlyFailed(context, error);
            }
            context.ExceptionHandled = true;
        }
    }
}
