using LJ.Pack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Host.Web
{
    public class LJWebHostOptions
    {
        /// <summary>
         /// 默认配置
         /// </summary>
        public static LJWebHostOptions Default { get; } = new LJWebHostOptions();

        /// <summary>
        /// 默认配置（带启动参数）
        /// </summary>
        public static LJWebHostOptions Main(string[] args)
        {
            return Default.WithArgs(args);
        }
        /// <summary>
        /// 命令行参数
        /// </summary>
        public string[]? Args { get; set; }


        /// <summary>
        /// 设置进程启动参数
        /// </summary>
        /// <param name="args">启动参数</param>
        /// <returns></returns>
        public LJWebHostOptions WithArgs(string[] args)
        {
            Args = args;
            return this;
        }

        /// <summary>
        /// Pack构建 委托
        /// </summary>
        public Action<IPackBuilder>? PackBuilderAction { get; set; }

        /// <summary>
        /// Pack构建
        /// </summary>
        /// <param name="packBuilderAction"></param>
        /// <returns></returns>
        public LJWebHostOptions ConfigurePack(Action<IPackBuilder> packBuilderAction)
        {
            PackBuilderAction = packBuilderAction;
            return this;
        }
    }
}
