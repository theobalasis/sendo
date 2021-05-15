using System;
using System.Collections.Generic;
using System.Linq;
using Sendo.Api.Data.Models;

namespace Sendo.Api.Endpoints.Dtos
{
    public class ContactGroupReadDto
    {
        public string Name { get; init; }

        public Guid UserId { get; init; }

        public ICollection<Guid> ContactIds { get; init; }

        public ContactGroupReadDto(ContactGroup contactGroup)
        {
            Name = contactGroup.Name;
            UserId = contactGroup.UserId;
            ContactIds = contactGroup.Contacts.Select(c => c.Id).ToList();
        }
    }
}
