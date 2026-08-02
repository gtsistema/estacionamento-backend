using Estac.Domain.Extensions;
using Estac.Domain.Input.Auth;
using Estac.Domain.Input.ContaBancaria;
using Estac.Domain.Input.Endereco;
using Estac.Domain.Input.Estacionamento;
using Estac.Domain.Input.Pessoa;
using Estac.Domain.Interface.Services.Auth;
using Estac.Domain.Models.Auth;
using Estac.Infra.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
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
                await CriarUsuarioSeNaoExistir(context, userService, services, usuario);
            }
        }

        private static async Task CriarUsuarioSeNaoExistir(IdentityContext context, IUserServices userService, IServiceProvider services, RegisterInput dto)
        {
            if (context.Users.Any(x => x.UserName == dto.UserName))
                return;

            await userService.RegisterAsync(dto);

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByNameAsync(dto.UserName);
            if (user is null) return;
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmar = await userManager.ConfirmEmailAsync(user, token);
            if (!confirmar.Succeeded)
            {
                var msg = string.Join(" ", confirmar.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Falha ao confirmar e-mail do usuário seed {dto.UserName}: {msg}");
            }
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
                Pessoa = new PessoaUsuarioInput
                {
                    Nome = "Administrador",
                    Cpf = StringExtentions.GerarCpf(),
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
