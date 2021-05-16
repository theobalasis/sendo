using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sendo.WebApi.Data.Models
{
    [Table("mail_template", Schema = "user_data")]
    public class MailTemplate
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("body")]
        public string Body { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
