using LJ.FaultHand.Polly.PollyPackBuilders;
using LJ.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.FaultHand.Polly
{
    public abstract class FaultHandPollyPack : FaultHandPack
    {
        public override void Add(IPackContext packContext)
        {
            packContext.ServiceCollection.AddTransient<IFaultHand, PollyFaultHand>();

            var pollyPackBuilder = new PollyPackBuilder(packContext.ServiceCollection);
            pollyPackBuilder.AddPipeline("LJ", 5);

            AddPollyPack(pollyPackBuilder);
            packContext.ServiceCollection.AddSingleton<IPollyPackBuilder>(pollyPackBuilder);
        }

        public override Task UseAsync(IPackProvider packProvider)
        {
            return Task.CompletedTask;
        }

        public abstract void AddPollyPack(IPollyPackBuilder pollyPackBuilder);
    }
}
