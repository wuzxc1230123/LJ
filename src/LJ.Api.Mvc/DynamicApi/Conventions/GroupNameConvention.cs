using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Api.Mvc.DynamicApi.Conventions
{
    /// <summary>
    /// 分组转换
    /// </summary>
    public class GroupNameConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerNamespace = controller.ControllerType.Namespace;
            var groupName = controllerNamespace!.Split('.').LastOrDefault();
            controller.ApiExplorer.GroupName = groupName;
        }
    }
}
