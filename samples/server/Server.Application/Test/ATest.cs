using LJ.Api.DynamicApi;
using LJ.Friendly;
using LJ.Service;
using Server.Application.Contracts;
using Server.Shared.Resources;

namespace Server.Application.Test
{
    public class ATest : ITest, IDynamicApi
    {
        private readonly IAppProvider appProvider;

        public ATest(IAppProvider appProvider)
        {
            this.appProvider = appProvider;
        }

        public Task<LoginInputDto> AAAAsync(int id, LoginInputDto input)
        {
            throw new NotImplementedException();
        }

        public Task<LoginInputDto> GetAAAAsync(int id, LoginInputDto input)
        {
            var a = appProvider.GetLocalizer<ValidateErrorMessagesResource>();
            var b = a[nameof(ValidateErrorMessagesResource.RequiredValidationError), "1"];

            throw Oops.Oh(typeof(ValidateErrorMessagesResource), nameof(ValidateErrorMessagesResource.RequiredValidationError), "1");
        }

    }
}
