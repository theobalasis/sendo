using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sendo.Api.Data.Access;
using Sendo.Api.Data.Models;
using Sendo.Api.Endpoints.Dtos;
using Sendo.Api.Endpoints.Security;

namespace Sendo.Api.Endpoints.Controllers
{
    [ApiController]
    [Route("contact_groups")]
    public class ContactGroupController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IEntityAuthorizationService<ContactGroup> _authorizationService;
        private readonly IEntityAuthorizationService<Contact> _contactAuthorizationService;
        private readonly IRepository<ContactGroup> _repository;
        private readonly IRepository<Contact> _contactRepository;

        public ContactGroupController(IAuthenticationService authenticationService,
                                      IEntityAuthorizationService<ContactGroup> authorizationService,
                                      IEntityAuthorizationService<Contact> contactAuthorizationService,
                                      IRepository<ContactGroup> repository,
                                      IRepository<Contact> contactRepository)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _contactAuthorizationService = contactAuthorizationService;
            _repository = repository;
            _contactRepository = contactRepository;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetContactGroupById(Guid id)
        {
            var contactGroup = await _repository.Query().Include(cg => cg.Contacts).FirstOrDefaultAsync(cg => cg.Id == id);
            if (contactGroup != null)
            {
                var user = await _authenticationService.RequestUser;
                if (user != null)
                {
                    if (await _authorizationService.AuthorizeAsync(user, contactGroup))
                    {
                        return Ok(new ContactGroupReadDto(contactGroup));
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
        public async Task<IActionResult> GetAllContactGroups()
        {
            var user = await _authenticationService.RequestUser;
            if (user != null)
            {
                var contactGroups = await _repository.Query()
                                                     .Where(cg => cg.UserId == user.Id)
                                                     .ToListAsync();
                return Ok(contactGroups.Select(cg => new ContactGroupReadDto(cg)));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateContactGroup(ContactGroupWriteDto dto)
        {
            var user = await _authenticationService.RequestUser;
            if (user != null)
            {
                var id = Guid.NewGuid();
                var contactGroup = new ContactGroup
                {
                    Id = id,
                    Name = dto.Name,
                    UserId = user.Id,
                    User = user
                };

                await _repository.AddAsync(contactGroup);
                return Ok(id);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("{groupId:guid}/contacts")]
        public async Task<IActionResult> AddContact(Guid groupId, [FromBody] Guid contactId)
        {
            var user = await _authenticationService.RequestUser;
            if (user != null)
            {
                var contactGroup = _repository.Query().Include(cg => cg.Contacts).FirstOrDefault(cg => cg.Id == groupId);
                var contact = _contactRepository.Query().Include(c => c.ContactGroups).FirstOrDefault(c => c.Id == contactId);
                if (contactGroup != null && contact != null)
                {
                    if (await _authorizationService.AuthorizeAsync(user, contactGroup)
                        && await _contactAuthorizationService.AuthorizeAsync(user, contact))
                    {
                        contactGroup.Contacts.Add(contact);
                        contact.ContactGroups.Add(contactGroup);
                        await _repository.UpdateAsync(contactGroup);
                        await _contactRepository.UpdateAsync(contact);
                        return Ok();
                    }
                    else
                    {
                        return Forbid();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete("{groupId:guid}/contacts/{contactId:guid}")]
        public async Task<IActionResult> RemoveContact(Guid groupId, Guid contactId)
        {
            var user = await _authenticationService.RequestUser;
            if (user != null)
            {
                var contactGroup = _repository.Query().Include(cg => cg.Contacts).FirstOrDefault(cg => cg.Id == groupId);
                var contact = _contactRepository.Query().Include(c => c.ContactGroups).FirstOrDefault(c => c.Id == contactId);
                if (contactGroup != null && contact != null)
                {
                    if (await _authorizationService.AuthorizeAsync(user, contactGroup)
                        && await _contactAuthorizationService.AuthorizeAsync(user, contact))
                    {
                        contactGroup.Contacts.Remove(contact);
                        contact.ContactGroups.Remove(contactGroup);
                        await _repository.UpdateAsync(contactGroup);
                        await _contactRepository.UpdateAsync(contact);
                        return Ok();
                    }
                    else
                    {
                        return Forbid();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteContactGroup(Guid id)
        {
            var contactGroup = await _repository.Query().FirstOrDefaultAsync(cg => cg.Id == id);
            if (contactGroup != null)
            {
                var user = await _authenticationService.RequestUser;
                if (user != null)
                {
                    if (await _authorizationService.AuthorizeAsync(user, contactGroup))
                    {
                        await _repository.RemoveAsync(contactGroup);
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