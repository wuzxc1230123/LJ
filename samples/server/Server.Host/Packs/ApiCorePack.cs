using LJ.Api.Mvc;
using LJ.Api.Mvc.ApiPackBuilders;
using Server.Application.Contracts;
using Server.Application.Test;

namespace Server.Host.Packs
{
    public class ApiCorePack : ApiMvcPack
    {
        public override void AddApiPack(IApiPackBuilder apiPackBuilder)
        {
            apiPackBuilder.AddDynamicApi<ITest, ATest>();
        }
    }
}
