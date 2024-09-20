using LJ.Service;

namespace LJ.Pack
{
    /// <summary>
    /// LJPack基类
    /// </summary>
    public abstract class LJPack
    {
 
        /// <summary>
        /// 加载服务
        /// </summary>
        /// <param name="packContext"></param>
        public abstract void Add(IPackContext packContext);
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="packProvider"></param>
        /// <returns></returns>
        public abstract Task UseAsync(IPackProvider packProvider);
    }
}
