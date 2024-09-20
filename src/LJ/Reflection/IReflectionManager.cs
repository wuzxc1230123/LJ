using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Reflection
{
    /// <summary>
    /// 反射管理器
    /// </summary>
    public interface IReflectionManager
    {
        /// <summary>
        /// 获取类别
        /// </summary>
        /// <param name="whereFunc"></param>
        /// <returns></returns>
        List<Type> GetTypes(Func<Type, bool>? whereFunc = null);

        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <param name="whereFunc"></param>
        /// <returns></returns>
        List<Assembly> GetAssemblies(Func<Assembly, bool>? whereFunc=null);
    }
}
