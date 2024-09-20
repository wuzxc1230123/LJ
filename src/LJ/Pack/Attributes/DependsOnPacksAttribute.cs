using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Pack.Attributes
{


    /// <summary>
    /// LJ模块依赖
    /// </summary>
    /// <param name="dependedPackType"></param>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnPacksAttribute( Type dependedPackType) : Attribute
    {

        /// <summary>
        /// 获取 当前模块的依赖模块类型集合
        /// </summary>
        public Type DependedPackType { get; } = dependedPackType;
    }
}
