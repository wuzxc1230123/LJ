using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Cache.EasyCaching.Redis
{
    /// <summary>
    /// 缓存设置
    /// </summary>
    public class PackCacheOptions
    {
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// host
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 数据库访问层
        /// </summary>
        public int Database { get; set; } = 1;
    }
}
