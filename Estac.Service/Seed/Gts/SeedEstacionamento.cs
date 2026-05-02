using Estac.Domain.Input.ContaBancaria;
using Estac.Domain.Input.Endereco;
using Estac.Domain.Input.Estacionamento;
using Estac.Domain.Input.Pessoa;
using Estac.Domain.Input.PessoaContato;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models.Enuns;
using Estac.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Estac.Service.Seed
{
    public class SeedEstacionamento
    {
        public async Task SeedAsync(IServiceProvider services, GtsContext context)
        {
           await Gravar(services, context);
        }

        private async Task Gravar(IServiceProvider services, GtsContext context)
        {
            //var service = services.GetService<IEstacionamentoService>();

            //if (!await context.Estacionamento.AnyAsync(x => x.Descricao == "GTS Estacionamento"))
            //      await service.Gravar(SeedPadrao());
        }

        private EstacionamentoPostInput SeedPadrao()
        {
            return new EstacionamentoPostInput()
            {
            };
        }
    }
}
