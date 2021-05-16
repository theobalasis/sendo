using System.Threading.Tasks;
using Sendo.WebApi.Data.Models;

namespace Sendo.WebApi.Endpoints.Security
{
    public interface IEntityAuthorizationService<TEntity>
    {
        bool Authorize(User user, TEntity entity);

        Task<bool> AuthorizeAsync(User user, TEntity entity);
    }
}
