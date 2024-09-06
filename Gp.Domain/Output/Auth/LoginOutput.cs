
namespace Gp.Domain.Output.Auth
{
    public class LoginOutput
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string NomeCompleto { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public TokenResponse Jwt { get; set; }
    }

    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTimeOffset ExpiresIn { get; set; }
        public long TimeInMiliseconds { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }

    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTimeOffset ExpiresIn { get; set; }
        public long TimeInMiliseconds { get; set; }
    }
}
