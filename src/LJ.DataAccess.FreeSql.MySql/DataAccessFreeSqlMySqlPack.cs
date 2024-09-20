using FreeSql;
using LJ.DataAccess.FreeSql.FreeSqlPackBuilders;
using LJ.Service;
using Microsoft.Extensions.DependencyInjection;
using LJ.Extensions;

namespace LJ.DataAccess.FreeSql.MySql
{
    public abstract class DataAccessFreeSqlMySqlPack : DataAccessFreeSqlPack
    {

        public override void Add(IPackContext packContext)
        {
           base.Add(packContext);

            packContext.ServiceCollection.AddSingleton<IFreeSql>(r =>
            {
                var dataAccessOptions = r.GetOptions<PackDataAccessOptions>();
                IFreeSql fsql = new FreeSqlBuilder()
                    .UseConnectionString(DataType.MySql, dataAccessOptions.ConnectionStrings)
                    .Build();
                return fsql;
            });
        }
    }
}
