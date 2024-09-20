using LJ.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Rest.HttpClient
{
    public abstract class HttpClientPack : RestPack
    {
        public override void Add(IPackContext packContext)
        {
            packContext.ServiceCollection.AddHttpClient();

            packContext.ServiceCollection.AddTransient<IRestClient, RestClient>();

            packContext.ServiceCollection.AddTransient<IRestFactory, RestFactory>();
        }

        public override Task UseAsync(IPackProvider packProvider)
        {
            return Task.CompletedTask;
        }

    }
}
