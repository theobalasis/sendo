using System;
using System.Collections.Generic;
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
    [Route("mail_templates")]
    public class MailTemplateController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IEntityAuthorizationService<MailTemplate> _authorizationService;
        private readonly IRepository<MailTemplate> _repository;

        public MailTemplateController(IAuthenticationService authenticationService,
                                      IEntityAuthorizationService<MailTemplate> authorizationService,
                                      IRepository<MailTemplate> repository)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _repository = repository;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetMailTemplateById(Guid id)
        {
            var user = await _authenticationService.RequestUser;
            if (user != null)
            {
                var mailTemplate = await _repository.Query().FirstOrDefaultAsync(mt => mt.Id == id);
                if (mailTemplate != null)
                {
                    if (await _authorizationService.AuthorizeAsync(user, mailTemplate))
                    {
                        return Ok(new MailTemplateReadDto(mailTemplate));
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

        [HttpGet]
        public async Task<ActionResult<List<MailTemplate>>> GetAllMailTemplates()
        {
            var user = await _authenticationService.RequestUser;
            if (user != null)
            {
                var mailTemplates = await _repository.Query()
                                                     .Where(mt => mt.UserId == user.Id)
                                                     .ToListAsync();
                return Ok(mailTemplates.Select(mt => new MailTemplateReadDto(mt)));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMailTemplate(MailTemplateWriteDto dto)
        {
            var user = await _authenticationService.RequestUser;
            if (user != null)
            {
                var id = Guid.NewGuid();
                var mailTemplate = new MailTemplate
                {
                    Id = id,
                    Name = dto.Name,
                    Body = dto.Body,
                    UserId = user.Id,
                    User = user
                };

                await _repository.AddAsync(mailTemplate);
                return Ok(id);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateMailTemplate(Guid id, MailTemplateWriteDto dto)
        {
            var user = await _authenticationService.RequestUser;
            if (user != null)
            {
                var mailTemplate = await _repository.Query().FirstOrDefaultAsync(mt => mt.Id == id);
                if (mailTemplate != null)
                {
                    if (await _authorizationService.AuthorizeAsync(user, mailTemplate))
                    {
                        mailTemplate.Name = dto.Name;
                        mailTemplate.Body = dto.Body;

                        await _repository.UpdateAsync(mailTemplate);
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
        public async Task<IActionResult> DeleteMailTemplate(Guid id)
        {
            var user = await _authenticationService.RequestUser;
            if (user != null)
            {
                var mailTemplate = await _repository.Query().FirstOrDefaultAsync(mt => mt.Id == id);
                if (mailTemplate != null)
                {
                    if (await _authorizationService.AuthorizeAsync(user, mailTemplate))
                    {
                        await _repository.RemoveAsync(mailTemplate);
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
    }
}
