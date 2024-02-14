using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Jazb.Datalayer.Context
{
    public interface IUnitOfWork
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();

        void BulkInsertAllData<T>(IEnumerable<T> model) where T : class;
        void BulkUpdateAllData<T>(IEnumerable<T> model) where T : class;
        
    }
}