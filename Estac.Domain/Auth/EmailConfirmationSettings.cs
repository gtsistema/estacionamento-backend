namespace Estac.Domain.Auth
{
    public class EmailConfirmationSettings
    {
        /// <summary>URL do SPA (ex.: http://localhost:4200)</summary>
        public string FrontendBaseUrl { get; set; }

        /// <summary>Rota no front (ex.: /auth/confirmar-email)</summary>
        public string ConfirmarEmailPath { get; set; } = "/auth/confirmar-email";

        /// <summary>Rota no front para redefinição de senha (ex.: /auth/redefinir-senha)</summary>
        public string RedefinirSenhaPath { get; set; } = "/auth/redefinir-senha";

        /// <summary>Se true, o POST de registro retorna o link (útil em dev; em prod prefira e-mail). </summary>
        public bool IncluirLinkNaRespostaDoCadastro { get; set; } = true;

        /// <summary>Se true, esqueci-senha retorna o link na API (dev); em prod use só e-mail.</summary>
        public bool IncluirLinkNaRespostaEsqueciSenha { get; set; } = true;
    }
}
