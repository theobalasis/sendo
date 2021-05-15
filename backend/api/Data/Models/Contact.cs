using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sendo.Api.Data.Models
{
    [Table("contact", Schema = "user_data")]
    public class Contact
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("mail_address")]
        public string MailAddress { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("middle_name")]
        public string MiddleName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("gender")]
        public Gender Gender { get; set; }
        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<ContactGroup> ContactGroups { get; set; }
    }
}
