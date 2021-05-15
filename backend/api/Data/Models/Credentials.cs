using System;

namespace Sendo.Api.Data.Models
{
    public class Credentials
    {
        public string SmtpHost { get; set; }

        public int SmtpPort { get; set; } = 587;

        public string ImapHost { get; set; }

        public int ImapPort { get; set; } = 993;

        public string MailAddress { get; set; }

        public string Password { get; set; }
    }
}
