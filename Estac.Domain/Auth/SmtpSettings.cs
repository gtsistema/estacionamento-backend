namespace Estac.Domain.Auth
{
    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; } = 587;
        public bool EnableSsl { get; set; } = true;
        public string User { get; set; }
        public string Password { get; set; }
        public string FromEmail { get; set; } = "admin@gtsistema.com";
        public string FromName { get; set; } = "GTS Sistema";
    }
}
