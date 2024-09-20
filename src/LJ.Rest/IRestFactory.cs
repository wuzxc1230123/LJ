using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Rest
{
    /// <summary>
    /// 工厂
    /// </summary>
    public interface IRestFactory
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        IRestClient Create();
    }
}
