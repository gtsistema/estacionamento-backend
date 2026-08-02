using Estac.Domain.Auth;
using Estac.Domain.Interface.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Estac.Service.Email
{
    public class SmtpEmailSenderService : IEmailSenderService
    {
        private readonly SmtpSettings _config;
        private readonly ILogger<SmtpEmailSenderService> _logger;

        public SmtpEmailSenderService(IOptions<SmtpSettings> options, ILogger<SmtpEmailSenderService> logger)
        {
            _config = options.Value ?? new SmtpSettings();
            _logger = logger;
        }

        public async Task<bool> EnviarAsync(string destinatario, string assunto, string corpo, bool isHtml, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(_config.Host) || string.IsNullOrWhiteSpace(_config.FromEmail))
            {
                _logger.LogWarning("SMTP não configurado (Host/From vazio). E-mail para {To} não enviado.", destinatario);
                return false;
            }

            if (string.IsNullOrWhiteSpace(destinatario) || !destinatario.Contains('@'))
            {
                _logger.LogWarning("Destinatário de e-mail inválido.");
                return false;
            }

            var from = string.IsNullOrWhiteSpace(_config.User) ? _config.FromEmail : _config.User;

            using var message = new MailMessage
            {
                From = new MailAddress(_config.FromEmail, _config.FromName, Encoding.UTF8),
                Subject = assunto,
                Body = corpo,
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8,
                IsBodyHtml = isHtml
            };
            message.To.Add(destinatario);

            using var client = new SmtpClient(_config.Host, _config.Port)
            {
                EnableSsl = _config.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            if (!string.IsNullOrEmpty(_config.User))
                client.Credentials = new NetworkCredential(_config.User, _config.Password);

            try
            {
                await client.SendMailAsync(message, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao enviar e-mail para {To}", destinatario);
                return false;
            }
        }
    }
}
