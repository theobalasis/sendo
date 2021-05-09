using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sendo.Api.Data.Models
{
    [Table("contact_group", Schema = "user_data")]
    public class ContactGroup
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        public string Name { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}
