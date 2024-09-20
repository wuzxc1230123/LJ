using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.DataAccess.FreeSql
{
    /// <summary>
    /// 数据访问设置
    /// </summary>
    public class PackDataAccessOptions
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionStrings { get; set; } = string.Empty;

        /// <summary>
        /// 是否加载种子数据
        /// </summary>
        public bool UseSeedData { get; set; } = false;


        /// <summary>
        /// 是否同步
        /// </summary>
        public bool UseAuto { get; set; } = false;
    }
}
