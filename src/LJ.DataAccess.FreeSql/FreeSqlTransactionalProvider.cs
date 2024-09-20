using FreeSql;
using System.Data;

namespace LJ.DataAccess.FreeSql
{
    public class FreeSqlTransactionalProvider(UnitOfWorkManager unitOfWorkManager) : ITransactionalProvider
    {
        public AsyncLocal<IUnitOfWork> UnitOfWork { get; set; } = new();


        private readonly UnitOfWorkManager _unitOfWorkManager = unitOfWorkManager;

        public void Begin(IsolationLevel? isolationLevel = null) {

            if (UnitOfWork.Value==null)
            {
                return ;
            }
            UnitOfWork.Value= _unitOfWorkManager.Begin(Propagation.Required, isolationLevel);
        }

        public void Commit()
        {

            if (UnitOfWork.Value == null)
            {
                return;
            }
            UnitOfWork.Value.Commit();
        }


        public void Rollback()
        {

            if (UnitOfWork.Value == null)
            {
                return;
            }
            UnitOfWork.Value.Rollback();
        }

        public void Dispose()
        {
            UnitOfWork.Value?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
