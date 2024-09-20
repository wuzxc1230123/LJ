using LJ.Dependency;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LJ.Host.Web.Dependency
{
    /// <summary>
    /// 基于当前HttpContext的<see cref="IServiceScope"/>工厂，如果当前操作处于HttpRequest作用域中，直接使用HttpRequest的作用域，否则创建新的作用域
    /// </summary>
    /// <remarks>
    /// 初始化一个<see cref="HttpContextServiceScopeFactory"/>类型的新实例
    /// </remarks>
    public class HttpContextServiceScopeFactory(IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor) : IHybridServiceScopeFactory
    {

        /// <summary>
        /// 获取 包装的<see cref="IServiceScopeFactory"/>
        /// </summary>
        public IServiceScopeFactory ServiceScopeFactory { get; } = serviceScopeFactory;

        /// <summary>
        /// 获取 当前请求的<see cref="IHttpContextAccessor"/>对象
        /// </summary>
        protected IHttpContextAccessor HttpContextAccessor { get; } = httpContextAccessor;

        /// <summary>
        /// 创建依赖注入服务的作用域，如果当前操作处于HttpRequest作用域中，直接使用HttpRequest的作用域，否则创建新的作用域
        /// </summary>
        /// <returns></returns>
        public virtual IServiceScope CreateScope()
        {
            var httpContext = HttpContextAccessor?.HttpContext;
            //不在HttpRequest作用域中
            if (httpContext == null)
            {
                return ServiceScopeFactory.CreateScope();
            }

            return new NonDisposedHttpContextServiceScope(httpContext.RequestServices);
        }


        /// <summary>
        /// 当前HttpRequest的<see cref="IServiceScope"/>的包装，保持HttpContext.RequestServices的可传递性，并且不释放
        /// </summary>
        /// <remarks>
        /// 初始化一个<see cref="NonDisposedHttpContextServiceScope"/>类型的新实例
        /// </remarks>
        protected class NonDisposedHttpContextServiceScope(IServiceProvider serviceProvider) : IServiceScope
        {

            /// <summary>
            /// 获取 当前HttpRequest的<see cref="IServiceProvider"/>
            /// </summary>
            public IServiceProvider ServiceProvider { get; } = serviceProvider;

            /// <summary>因为是HttpContext的，啥也不做，避免在using使用时被释放</summary>
            public void Dispose()
            { }
        }
    }

}
