using LJ.Service;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.AspNetCore.Service
{
    public interface IAspNetCorePackContext : IPackContext
    {
        /// <summary>
        /// Web 服务
        /// </summary>
        public WebApplicationBuilder WebApplicationBuilder { get; }
    }
}
