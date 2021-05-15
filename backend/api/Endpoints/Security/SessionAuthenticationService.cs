using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sendo.Api.Data.Access;
using Sendo.Api.Data.Models;
using Sendo.Api.Mail;

namespace Sendo.Api.Endpoints.Security
{
    public class SessionAuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<SessionToken> _sessionRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMailService _mailService;

        public Task<User?> RequestUser
        {
            get
            {
                var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(' ');
                if (authorizationHeader.Length == 2)
                {
                    if (authorizationHeader[0] == "Session")
                    {
                        var sessionTokenString = authorizationHeader[1];
                        var sessionToken = _sessionRepository.Query().FirstOrDefault<SessionToken>(st => st.Token == sessionTokenString);
                        if (sessionToken != null)
                        {
                            return _userRepository.Query().FirstOrDefaultAsync<User?>(u => u.Id == sessionToken.UserId);
                        }
                    }
                }
                return Task.FromResult<User?>(null);
            }
        }

        public SessionAuthenticationService(IHttpContextAccessor httpContextAccessor,
                                            IRepository<SessionToken> sessionRepository,
                                            IRepository<User> userRepository,
                                            IMailService mailService)
        {
            _httpContextAccessor = httpContextAccessor;
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            _mailService = mailService;
        }

        public async Task<string?> AuthenticateAsync(Credentials credentials)
        {
            var user = await _userRepository.Query().FirstOrDefaultAsync<User?>(u => u.MailAddress == credentials.MailAddress);
            var tokenString = SecurityUtils.GenerateRandomString(16);
            if (user != null)
            {
                // Existing user login

                var hash = SecurityUtils.HashPassword(credentials.Password, user.Salt);
                var token = await _sessionRepository.Query().FirstOrDefaultAsync<SessionToken>(st => st.UserId == user.Id);
                token.Token = tokenString;
                await _sessionRepository.UpdateAsync(token);


                if (hash == user.PasswordHash)
                {
                    // Correct password per stored password hash, try matching with remote mail service.
                    // If unsuccessful, check if remote service is unreachable.
                    if (await _mailService.TryLoginAsync(credentials))
                    {
                        return tokenString;
                    }
                    else
                    {
                        if (await _mailService.TryConnectAsync(credentials))
                        {
                            return null;
                        }
                        else
                        {
                            return tokenString;
                        }
                    }
                }
                else
                {
                    // Wrong password per stored password hash. Try matching with remote mail service
                    // for possible password update.
                    if (await _mailService.TryLoginAsync(credentials))
                    {
                        user.PasswordHash = hash;
                        await _userRepository.UpdateAsync(user);

                        return tokenString;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                // First time login

                if (await _mailService.TryLoginAsync(credentials))
                {
                    var salt = SecurityUtils.GenerateRandomString(16);
                    user = new User
                    {
                        Id = Guid.NewGuid(),
                        MailAddress = credentials.MailAddress,
                        PasswordHash = SecurityUtils.HashPassword(credentials.Password, salt),
                        Salt = salt
                    };

                    var sessionToken = new SessionToken
                    {
                        Id = Guid.NewGuid(),
                        Token = tokenString,
                        User = user,
                        UserId = user.Id
                    };

                    await _userRepository.AddAsync(user);
                    await _sessionRepository.AddAsync(sessionToken);

                    return tokenString;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
