using System;

namespace Sendo.Api.Queries
{
    public class ContactQuery
    {
        public Guid? UserId { get; set; }
        public Guid? ContactGroupId { get; set; }
        public string? Gender { get; set; }
        public int LowestAge { get; set; }
        public int HighestAge { get; set; }
    }
}
