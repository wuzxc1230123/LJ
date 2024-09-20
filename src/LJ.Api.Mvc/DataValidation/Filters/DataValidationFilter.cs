using LJ.Api.DynamicApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LJ.Api.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using LJ.Api.Mvc.UnifyResult;
using LJ.Api.Mvc.UnifyResult.Providers;

namespace LJ.Api.Mvc.DataValidation.Filters
{
    /// <summary>
    /// 数据验证拦截器
    /// </summary>
    public sealed class DataValidationFilter : IAsyncActionFilter, IOrderedFilter
    {
        /// <summary>
        /// 过滤器排序
        /// </summary>
        private const int FilterOrder = -1000;

        /// <summary>
        /// 排序属性
        /// </summary>
        public int Order => FilterOrder;


        /// <summary>
        /// 拦截请求
        /// </summary>
        /// <param name="context">动作方法上下文</param>
        /// <param name="next">中间件委托</param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 排除 WebSocket 请求处理
            if (context.HttpContext.IsWebSocketRequest())
            {
                await next();
                return;
            }

            // 如果其他过滤器已经设置了结果，则跳过验证
            if (context.Result != null)
            {
                await next();
                return;
            }

            // 如果验证通过，跳过后面的动作
            if (context.ModelState.IsValid)
            {
                await next();
                return;
            }

            // 获取失败的验证信息列表
            var error = context.ModelState
                .Where(s => s.Value != null && s.Value.ValidationState == ModelValidationState.Invalid)
                .SelectMany(s => s.Value!.Errors.ToList())
                .Select(e => e.ErrorMessage)
                .First();


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
                context.Result = unifyResultProvider.OnValidateFailed(context, error);
            }
        }
    }
}
