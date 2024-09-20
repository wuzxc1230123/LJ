using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LJ.Extensions;
using LJ.Data.Api;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LJ.Api.Mvc.UnifyResult;

/// <summary>
/// 规范化结果上下文
/// </summary>
public static class UnifyContext
{

    /// <summary>
    /// 跳过规范化处理的 Response Content-Type
    /// </summary>
    public static string[] ResponseContentTypesOfNonUnify { get; } =
        [
        "text/event-stream",
        "application/pdf",
        "application/octet-stream",
        "image/"
         ];

    /// <summary>
    /// 检查 HttpContext 是否进行规范化处理
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns>返回 true 跳过处理，否则进行规范化处理</returns>
    public static bool CheckHttpContextNonUnify(HttpContext httpContext)
    {
        var contentType = httpContext.Response.ContentType;
        if (ResponseContentTypesOfNonUnify.Any(u => contentType != null && contentType.Contains(u, StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }

        return false;
    }


    /// <summary>
    /// 检查是否是有效的结果（可进行规范化的结果）
    /// </summary>
    /// <param name="result"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    internal static bool CheckVaildResult(IActionResult result, out object? data)
    {
        data = default;

        // 排除以下结果，跳过规范化处理
        var isDataResult = result switch
        {
            ViewResult => false,
            PartialViewResult => false,
            FileResult => false,
            ChallengeResult => false,
            SignInResult => false,
            SignOutResult => false,
            RedirectToPageResult => false,
            RedirectToRouteResult => false,
            RedirectResult => false,
            RedirectToActionResult => false,
            LocalRedirectResult => false,
            ForbidResult => false,
            ViewComponentResult => false,
            PageResult => false,
            BadRequestObjectResult => false,
            NotFoundResult => false,
            NotFoundObjectResult => false,
            _ => true,
        };

        // 目前支持返回值 ActionResult
        if (isDataResult) data = result switch
        {
            // 处理内容结果
            ContentResult content => content.Content,
            // 处理对象结果
            ObjectResult obj => obj.Value,
            // 处理 JSON 对象
            JsonResult json => json.Value,
            _ => null,
        };
        return isDataResult;
    }

}
