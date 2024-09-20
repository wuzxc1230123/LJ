using FreeSql;
using LJ.DataAccess.SeedDatas;
using LJ.Thread;
using Microsoft.Extensions.DependencyInjection;

namespace LJ.DataAccess.FreeSql
{
    public abstract class DataAccessFreeSqlPack : DataAccessPack
    {
        public override void Add(IPackContext packContext)
        {
            packContext.OptionsManager.Add<PackDataAccessOptions>("DataAccess");

            packContext.ServiceCollection.AddFreeRepository();
            packContext.ServiceCollection.AddScoped<UnitOfWorkManager>();

            var freeSqlPackBuilder = new FreeSqlPackBuilder(packContext.ServiceCollection);
            AddFreeSqlPack(freeSqlPackBuilder);
            freeSqlPackBuilder.Load();
            packContext.ServiceCollection.AddSingleton<IFreeSqlPackBuilder>(freeSqlPackBuilder);
        }

        public override async Task UseAsync(IPackProvider packProvider)
        {
            var freeSql = packProvider.ServiceProvider.GetRequiredService<IFreeSql>();
            //加载模型映射
            var codeFirstMaps = packProvider.ServiceProvider.GetServices<ICodeFirstMap>();
            foreach ( var codeFirstMap in codeFirstMaps) 
            {
                codeFirstMap.Run(freeSql.CodeFirst);
            }

            var dataAccessOptions = packProvider.OptionsProvider.Get<PackDataAccessOptions>();

            if (dataAccessOptions.UseAuto)
            {
                var freeSqlPackBuilder = packProvider.ServiceProvider.GetRequiredService<IFreeSqlPackBuilder>();
                freeSql.CodeFirst.SyncStructure([.. freeSqlPackBuilder.LoadDataTypes]);
            }


            if (dataAccessOptions.UseSeedData)
            {
                await Task.Run(async () => {
                    using var serviceScope = packProvider.ServiceProvider.CreateScope();
                    var seedDatas = serviceScope.ServiceProvider.GetServices<ISeedData>();
                    if (seedDatas.Any())
                    {
                        return;
                    }
                    var cancellationTokenProvider = serviceScope.ServiceProvider.GetRequiredService<ICancellationTokenProvider>();
                    var transactionalProvider = serviceScope.ServiceProvider.GetRequiredService<ITransactionalProvider>();

                    transactionalProvider.Begin();
                    foreach (var seedData in seedDatas.OrderBy(a => a.Seq)) 
                    {
                        await seedData.RunAsync(cancellationTokenProvider.Token);
                    }
                    transactionalProvider.Commit();
                });
            }
        }


        public abstract void AddFreeSqlPack (IFreeSqlPackBuilder freeSqlPackBuilder);
    }
}
