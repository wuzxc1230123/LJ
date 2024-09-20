using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.DataAccess.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public interface ISeedData
    {
        /// <summary>
        /// 执行顺序
        /// </summary>
        public int Seq { get;}


        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task RunAsync(CancellationToken cancellationToken = default);
    }
}
