using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LJ.Data.Api;
using LJ.Extensions;
using Microsoft.Extensions.Localization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LJ.Api.Mvc.UnifyResult.Providers
{
    public class UnifyResultProvider : IUnifyResultProvider
    {
        public IActionResult OnSucceeded(ActionExecutedContext context, object? data)
        {
            return new JsonResult(Result(StatusCodes.Status200OK, true, data: data)
             , context.HttpContext.RequestServices.GetOptions<JsonOptions>().JsonSerializerOptions);
        }

        public IActionResult OnValidateFailed(ActionExecutingContext context, string? errors)
        {
            return new JsonResult(Result(StatusCodes.Status400BadRequest, true, errors: errors)
             , context.HttpContext.RequestServices.GetOptions<JsonOptions>().JsonSerializerOptions);
        }

        public IActionResult? OnFriendlyFailed(ExceptionContext context, string error)
        {
            return new JsonResult(Result(StatusCodes.Status500InternalServerError, true, errors: error)
              , context.HttpContext.RequestServices.GetOptions<JsonOptions>().JsonSerializerOptions);
        }

        public static ApiResult<object> Result(int statusCode, bool succeeded, object? data = default, string? errors = default)
        {
            return new ApiResult<object>
            {
                StatusCode = statusCode,
                Succeeded = succeeded,
                Data = data,
                Errors = errors,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };
        }


    }
}
