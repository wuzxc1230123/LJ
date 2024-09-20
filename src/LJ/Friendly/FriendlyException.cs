namespace LJ.Friendly
{
    /// <summary>
    /// 友好异常
    /// </summary>
    public class FriendlyException(Type errorMessageResourceType, string errorMessageResourceName) : Exception(errorMessageResourceName)
    {
        public FriendlyException(Type errorMessageResourceType, string errorMessageResourceName, params object[] arguments) : this(errorMessageResourceType, errorMessageResourceName)
        {
            Arguments = arguments;
        }

        /// <summary>
        /// 友好异常
        /// </summary>
        public Type ErrorMessageResourceType { get; set; } = errorMessageResourceType;

        /// <summary>
        /// 友好异常
        /// </summary>
        public string ErrorMessageResourceName { get; set; } = errorMessageResourceName;

        /// <summary>
        /// 附加数据
        /// </summary>
        public object[]? Arguments { get; set; }
    }
}
