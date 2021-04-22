using System;

namespace Sendo.Api.Models
{
    public class Credentials
    {
        public string MailAddress { get; init; }
        public string Password { get; init; }

        public override bool Equals(object obj)
        {
            return obj is Credentials credentials &&
                   MailAddress == credentials.MailAddress &&
                   Password == credentials.Password;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MailAddress, Password);
        }
    }
}
