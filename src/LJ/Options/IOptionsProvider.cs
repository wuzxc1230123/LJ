using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Options
{
    /// <summary>
    /// 选项模式提供器
    /// </summary>
    public interface IOptionsProvider
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        TOptions Get<TOptions>(string key) where TOptions : class;

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <returns></returns>
        TOptions Get<TOptions>() where TOptions : class;
    }
}
