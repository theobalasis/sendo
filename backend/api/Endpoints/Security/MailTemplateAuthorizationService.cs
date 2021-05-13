using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sendo.Api.Data.Access;
using Sendo.Api.Data.Models;

namespace Sendo.Api.Endpoints.Security
{
    public class MailTemplateAuthorizationService : IEntityAuthorizationService<MailTemplate>
    {
        private readonly IRepository<MailTemplate> _mailTemplateRepository;

        public MailTemplateAuthorizationService(IRepository<MailTemplate> mailTemplateRepository)
        {
            _mailTemplateRepository = mailTemplateRepository;
        }

        public async Task<bool> AuthorizeAsync(User user, MailTemplate entity)
        {
            var campaign = await _mailTemplateRepository.Query()
                                                    .FirstOrDefaultAsync<MailTemplate?>(c =>
                                                        c.Id == entity.Id && c.UserId == user.Id
                                                    );

            return campaign != null;
        }
    }
}
