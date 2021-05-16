using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sendo.WebApi.Data.Models;
using Sendo.WebApi.Endpoints.Security;

namespace Sendo.WebApi.Endpoints.Controllers
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
            var split = credentials.MailAddress.Split('@');
            var domain = split[split.Length - 1];
            
            if (credentials.SmtpHost == "")
            {
                credentials.SmtpHost = $"smtp.{domain}";
            }

            if (credentials.ImapHost == "")
            {
                credentials.ImapHost = $"imap.{domain}";
            }

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