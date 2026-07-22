using Estac.Domain.Models;
using Estac.Domain.Models.Auth;
using Estac.Domain.Models.Enuns;
using Estac.Infra.EntityBuilders;
using Estac.Infra.EntityBuilders.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Estac.Infra.Context
{
    public class GtsContext : DbContext
    {
        public GtsContext(DbContextOptions<GtsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //VEICULO
            modelBuilder.Entity<VeiculoModelo>(new VeiculoModeloMapping().Configure);
            modelBuilder.Entity<Veiculo>(new VeiculoMapping().Configure);
            modelBuilder.Entity<VeiculoMarca>(new VeiculoMarcaMapping().Configure);
            modelBuilder.Entity<VeiculoDetalhe>(new VeiculoDetalheMapping().Configure);
            modelBuilder.Entity<VeiculoPlaca>(new VeiculoPlacaMapping().Configure);

            //PESSOA
            modelBuilder.Entity<Pessoa>(new PessoaMapping().Configure);
            modelBuilder.Entity<PessoaEndereco>(new PessoaEnderecoMapping().Configure);
            modelBuilder.Entity<PessoaPapel>(new PessoaPapelMapping().Configure);
            modelBuilder.Entity<PessoaContato>(new PessoaContatoMapping().Configure);

            //MOTORISTA
            modelBuilder.Entity<Motorista>(new MotoristaMapping().Configure);
            modelBuilder.Entity<VeiculoMotorista>(new VeiculoMotoristaMapping().Configure);
            modelBuilder.Entity<Vaga>(new VagaMapping().Configure);
            modelBuilder.Entity<VagaVeiculo>(new VagaVeiculoMapping().Configure);

            //TRANSPORTADORA
            modelBuilder.Entity<Transportadora>(new TransportadoraMapping().Configure);

            // MOVIMENTO
            modelBuilder.Entity<EntradaSaida>(new EntradaSaidaMapping().Configure);
            modelBuilder.Entity<EntradaSaidaSuspensao>(new EntradaSaidaSuspensaoMapping().Configure);

            // ESTACIONAMENTO
            modelBuilder.Entity<Estacionamento>(new EstacionamentoMapping().Configure);

            modelBuilder.Entity<EstacionamentoFoto>(new EstacionamentoFotoMapping().Configure);
            modelBuilder.Entity<ContaBancaria>(new ContaBancariaMapping().Configure);

            // FINANCEIRO
            modelBuilder.Entity<ConfiguracaoCobranca>(new ConfiguracaoCobrancaMapping().Configure);
            modelBuilder.Entity<ConfiguracaoCobrancaRegra>(new ConfiguracaoCobrancaRegraMapping().Configure);
            modelBuilder.Entity<Fatura>(new FaturaMapping().Configure);

        }

        public DbSet<VeiculoMotorista> VeiculoMotoristas { get; set; }
        public DbSet<Vaga> Vaga { get; set; }
        public DbSet<VagaVeiculo> VagaVeiculo { get; set; }
        public DbSet<EntradaSaida> EntradaSaida { get; set; }
        public DbSet<EntradaSaidaSuspensao> EntradaSaidaSuspensao { get; set; }

        //VEICULO
        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<VeiculoDetalhe> VeiculoDetalhe { get; set; }
        public DbSet<VeiculoMarca> VeiculoMarca { get; set; }
        public DbSet<VeiculoModelo> VeiculoModelo { get; set; }
        public DbSet<VeiculoPlaca> VeiculoPlaca { get; set; }

        // PESSOA
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<PessoaEndereco> PessoaEndereco { get; set; }
        public DbSet<PessoaPapel> PessoalPapel { get; set; }
        public DbSet<PessoaContato> PessoaContato { get; set; }
        public DbSet<Motorista> Motorista { get; set; }

        // ESTACIONAMENTO
        public DbSet<Estacionamento> Estacionamento { get; set; }
        public DbSet<EstacionamentoFoto> EstacionamentoFoto { get; set; }
        public DbSet<ContaBancaria> ContaBancaria { get; set; }
        public DbSet<Transportadora> Transportadora { get; set; }

        // FINANCEIRO
        public DbSet<ConfiguracaoCobranca> ConfiguracaoCobranca { get; set; }
        public DbSet<ConfiguracaoCobrancaRegra> ConfiguracaoCobrancaRegra { get; set; }
        public DbSet<Fatura> Fatura { get; set; }

    }
}
