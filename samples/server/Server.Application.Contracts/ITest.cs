using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Contracts
{
    /// <summary>
    /// 接口一号
    /// </summary>
    public interface ITest
    {
        /// <summary>
        /// 数据一号
        /// </summary>
        /// <param name="id">a</param>
        /// <param name="input">aa</param>
        /// <returns></returns>
        Task<LoginInputDto> GetAAAAsync(int id, LoginInputDto input);

        /// <summary>
        /// 数据一号2
        /// </summary>
        /// <param name="id">a</param>
        /// <param name="input">aa</param>
        /// <returns></returns>
        Task<LoginInputDto> AAAAsync(int id, LoginInputDto input);
    }
}
