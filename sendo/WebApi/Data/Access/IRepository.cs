using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Sendo.WebApi.Data.Access
{
    /// <summary>
    /// Exposes methods for data store CRUD operations.
    /// </summary>
    /// <remarks>
    /// Implementations of IRepository can be for any kind of data store, thus only basic CRUD operations are defined
    /// instead of transaction functionality or any other feature specific to a subset of data store types.
    /// </remarks>
    public interface IRepository<T>
    {
        void Add(T entity);

        Task AddAsync(T entity);

        void Update(T entity);

        Task UpdateAsync(T entity);

        void Remove(T entity);

        Task RemoveAsync(T entity);

        IQueryable<T> Query();
    }
}
