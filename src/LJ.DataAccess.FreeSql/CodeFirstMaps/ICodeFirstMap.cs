using FreeSql;
using FreeSql.Extensions.EfCoreFluentApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.DataAccess.FreeSql.CodeFirstMaps
{
    /// <summary>
    /// 模型映射
    /// </summary>
    public interface ICodeFirstMap
    {
        void Run(ICodeFirst codeFirst);
    }
}
