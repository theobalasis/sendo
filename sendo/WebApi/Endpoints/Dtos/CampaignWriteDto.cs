using System;

namespace Sendo.WebApi.Endpoints.Dtos
{
    public class CampaignWriteDto
    {
        public string Name { get; init; }

        public Guid ContactGroupId { get; init; }

        public Guid MailTemplateId { get; init; }
    }
}
