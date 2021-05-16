using Sendo.Core.Models;
using System;
using System.Collections.Generic;

namespace Sendo.Core.Dtos
{
    public class ContactReadDto
    {
        public string MailAddress { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Guid UserId { get; set; }

        public ICollection<Guid> ContactGroupIds { get; set; }
    }
}
