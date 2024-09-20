using Microsoft.Extensions.DependencyInjection;

namespace LJ.Service
{
    /// <summary>
    /// LJ  ServiceCollection
    /// </summary>
    public interface IPackContext
    {
        /// <summary>
        /// ServiceCollection
        /// </summary>
        IServiceCollection ServiceCollection { get; }   

        /// <summary>
        /// 选项模式管理器
        /// </summary>
        IOptionsManager OptionsManager { get; }

        /// <summary>
        /// 反射管理器
        /// </summary>
        IReflectionManager ReflectionManager { get; }

        /// <summary>
        /// 依赖注入管理器
        /// </summary>
        IDependencyManager DependencyManager { get; }
    }
}
