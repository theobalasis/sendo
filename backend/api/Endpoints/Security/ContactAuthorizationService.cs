using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sendo.Api.Data.Access;
using Sendo.Api.Data.Models;

namespace Sendo.Api.Endpoints.Security
{
    public class ContactAuthorizationService : IEntityAuthorizationService<Contact>
    {
        private readonly IRepository<Contact> _contactRepository;

        public ContactAuthorizationService(IRepository<Contact> contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<bool> AuthorizeAsync(User user, Contact entity)
        {
            var contact = await _contactRepository.Query()
                                                  .FirstOrDefaultAsync<Contact?>(c =>
                                                      c.Id == entity.Id && c.UserId == user.Id
                                                  );

            return contact != null;
        }
    }
}
