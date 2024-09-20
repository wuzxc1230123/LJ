using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.DataAccess.FreeSql
{
    public abstract class FreeSqlDataProvider: IDataProvider
    {
        private readonly ITransactionalProvider  _transactionalProvider;

        public IFreeSql Orm { get; } = null!;

        public FreeSqlDataProvider(ITransactionalProvider transactionalProvider)
        {
            _transactionalProvider = transactionalProvider;
            if (_transactionalProvider is not FreeSqlTransactionalProvider freeSqlTransactionalProvider)
            {
              throw  new AppException($"{GetType().FullName} ITransactionalProvider is not FreeSqlTransactionalProvider");
            }
            freeSqlTransactionalProvider.Begin(null);
            Orm = freeSqlTransactionalProvider.UnitOfWork.Value!.Orm;
        }
    }


    public abstract class FreeSqlDataProvider<T>(ITransactionalProvider transactionalProvider) : FreeSqlDataProvider(transactionalProvider), IDataProvider<T> where T : class
    {
        public IBaseRepository<T> Repository => Orm.GetRepository<T>();
    }
}
