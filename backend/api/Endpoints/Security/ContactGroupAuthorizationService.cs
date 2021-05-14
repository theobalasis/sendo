using System.Linq;
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

        public bool Authorize(User user, ContactGroup entity)
        {
            var contactGroup = _contactGroupRepository.Query()
                                                  .FirstOrDefault(cg =>
                                                      cg.Id == entity.Id && cg.UserId == user.Id
                                                  );

            return contactGroup != null;
        }

        public async Task<bool> AuthorizeAsync(User user, ContactGroup entity)
        {
            var contactGroup = await _contactGroupRepository.Query()
                                                    .FirstOrDefaultAsync<ContactGroup?>(cg =>
                                                        cg.Id == entity.Id && cg.UserId == user.Id
                                                    );

            return contactGroup != null;
        }
    }
}
