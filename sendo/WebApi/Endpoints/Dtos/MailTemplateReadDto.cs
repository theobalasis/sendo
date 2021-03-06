

using System;
using Sendo.WebApi.Data.Models;

namespace Sendo.WebApi.Endpoints.Dtos
{
    public class MailTemplateReadDto
    {
        public string Name { get; init; }

        public string Body { get; init; }

        public Guid UserId { get; init; }

        public MailTemplateReadDto(MailTemplate mailTemplate)
        {
            Name = mailTemplate.Name;
            Body = mailTemplate.Body;
            UserId = mailTemplate.UserId;
        }
    }
}
