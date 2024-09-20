using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace LJ.Api.Mvc.UnifyResult.Providers
{
    /// <summary>
    /// 规范化结果提供器
    /// </summary>
    public interface IUnifyResultProvider
    {

        /// <summary>
        /// 成功返回值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        IActionResult OnSucceeded(ActionExecutedContext context, object? data);

        /// <summary>
        /// 验证失败
        /// </summary>
        /// <param name="context"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        IActionResult OnValidateFailed(ActionExecutingContext context, string? errors);

        /// <summary>
        /// 友好异常
        /// </summary>
        /// <param name="context"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        IActionResult? OnFriendlyFailed(ExceptionContext context, string error);

    }
}
