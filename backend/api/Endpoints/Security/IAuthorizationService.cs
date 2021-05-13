using System.Threading.Tasks;
using Sendo.Api.Data.Models;

namespace Sendo.Api.Endpoints.Security
{
    public interface IEntityAuthorizationService<TEntity>
    {
        Task<bool> AuthorizeAsync(User user, TEntity entity);
    }
}
