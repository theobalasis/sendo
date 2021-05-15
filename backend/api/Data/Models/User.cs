using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sendo.Api.Data.Models
{
    [Table("user", Schema = "user_data")]
    public class User
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("mail_address")]
        public string MailAddress { get; set; }
        [Column("password_hash")]
        public string PasswordHash { get; set; }
        [Column("salt")]
        public string Salt { get; set; }
    }
}
