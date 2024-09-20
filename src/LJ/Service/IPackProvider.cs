using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Service
{
    public interface IPackProvider
    {
        /// <summary>
        /// 选项模式
        /// </summary>
        public IOptionsProvider OptionsProvider { get; }

        /// <summary>
        /// 服务提供
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

    }
}
