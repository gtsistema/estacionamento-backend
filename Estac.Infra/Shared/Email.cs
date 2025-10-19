using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Estac.Infra.Shared
{
    public class Email
    {
        private readonly SmtpClient _smtpClient;

        public Email(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var mailMessage = new MailMessage("no-reply@domain.com", to, subject, body);
            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
