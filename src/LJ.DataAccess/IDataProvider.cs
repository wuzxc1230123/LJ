using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.DataAccess
{
    /// <summary>
    /// 数据提供器
    /// </summary>
    public interface IDataProvider
    {
    }

    /// <summary>
    /// 数据提供器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataProvider<in T>: IDataProvider where T :class 
    {
    }
}
