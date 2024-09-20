namespace LJ.Rest
{
    /// <summary>
    /// Rest 客户端
    /// </summary>
    public interface IRestClient
    {

        /// <summary>
        /// 添加查询函数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IRestClient AddQueryParameter(string key, string value);

        /// <summary>
        /// 添加头
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        IRestClient AddHeaderParameter(string key, string value);

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="bodyParameter"></param>
        /// <returns></returns>
        IRestClient AddBodyParameter(object bodyParameter);

        /// <summary>
        /// 请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="restType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RestResult<T>> SendAsyc<T>(string url, RestType restType, CancellationToken cancellationToken);
    }
}
