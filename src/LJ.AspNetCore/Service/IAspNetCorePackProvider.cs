using LJ.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.AspNetCore.Service
{
    public interface IAspNetCorePackProvider : IPackProvider
    {
        /// <summary>
        /// Web服务
        /// </summary>
        public WebApplication WebApplication { get; }

        /// <summary>
        /// 日志服务
        /// </summary>
        public ILogger Logger { get; }

    }
}
