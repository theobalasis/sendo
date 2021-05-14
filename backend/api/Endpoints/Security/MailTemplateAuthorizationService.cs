using System.Linq;
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

        public bool Authorize(User user, MailTemplate entity)
        {
            var mailTemplate = _mailTemplateRepository.Query()
                                                      .FirstOrDefault(mt =>
                                                          mt.Id == entity.Id && mt.UserId == user.Id
                                                      );

            return mailTemplate != null;
        }

        public async Task<bool> AuthorizeAsync(User user, MailTemplate entity)
        {
            var mailTemplate = await _mailTemplateRepository.Query()
                                                            .FirstOrDefaultAsync<MailTemplate?>(mt =>
                                                                mt.Id == entity.Id && mt.UserId == user.Id
                                                            );

            return mailTemplate != null;
        }
    }
}
