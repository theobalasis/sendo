using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sendo.Api.Data.Access;
using Sendo.Api.Data.Models;

namespace Sendo.Api.Endpoints.Security
{
    public class ContactGroupAuthorizationService : IEntityAuthorizationService<ContactGroup>
    {
        private readonly IRepository<ContactGroup> _contactGroupRepository;

        public ContactGroupAuthorizationService(IRepository<ContactGroup> contactGroupRepository)
        {
            _contactGroupRepository = contactGroupRepository;
        }

        public async Task<bool> AuthorizeAsync(User user, ContactGroup entity)
        {
            var campaign = await _contactGroupRepository.Query()
                                                    .FirstOrDefaultAsync<ContactGroup?>(c =>
                                                        c.Id == entity.Id && c.UserId == user.Id
                                                    );

            return campaign != null;
        }
    }
}
