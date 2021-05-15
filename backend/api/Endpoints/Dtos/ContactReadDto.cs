using System;
using System.Collections.Generic;
using System.Linq;
using Sendo.Api.Data.Models;

namespace Sendo.Api.Endpoints.Dtos
{
    public class ContactReadDto
    {
        public string MailAddress { get; init; }

        public string FirstName { get; init; }

        public string MiddleName { get; init; }

        public string LastName { get; init; }

        public Gender Gender { get; init; }

        public DateTime DateOfBirth { get; init; }

        public Guid UserId { get; init; }

        public ICollection<Guid> ContactGroupIds { get; init; }

        public ContactReadDto(Contact contact)
        {
            MailAddress = contact.MailAddress;
            FirstName = contact.FirstName;
            MiddleName = contact.MiddleName;
            LastName = contact.LastName;
            Gender = contact.Gender;
            DateOfBirth = contact.DateOfBirth;
            UserId = contact.UserId;
            ContactGroupIds = contact.ContactGroups.Select(cg => cg.Id).ToList();
        }
    }
}