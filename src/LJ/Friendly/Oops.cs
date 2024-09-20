namespace LJ.Friendly;


/// <summary>
/// 抛异常静态类
/// </summary>
public static class Oops
{
    /// <summary>
    /// 抛出友好异常
    /// </summary>
    /// <param name="errorMessageResourceType"></param>
    /// <param name="errorMessageResourceName"></param>
    /// <returns></returns>
    public static FriendlyException Oh(Type errorMessageResourceType, string errorMessageResourceName)
    {
        var friendlyException = new FriendlyException(errorMessageResourceType, errorMessageResourceName);
        return friendlyException;
    }

    /// <summary>
    /// 抛出友好异常
    /// </summary>
    /// <param name="errorMessageResourceType"></param>
    /// <param name="errorMessageResourceName"></param>
    /// <returns></returns>
    public static FriendlyException Oh(Type errorMessageResourceType, string errorMessageResourceName,params object[] arguments)
    {
        var friendlyException = new FriendlyException(errorMessageResourceType, errorMessageResourceName, arguments);
        return friendlyException;
    }
}
