using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sendo.Api.Models
{
    [Table("campaign", Schema = "user_data")]
    public class Campaign
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        public string Name { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Column("contact_group_id")]
        public Guid ContactGroupId { get; set; }
        public ContactGroup ContactGroup { get; set; }

        [Column("mail_template_id")]
        public Guid MailTemplateId { get; set; }
        public MailTemplate MailTemplate { get; set; }
    }
}
