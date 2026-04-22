using Estac.Domain.Extensions;
using Estac.Domain.Input.Auth;
using Estac.Domain.Input.ContaBancaria;
using Estac.Domain.Input.Endereco;
using Estac.Domain.Input.Estacionamento;
using Estac.Domain.Input.Pessoa;
using Estac.Domain.Interface.Services.Auth;
using Estac.Domain.Models.Auth;
using Estac.Infra.Context;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;

namespace Estac.Service.Seed.Identity
{
    public class SeedUsuario
    {
        public async Task ExecuteAsync(IServiceProvider services, IdentityContext context)
        {
           await Gravar(services, context);
        }

        private async Task Gravar(IServiceProvider services, IdentityContext context)
        {
            var userService = services.GetRequiredService<IUserServices>();

            var usuarios = ObterUserNames();

            foreach (var usuario in usuarios)
            {
                await CriarUsuarioSeNaoExistir(context, userService, usuario);
            }
        }

        private static async Task CriarUsuarioSeNaoExistir(IdentityContext context, IUserServices userService, RegisterInput dto)
        {
            if (context.Users.Any(x => x.UserName == dto.UserName))
                return;

            await userService.RegisterAsync(dto);
        }

        private static RegisterInput CriarUsuarioAdmin(string userName)
        {
            return new RegisterInput
            {
                UserName = userName,
                Password = "@Admin2134",
                ConfirmPassword = "@Admin2134",
                Email = $"{userName}@email.com",
                EstacionamentoId = 1,
                Pessoa = new PessoaUsuarioImput
                {
                    Nome = "Administrador",
                    Documento = StringExtentions.GerarCpf(),
                    TipoPessoa = Domain.Models.Enuns.TipoPessoa.Fisica
                },
                Perfil = new ApplicationRole
                {
                    Name = "Admin"
                }
            };
        }

        private static List<RegisterInput> ObterUserNames()
        {
            return new List<RegisterInput>
            {
                CriarUsuarioAdmin("jean.correa"),
                CriarUsuarioAdmin("alex.penna")
            };
        }
    }
}
