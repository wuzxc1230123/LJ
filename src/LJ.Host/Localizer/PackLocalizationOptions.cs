using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Host.Localizer
{
    public class PackLocalizationOptions
    {
        /// <summary>
        /// 默认
        /// </summary>
        public string DefaultCulture { get; set; } = "zh-CN";

        /// <summary>
        /// 支持列表
        /// </summary>
        public string[] Cultures { get; set; } = ["zh-CN", "en-US"];
    }
}
