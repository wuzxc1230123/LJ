using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LJ.Api.Mvc;
using Microsoft.Extensions.DependencyInjection;
using LJ.Api.Mvc.UnifyResult;
using LJ.Api.Mvc.UnifyResult.Providers;

namespace LJ.Api.Mvc.UnifyResult.Filters
{
    /// <summary>
    /// 规范化结构（请求成功）过滤器
    /// </summary>
    public class SucceededUnifyResultFilter : IAsyncActionFilter, IOrderedFilter
    {
        /// <summary>
        /// 过滤器排序
        /// </summary>
        private const int FilterOrder = 8888;

        /// <summary>
        /// 排序属性
        /// </summary>
        public int Order => FilterOrder;

        /// <summary>
        /// 处理规范化结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 执行 Action 并获取结果
            var actionExecutedContext = await next();
            if (actionExecutedContext.Canceled)
            {
                return;
            }
            // 排除 WebSocket 请求处理
            if (actionExecutedContext.HttpContext.IsWebSocketRequest()) return;

            // 处理已经含有状态码结果的 Result
            if (actionExecutedContext.Result is IStatusCodeActionResult statusCodeResult && statusCodeResult.StatusCode != null)
            {
                // 小于 200 或者 大于 299 都不是成功值，直接跳过
                if (statusCodeResult.StatusCode.Value < 200 || statusCodeResult.StatusCode.Value > 299)
                {
                    return;
                }
            }
            if (!context.ModelState.IsValid)
            {
                return;
            }
            // 如果出现异常，则不会进入该过滤器
            if (actionExecutedContext.Exception != null) return;

            // 获取控制器信息
            if (context.ActionDescriptor is not ControllerActionDescriptor) return;


            // 判断是否支持 MVC 规范化处理或特定检查
            if (UnifyContext.CheckHttpContextNonUnify(context.HttpContext)) return;


            // 处理 BadRequestObjectResult 类型规范化处理
            IActionResult? result;

            // 检查是否是有效的结果（可进行规范化的结果）
            if (actionExecutedContext.Result == null) return;
            if (UnifyContext.CheckVaildResult(actionExecutedContext.Result, out var data))
            {
                var unifyResultProvider = context.HttpContext.RequestServices.GetRequiredService<IUnifyResultProvider>();
                result = unifyResultProvider.OnSucceeded(actionExecutedContext, data);

                // 如果是不能规范化的结果类型，则跳过
                if (result == null) return;

                actionExecutedContext.Result = result;
            }


        }
    }
}
