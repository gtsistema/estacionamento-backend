using Estac.Domain.Auth;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Infra.EntityBuilders;
using Estac.Infra.EntityBuilders.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Estac.Infra.Context
{
    public class GtsContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public GtsContext(DbContextOptions<GtsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<ApplicationUser>(new UserMapping().Configure);
            modelBuilder.Entity<ApplicationRole>(new RoleMapping().Configure);
            modelBuilder.Entity<IdentityUserRole<Guid>>(new UserRoleMapping().Configure);
            modelBuilder.Entity<IdentityUserToken<Guid>>(new UserTokenMapping().Configure);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>(new RoleClaimMapping().Configure);
            modelBuilder.Entity<IdentityUserClaim<Guid>>(new UserClaimMapping().Configure);
            modelBuilder.Entity<IdentityUserLogin<Guid>>(new UserLoginMapping().Configure);

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
            modelBuilder.Entity<MotoristaVeiculo>(new MotoristaVeiculoMapping().Configure);
            modelBuilder.Entity<Vaga>(new VagaMapping().Configure);
            modelBuilder.Entity<VagaVeiculo>(new VagaVeiculoMapping().Configure);

            // ESTACIONAMENTO
            modelBuilder.Entity<Estacionamento>(new EstacionamentoMapping().Configure);

        }

        public DbSet<MotoristaVeiculo> MotoristaVeiculo { get; set; }
        public DbSet<Vaga> Vaga { get; set; }
        public DbSet<VagaVeiculo> VagaVeiculo { get; set; }

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

    }
}
