using System;

namespace Sendo.Core.Dtos
{
    public class CampaignWriteDto
    {
        public string Name { get; set; }

        public Guid ContactGroupId { get; set; }

        public Guid MailTemplateId { get; set; }
    }
}
