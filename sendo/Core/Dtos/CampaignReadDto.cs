using System;

namespace Sendo.Core.Dtos
{
    public class CampaignReadDto
    {
        public string Name { get; set; }

        public Guid UserId { get; set; }

        public Guid ContactGroupId { get; set; }

        public Guid MailTemplateId { get; set; }
    }
}
