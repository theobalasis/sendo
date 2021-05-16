using System;
using System.Collections.Generic;

namespace Sendo.Core.Dtos
{
    public class ContactGroupReadDto
    {
        public string Name { get; set; }

        public Guid UserId { get; set; }

        public ICollection<Guid> ContactIds { get; set; }
    }
}
