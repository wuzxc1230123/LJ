using Server.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace Server.Shared
{
    /// <summary>
    /// 数据类型
    /// </summary>
    public enum ADataType
    {
        /// <summary>
        /// 说明
        /// </summary>
        [Display(Name = nameof(UserCenterResource.UserName), ResourceType = typeof(UserCenterResource))]
        A,
        /// <summary>
        /// 说1
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Password), ResourceType = typeof(UserCenterResource))]
        B,
    }
}
