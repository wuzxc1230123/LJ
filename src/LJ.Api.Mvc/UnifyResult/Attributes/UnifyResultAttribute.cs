using LJ.Data.Api;
using LJ.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Api.Mvc.UnifyResult.Attributes
{
    /// <summary>
    /// 规范化结果配置
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class UnifyResultAttribute : ProducesResponseTypeAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="statusCode"></param>
        /// <param name="method"></param>
        public UnifyResultAttribute(Type type, int statusCode) : base(type, statusCode)
        {
            WrapType(type);
        }

        /// <summary>
        /// 包装类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        private void WrapType(Type type)
        {
            if (type != null)
            {
                var unityMetadata = typeof(ApiResult<>);

                if (unityMetadata != null && !type.HasImplementedRawGeneric(unityMetadata))
                {
                    Type = unityMetadata.MakeGenericType(type);
                }
            }
        }
    }
}
