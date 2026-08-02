namespace Estac.Domain.Interface.Services
{
    public interface IEmailSenderService
    {
        Task<bool> EnviarAsync(string destinatario, string assunto, string corpo, bool isHtml, CancellationToken cancellationToken = default);
    }
}
