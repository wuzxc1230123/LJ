using System.Data;

namespace LJ.DataAccess
{
    /// <summary>
    /// 事务提供器
    /// </summary>
    public interface ITransactionalProvider:IDisposable
    {
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="isolationLevel"></param>
        void Begin(IsolationLevel? isolationLevel = null);

        /// <summary>
        /// 提交
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();
    }
}
