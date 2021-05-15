using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Sendo.Api.Data.Access;
using Sendo.Api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Sendo.Api.Endpoints.Dtos;
using System.Collections.Generic;
using Sendo.Api.Endpoints.Security;

namespace Sendo.Api.Endpoints.Controllers
{
    [ApiController]
    [Route("campaigns")]
    public class CampaignController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IEntityAuthorizationService<Campaign> _campaignAuthorizationService;
        private readonly IRepository<Campaign> _campaignRepository;
        private readonly IRepository<ContactGroup> _contactGroupRepository;
        private readonly IRepository<MailTemplate> _mailTemplateRepository;

        public CampaignController(IAuthenticationService authenticationService,
                                  IEntityAuthorizationService<Campaign> campaignAuthorizationService,
                                  IRepository<Campaign> campaignRepository,
                                  IRepository<ContactGroup> contactGroupRepository,
                                  IRepository<MailTemplate> mailTemplateRepository)
        {
            _authenticationService = authenticationService;
            _campaignAuthorizationService = campaignAuthorizationService;
            _campaignRepository = campaignRepository;
            _contactGroupRepository = contactGroupRepository;
            _mailTemplateRepository = mailTemplateRepository;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCampaignById(Guid id)
        {
            var campaign = await _campaignRepository.Query().FirstOrDefaultAsync(c => c.Id == id);
            if (campaign != null)
            {
                var user = await _authenticationService.RequestUser;
                if (user != null)
                {
                    if (await _campaignAuthorizationService.AuthorizeAsync(user, campaign))
                    {
                        return Ok(new CampaignReadDto(campaign));
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
        public async Task<IActionResult> GetCampaigns()
        {
            var user = await _authenticationService.RequestUser;
            if (user != null)
            {
                var campaigns = await _campaignRepository.Query()
                                                         .Where(c => c.UserId == user.Id)
                                                         .ToListAsync();
                return Ok(campaigns.Select(c => new CampaignReadDto(c)));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCampaign(CampaignWriteDto dto)
        {
            var user = await _authenticationService.RequestUser;
            if (user != null)
            {
                var contactGroup = await _contactGroupRepository.Query()
                                                                .FirstOrDefaultAsync(cg =>
                                                                    cg.Id == dto.ContactGroupId
                                                                    && cg.UserId == user.Id
                                                                );
                var mailTemplate = await _mailTemplateRepository.Query()
                                                                .FirstOrDefaultAsync(mt =>
                                                                    mt.Id == dto.MailTemplateId
                                                                    && mt.UserId == user.Id
                                                                );

                if (contactGroup != null && mailTemplate != null)
                {
                    var id = Guid.NewGuid();
                    var campaign = new Campaign
                    {
                        Id = id,
                        Name = dto.Name,
                        UserId = user.Id,
                        User = user,
                        ContactGroupId = contactGroup.Id,
                        ContactGroup = contactGroup,
                        MailTemplateId = mailTemplate.Id,
                        MailTemplate = mailTemplate
                    };

                    await _campaignRepository.AddAsync(campaign);
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCampaign(Guid id, CampaignWriteDto dto)
        {
            var campaign = await _campaignRepository.Query().FirstOrDefaultAsync(c => c.Id == id);
            if (campaign != null)
            {
                var user = await _authenticationService.RequestUser;
                if (user != null)
                {
                    if (await _campaignAuthorizationService.AuthorizeAsync(user, campaign))
                    {
                        if (dto.ContactGroupId == campaign.ContactGroupId && dto.MailTemplateId == campaign.MailTemplateId)
                        {
                            campaign.Name = dto.Name;
                            await _campaignRepository.UpdateAsync(campaign);
                            return Ok();
                        }
                        else
                        {
                            return BadRequest();
                        }
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
        public async Task<IActionResult> DeleteCampaign(Guid id)
        {
            var campaign = await _campaignRepository.Query().FirstOrDefaultAsync(c => c.Id == id);
            if (campaign != null)
            {
                var user = await _authenticationService.RequestUser;
                if (user != null)
                {
                    if (await _campaignAuthorizationService.AuthorizeAsync(user, campaign))
                    {
                        await _campaignRepository.RemoveAsync(campaign);
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
