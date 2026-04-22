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
            var service = services.GetService<IEstacionamentoService>();

            if (!await context.Estacionamento.AnyAsync(x => x.Descricao == "GTS Estacionamento"))
                  await service.Gravar(SeedPadrao());
        }

        private EstacionamentoPostInput SeedPadrao()
        {
            return new EstacionamentoPostInput
            {
                Id = 0,
                Descricao = "GTS Estacionamento",
                PessoaId = 0,
                CapacidadeVeiculo = 15,
                TamanhoTerreno = "105",
                ResposanvelLegal = "Jean",
                ResponsavelCpf = "01423489020",
                PossuiSeguranca = true,
                PossuiBanheiro = true,
                TipoCobranca = TipoCobranca.Gratuito,
                CobrancaPorcentagem = 0,
                CobrancaValor = 0,
                ContaBancaria = new List<ContaBancariaInput>(),
                Pessoa = new PessoaInput
                {
                    Id = 0,
                    TipoPessoa = Domain.Models.Enuns.TipoPessoa.Fisica,
                    NomeRazaoSocial = "Gt Sistemas LTDA",
                    NomeFantasia = "Gt Sistemas",
                    Documento = "73296098000140",
                    Email = "admin@gtsistema.com",
                    Ativo = true,
                    Enderecos = new List<PessoaEnderecoInput>
                    {
                        new PessoaEnderecoInput
                        {
                            Principal = true,
                            TipoEndereco = TipoEndereco.Residencial,
                            Cep = "78051020",
                            Logradouro = "Rua Sete Copa",
                            Numero = "21",
                            Complemento = "A",
                            Bairro = "Centro",
                            Cidade = "Cuiabá",
                            Estado = "MT"
                        }
                    },
                    Contatos = new List<PessoaContatoInput>
                    {
                        new PessoaContatoInput
                        {
                            Principal = true,
                            TipoContato = (TipoContato)1,
                            Numero = "65990112121",
                            Observacao = "contato"
                        }
                    }
                },
            };
        }
    }
}
