using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Sendo.WebApi.Data.Access
{
    /// <summary>
    /// Exposes additional methods for functionality specific to relational databases.
    /// </summary>
    public interface IDbRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Initiates a database transaction, that must then be either committed or rolled back.
        /// </summary>
        IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Snapshot);

        /// <summary>
        /// Asynchronously initiates a database transaction, that must then be either committed or rolled back.
        /// </summary>
        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Snapshot);
    }
}
