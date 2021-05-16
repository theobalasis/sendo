using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sendo.WebApi.Data.Access;
using Sendo.WebApi.Data.Models;
using Sendo.WebApi.Endpoints.Dtos;
using Sendo.WebApi.Endpoints.Security;

namespace Sendo.WebApi.Endpoints.Controllers
{
    [ApiController]
    [Route("contacts")]
    public class ContactController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IEntityAuthorizationService<Contact> _authorizationService;
        private readonly IRepository<Contact> _repository;

        public ContactController(IAuthenticationService authenticationService,
                                 IEntityAuthorizationService<Contact> authorizationService,
                                 IRepository<Contact> repository)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _repository = repository;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetContactById(Guid id)
        {
            var contact = await _repository.Query().Include(c => c.ContactGroups).FirstOrDefaultAsync(c => c.Id == id);
            if (contact != null)
            {
                var user = await _authenticationService.RequestUser;
                if (user != null)
                {
                    if (await _authorizationService.AuthorizeAsync(user, contact))
                    {
                        return Ok(new ContactReadDto(contact));
                    }
                    else
                    {
                        return Forbid();
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetContacts()
        {
            var user = await _authenticationService.RequestUser;
            if (user != null)
            {
                var contacts = await _repository.Query()
                                                 .Where(c => c.UserId == user.Id)
                                                 .ToListAsync();
                return Ok(contacts.Select(c => new ContactReadDto(c)));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(ContactWriteDto dto)
        {
            var user = await _authenticationService.RequestUser;
            if (user != null)
            {
                var id = Guid.NewGuid();
                var contact = new Contact
                {
                    Id = id,
                    MailAddress = dto.MailAddress,
                    FirstName = dto.FirstName,
                    MiddleName = dto.MiddleName,
                    LastName = dto.LastName,
                    Gender = dto.Gender,
                    DateOfBirth = dto.DateOfBirth,
                    UserId = user.Id,
                    User = user
                };

                await _repository.AddAsync(contact);
                return Ok(id);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateContact(Guid id, ContactWriteDto dto)
        {
            var contact = await _repository.Query().FirstOrDefaultAsync(c => c.Id == id);
            if (contact != null)
            {
                var user = await _authenticationService.RequestUser;
                if (user != null)
                {
                    if (await _authorizationService.AuthorizeAsync(user, contact))
                    {
                        contact.MailAddress = dto.MailAddress;
                        contact.FirstName = dto.FirstName;
                        contact.MiddleName = dto.MiddleName;
                        contact.LastName = dto.LastName;
                        contact.Gender = dto.Gender;
                        contact.DateOfBirth = dto.DateOfBirth;

                        await _repository.UpdateAsync(contact);
                        return Ok();
                    }
                    else
                    {
                        return Forbid();
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await _repository.Query().FirstOrDefaultAsync(c => c.Id == id);
            if (contact != null)
            {
                var user = await _authenticationService.RequestUser;
                if (user != null)
                {
                    if (await _authorizationService.AuthorizeAsync(user, contact))
                    {
                        await _repository.RemoveAsync(contact);
                        return Ok();
                    }
                    else
                    {
                        return Forbid();
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
