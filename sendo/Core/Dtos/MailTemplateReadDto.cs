using System;

namespace Sendo.Core.Dtos
{
    public class MailTemplateReadDto
    {
        public string Name { get; set; }

        public string Body { get; set; }

        public Guid UserId { get; set; }
    }
}
