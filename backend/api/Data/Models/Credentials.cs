using System;

namespace Sendo.Api.Data.Models
{
    public class Credentials
    {
        public Uri SmtpUri { get; init; } = new Uri("");

        public Uri ImapUri { get; init; } = new Uri("");

        public string MailAddress { get; init; } = "";

        public string Password { get; init; } = "";
    }
}
