using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Pack
{
    /// <summary>
    /// 模块构建器
    /// </summary>
    public interface IPackBuilder
    {

        /// <summary>
        /// 加载模块
        /// </summary>
        List<LJPack> LoadPacks { get; }


        /// <summary>
        /// 添加模块
        /// </summary>
        /// <typeparam name="TPack"></typeparam>
        void Add<TPack>() where TPack : LJPack,new();
    }
}
