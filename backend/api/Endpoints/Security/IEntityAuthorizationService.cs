using System.Threading.Tasks;
using Sendo.Api.Data.Models;

namespace Sendo.Api.Endpoints.Security
{
    public interface IEntityAuthorizationService<TEntity>
    {
        bool Authorize(User user, TEntity entity);

        Task<bool> AuthorizeAsync(User user, TEntity entity);
    }
}
