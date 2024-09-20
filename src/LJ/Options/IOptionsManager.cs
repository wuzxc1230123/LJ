using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Options
{
    /// <summary>
    /// Options管理器
    /// </summary>
    public interface IOptionsManager
    {
        /// <summary>
        /// 添加Options
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="key"></param>
        /// <param name="optionsPath"></param>
        void Add<TOptions>(string key, string optionsPath) where TOptions : class;

        /// <summary>
        /// 添加Options
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="optionsPath"></param>
        void Add<TOptions>(string optionsPath) where TOptions : class;
    }
}
