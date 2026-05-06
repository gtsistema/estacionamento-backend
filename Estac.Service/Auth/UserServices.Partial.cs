using Estac.Domain.Extensions;
using Estac.Domain.Input.Auth;
using Estac.Domain.Input.Pessoa;
using Estac.Domain.Models;
using Estac.Domain.Models.Auth;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output;
using Estac.Domain.Output.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Estac.Service.Auth
{
    public partial class UserServices
    {
        private const string AssuntoEmailConfirmacao = "Confirme seu e-mail - GTS Sistema";
        private const string AssuntoEsqueciSenha = "Redefinição de senha - GTS Sistema";
        private const string MsgEsqueciSenhaGenerica =
            "Se existir uma conta com este e-mail, você receberá instruções para redefinir a senha.";
        private const string MsgEmailNaoConfirmado =
            "Confirme o e-mail antes de entrar. Verifique a caixa de entrada ou solicite um novo link.";

        private static string PrimeiroErroCadastro(ApplicationIdentityResult result) =>
            result.Errors.Count > 0
                ? result.Errors[0]
                : "Não foi possível criar o usuário.";

        private static string MensagemErrosIdentity(IdentityResult result) =>
            string.Join(", ", result.Errors.Select(e => e.Description));

        private async Task<ActionResult> RespostaLoginFalhouAsync(string userName, Microsoft.AspNetCore.Identity.SignInResult signIn)
        {
            if (signIn.IsNotAllowed)
            {
                var u = await _identityUserManager.FindByNameAsync(userName);
                if (u != null && !u.EmailConfirmed)
                    return await RetornNo(false, MsgEmailNaoConfirmado);
            }
            return await RetornNo(false, Resources.Resources.MSG_Usuario_Ou_Senha_Invalida);
        }

        private static string ValidarInputAlteracao(int id, RegisterInput input)
        {
            if (id <= 0) return "Id de usuário inválido.";
            if (input is null) return "Dados para alteração do usuário são obrigatórios.";
            if (input.Pessoa is null) return "Dados de pessoa são obrigatórios.";
            if (input.Perfil is null || string.IsNullOrWhiteSpace(input.Perfil.Name)) return "Perfil é obrigatório.";
            if (!string.IsNullOrEmpty(input.Password) && input.Password != input.ConfirmPassword)
                return "Confirmação de senha não confere.";
            return null;
        }

        private async Task<Pessoa> SincronizarPessoaDoUsuarioAsync(ApplicationUser user, RegisterInput input)
        {
            if (user.PessoaId is int pessoaIdVinculo && pessoaIdVinculo > 0)
            {
                var pessoa = await _pessoaRepositories.SelecionarIdSimplesAsync(pessoaIdVinculo);
                if (pessoa is null) return null;
                _mapper.Map(input.Pessoa, pessoa);
                pessoa.Ativo = true;
                await _pessoaRepositories.Alterar(pessoa);
                return pessoa;
            }

            var nova = _mapper.Map<Pessoa>(input.Pessoa);
            nova.Ativo = true;
            nova.AdicionarPapel(TipoPapel.Funcionario);
            await _pessoaRepositories.Gravar(nova);
            user.PessoaId = nova.Id;
            return nova;
        }

        private async Task<ActionResult> AtualizarUsuarioIdentityAsync(
            ApplicationUser user,
            RegisterInput input,
            Pessoa pessoa)
        {
            var setName = await _identityUserManager.SetUserNameAsync(user, input.UserName);
            if (!setName.Succeeded)
                return await RetornNo(false, MensagemErrosIdentity(setName));

            if (!string.IsNullOrWhiteSpace(input.Email))
            {
                var setEmail = await _identityUserManager.SetEmailAsync(user, input.Email);
                if (!setEmail.Succeeded)
                    return await RetornNo(false, MensagemErrosIdentity(setEmail));
            }

            user.EstacionamentoId = input.EstacionamentoId;
            user.TransportadoraId = input.TransportadoraId;
            user.FullName = pessoa.Descricao;
            user.EmailConfirmed = true;

            var update = await _identityUserManager.UpdateAsync(user);
            if (!update.Succeeded)
                return await RetornNo(false, MensagemErrosIdentity(update));

            return null;
        }

        private async Task<ActionResult> SincronizarPerfilAsync(ApplicationUser user, string nomePerfil)
        {
            var rolesAtuais = await _identityUserManager.GetRolesAsync(user);
            if (rolesAtuais.Contains(nomePerfil, StringComparer.Ordinal))
                return null;

            foreach (var r in rolesAtuais)
            {
                var rem = await _identityUserManager.RemoveFromRoleAsync(user, r);
                if (!rem.Succeeded)
                    return await RetornNo(false, MensagemErrosIdentity(rem));
            }

            var addRole = await _userManager.AddToRoleAsync(user, nomePerfil);
            if (addRole is not null)
                return await RetornNo(false, string.Join(", ", addRole));

            return null;
        }

        private async Task<RegistroUsuarioOutput> MontarRespostaPosRegistroAsync(
            Pessoa pessoa,
            ApplicationUser usuarioAtual)
        {
            var token = await _userManager.GerarTokenDeConfirmacaoDeEmailAsync(usuarioAtual);
            var link = MontarLinkConfirmacaoFrontend(usuarioAtual.Id, token);
            var nomeSaudacao = pessoa.Descricao ?? usuarioAtual.UserName;
            var corpoHtml = MontarCorpoEmailConfirmacao(nomeSaudacao, link);
            var podeEnviar = !string.IsNullOrEmpty(link) && !string.IsNullOrWhiteSpace(usuarioAtual.Email);
            var enviado = podeEnviar && await _emailSender.EnviarAsync(
                usuarioAtual.Email,
                AssuntoEmailConfirmacao,
                corpoHtml,
                isHtml: true);

            if (!enviado)
            {
                _logger.LogWarning(
                    "E-mail de confirmação não enviado para {Email} (verifique Smtp no appsettings).",
                    usuarioAtual.Email);
            }

            return new RegistroUsuarioOutput
            {
                Email = usuarioAtual.Email,
                Mensagem = enviado
                    ? "Cadastro realizado. Enviamos um e-mail de confirmação; verifique a caixa de entrada."
                    : "Cadastro realizado, mas o envio do e-mail de confirmação falhou. Use o link exibido abaixo (se habilitado) ou configure o SMTP e tente reenviar no futuro.",
                LinkConfirmacaoNoFrontend = _emailConfirmation.IncluirLinkNaRespostaDoCadastro || !enviado ? link : null,
                EmailDeConfirmacaoEnviado = enviado
            };
        }

        private string MontarLinkConfirmacaoFrontend(int userId, string token)
        {
            if (string.IsNullOrWhiteSpace(_emailConfirmation.FrontendBaseUrl) || string.IsNullOrWhiteSpace(token))
                return null;
            var path = _emailConfirmation.ConfirmarEmailPath;
            if (string.IsNullOrWhiteSpace(path))
                path = "/auth/confirmar-email";
            if (!path.StartsWith("/"))
                path = "/" + path;
            return $"{_emailConfirmation.FrontendBaseUrl.TrimEnd('/')}{path}?userId={userId}&token={Uri.EscapeDataString(token)}";
        }

        private string MontarLinkRedefinirSenhaFrontend(string email, string token)
        {
            if (string.IsNullOrWhiteSpace(_emailConfirmation.FrontendBaseUrl) ||
                string.IsNullOrWhiteSpace(token) ||
                string.IsNullOrWhiteSpace(email))
                return null;
            var path = _emailConfirmation.RedefinirSenhaPath;
            if (string.IsNullOrWhiteSpace(path))
                path = "/auth/redefinir-senha";
            if (!path.StartsWith("/"))
                path = "/" + path;
            return $"{_emailConfirmation.FrontendBaseUrl.TrimEnd('/')}{path}?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";
        }

        private async Task<ActionResult> ProcessarEsqueciSenhaAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null || user.IsDeleted == true)
            {
                return await RetornOk(
                    new EsqueciSenhaOutput
                    {
                        Mensagem = MsgEsqueciSenhaGenerica,
                        EmailEnviado = false
                    },
                    MsgEsqueciSenhaGenerica);
            }

            var token = await _userManager.GerarTokenDeRecuperacaoDeSenhaAsync(user);
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogWarning("Token de recuperação de senha não gerado para {Email}.", email);
                return await RetornOk(
                    new EsqueciSenhaOutput { Mensagem = MsgEsqueciSenhaGenerica, EmailEnviado = false },
                    MsgEsqueciSenhaGenerica);
            }

            var link = MontarLinkRedefinirSenhaFrontend(email, token);
            var nome = user.FullName ?? user.UserName;
            var corpoHtml = MontarCorpoEmailRedefinirSenha(nome, link);
            var podeEnviar = !string.IsNullOrEmpty(link) && !string.IsNullOrWhiteSpace(user.Email);
            var enviado = podeEnviar && await _emailSender.EnviarAsync(
                user.Email,
                AssuntoEsqueciSenha,
                corpoHtml,
                isHtml: true);

            if (!enviado)
            {
                _logger.LogWarning(
                    "E-mail de redefinição de senha não enviado para {Email} (verifique Smtp e FrontendBaseUrl).",
                    user.Email);
            }

            return await RetornOk(
                new EsqueciSenhaOutput
                {
                    Mensagem = MsgEsqueciSenhaGenerica,
                    LinkRedefinicaoNoFrontend =
                        _emailConfirmation.IncluirLinkNaRespostaEsqueciSenha || !enviado ? link : null,
                    EmailEnviado = enviado
                },
                MsgEsqueciSenhaGenerica);
        }

        private static string MontarCorpoEmailRedefinirSenha(string nome, string linkRedefinicao)
        {
            var nomeSeg = WebUtility.HtmlEncode(nome ?? "usuário");
            var linkSeg = WebUtility.HtmlEncode(linkRedefinicao ?? "");
            return $@"<!DOCTYPE html>
                <html><body>
                <p>Olá, {nomeSeg}.</p>
                <p>Recebemos um pedido para redefinir a senha da sua conta no <strong>GTS Sistema</strong>.</p>
                <p><a href=""{linkSeg}"">Redefinir senha</a></p>
                <p>Se você não solicitou, ignore este e-mail. O link expira após um tempo.</p>
                <p style=""word-break:break-all;font-size:12px"">{linkSeg}</p>
                </body></html>";
        }

        private static string MontarCorpoEmailConfirmacao(string nome, string linkConfirmacao)
        {
            var nomeSeg = WebUtility.HtmlEncode(nome ?? "usuário");
            var linkSeg = WebUtility.HtmlEncode(linkConfirmacao ?? "");
            return $@"<!DOCTYPE html>
                <html><body>
                <p>Olá, {nomeSeg}.</p>
                <p>Confirme seu e-mail para ativar a conta no <strong>GTS Sistema</strong>:</p>
                <p><a href=""{linkSeg}"">Confirmar e-mail</a></p>
                <p>Se o botão não abrir, copie o endereço abaixo no navegador:</p>
                <p style=""word-break:break-all;font-size:12px"">{linkSeg}</p>
                </body></html>";
        }

        private async Task<UsuarioAcessOutput> MontarLoginResponseAsync(ApplicationUser user)
        {
            var permissoes = await _perfilRepositories.BuscarPerfilPorUsuarioToken(user.Id);
            var menus = await _menuRepositories.BuscarMenuUsuarioLogin(permissoes.RoleId);
            var jwt = await GenerateJwtAsync(user);
            return new UsuarioAcessOutput { Jwt = jwt, Menus = menus };
        }

        private async Task<TokenResponse> GenerateJwtAsync(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_bearerTokenSettings.Secret);
            var timeInMs = _bearerTokenSettings.ExpirationInMinutes * 60 * 1000123;
            var expires = DateTime.UtcNow.AddMilliseconds(timeInMs);
            var refreshTimeInMs = _bearerTokenSettings.RefreshExpirationInMinutes * 60 * 1000123;
            var refreshExpires = DateTime.UtcNow.AddMinutes(_bearerTokenSettings.RefreshExpirationInMinutes);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _bearerTokenSettings.Issuer,
                Audience = _bearerTokenSettings.ValidOn,
                Expires = expires,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(await CriarClaimsAsync(user.Id))
            });

            return new TokenResponse
            {
                ExpiresIn = expires,
                TimeInMiliseconds = (long)timeInMs,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = new RefreshToken
                {
                    Token = Guid.NewGuid().ToString().Replace("-", string.Empty),
                    ExpiresIn = refreshExpires,
                    TimeInMiliseconds = (long)refreshTimeInMs
                }
            };
        }

        private async Task<List<Claim>> CriarClaimsAsync(int usuarioId)
        {
            var acesso = await _perfilRepositories.BuscarPerfilPorUsuarioToken(usuarioId);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, acesso.UserId.ToString()),
                new Claim(ClaimTypes.Name, acesso.UserName),
                new Claim(ClaimTypes.Email, acesso.Email ?? ""),
                new Claim(ClaimTypes.Role, acesso.Role),
                new Claim("RoleId", acesso.RoleId.ToString()),
                new Claim("EmpresaId", acesso.EstacionamentoId.HasValue
                    ? acesso.EstacionamentoId?.ToString()
                    : acesso.TransportadoraId?.ToString())
            };
            foreach (var permissao in acesso.Permissions.Select(p => p.Descricao).Distinct())
            {
                if (permissao is not null)
                    claims.Add(new Claim("permission", permissao));
            }
            return claims;
        }
    }
}
