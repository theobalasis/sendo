using System;
using Sendo.WebApi.Data.Models;

namespace Sendo.WebApi.Endpoints.Dtos
{
    public class CampaignReadDto
    {
        public string Name { get; init; }

        public Guid UserId { get; init; }

        public Guid ContactGroupId { get; init; }

        public Guid MailTemplateId { get; init; }

        public CampaignReadDto(Campaign campaign)
        {
            Name = campaign.Name;
            UserId = campaign.UserId;
            ContactGroupId = campaign.ContactGroupId;
            MailTemplateId = campaign.MailTemplateId;
        }
    }
}
