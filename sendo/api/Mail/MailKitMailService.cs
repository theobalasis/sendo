using System.Threading.Tasks;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using Sendo.WebApi.Data.Models;

namespace Sendo.WebApi.Mail
{
    public class MailKitMailService : IMailService
    {
        public async Task<bool> TryConnectAsync(Credentials credentials)
        {
            using (var smtpClient = new SmtpClient())
            {
                using (var imapClient = new ImapClient())
                {
                    try
                    {
                        await smtpClient.ConnectAsync(credentials.SmtpHost, credentials.SmtpPort);
                        await imapClient.ConnectAsync(credentials.ImapHost, credentials.ImapPort);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public async Task<bool> TryLoginAsync(Credentials credentials)
        {
            using (var smtpClient = new SmtpClient())
            {
                using (var imapClient = new ImapClient())
                {
                    await smtpClient.ConnectAsync(credentials.SmtpHost, credentials.SmtpPort);
                    await imapClient.ConnectAsync(credentials.ImapHost, credentials.ImapPort);
                    try
                    {
                        await smtpClient.AuthenticateAsync(credentials.MailAddress, credentials.Password);
                        await imapClient.AuthenticateAsync(credentials.MailAddress, credentials.Password);
                        return true;
                    }
                    catch (AuthenticationException)
                    {
                        return false;
                    }
                }
            }
        }
    }
}
