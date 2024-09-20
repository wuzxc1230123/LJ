using Server.Shared;
using Server.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace Server.Application.Contracts
{
    /// <summary>
    /// 登录输入参数
    /// </summary>
    [Display(Name = nameof(UserCenterResource.LoginInput), ResourceType = typeof(UserCenterResource))]
    public class LoginInputDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = nameof(UserCenterResource.UserName), ResourceType = typeof(UserCenterResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(32, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [MinLength(5, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMinValidationError))]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Password), ResourceType = typeof(UserCenterResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(32, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [MinLength(5, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMinValidationError))]
        public string Password { get; set; } = null!;

        /// <summary>
        /// 数据
        /// </summary>
        [Display(Name = nameof(UserCenterResource.LoginInput), ResourceType = typeof(UserCenterResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public ADataType DataType { get; set; }
    }
}
