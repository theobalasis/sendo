using System;
using System.Threading.Tasks;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using Sendo.WebApi.Data.Models;

namespace Sendo.WebApi.Mail
{
    public interface IMailService
    {
        Task<bool> TryLoginAsync(Credentials credentials);

        Task<bool> TryConnectAsync(Credentials credentials);
    }
}