using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sendo.Api.Data.Models;
using Sendo.Api.Endpoints.Security;

namespace Sendo.Api.Endpoints.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(Credentials credentials)
        {
            var token = await _authenticationService.AuthenticateAsync(credentials);

            if (token != null)
            {
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}