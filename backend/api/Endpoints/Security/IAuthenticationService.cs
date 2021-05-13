using System.Threading.Tasks;
using Sendo.Api.Data.Models;

namespace Sendo.Api.Endpoints.Security
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// The user making the current request. Null if unauthenticated.
        /// </summary>
        Task<User?> RequestUser { get; }

        /// <summary>
        /// Authenticate a set of credentials and return an access token.
        /// </summary>
        /// <returns>The string representation of an access token, or null if authentication fails</returns>
        Task<string?> AuthenticateAsync(Credentials credentials);
    }
}
