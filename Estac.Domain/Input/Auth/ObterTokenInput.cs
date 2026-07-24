namespace Estac.Domain.Input.Auth
{
    /// <summary>
    /// Autenticação alternativa: UserName+Password <b>ou</b> Secret (igual a BearerTokenSettings.Secret).
    /// </summary>
    public class ObterTokenInput
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        /// <summary>Se informado e igual a BearerTokenSettings.Secret, libera o token sem usuário.</summary>
        public string Secret { get; set; }
    }
}
