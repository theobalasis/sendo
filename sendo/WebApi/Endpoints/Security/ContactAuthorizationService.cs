using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sendo.WebApi.Data.Access;
using Sendo.WebApi.Data.Models;

namespace Sendo.WebApi.Endpoints.Security
{
    public class ContactAuthorizationService : IEntityAuthorizationService<Contact>
    {
        private readonly IRepository<Contact> _contactRepository;

        public ContactAuthorizationService(IRepository<Contact> contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public bool Authorize(User user, Contact entity)
        {
            var contact = _contactRepository.Query()
                                             .FirstOrDefault(c =>
                                                 c.Id == entity.Id && c.UserId == user.Id
                                             );

            return contact != null;
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
