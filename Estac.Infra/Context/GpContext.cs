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
    public class GpContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public GpContext(DbContextOptions<GpContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<ApplicationUser>(new UserMapping().Configure);
            modelBuilder.Entity<IdentityRole<Guid>>(new RoleMapping().Configure);
            modelBuilder.Entity<IdentityUserRole<Guid>>(new UserRoleMapping().Configure);
            modelBuilder.Entity<IdentityUserToken<Guid>>(new UserTokenMapping().Configure);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>(new RoleClaimMapping().Configure);
            modelBuilder.Entity<IdentityUserClaim<Guid>>(new UserClaimMapping().Configure);
            modelBuilder.Entity<IdentityUserLogin<Guid>>(new UserLoginMapping().Configure);


            modelBuilder.Entity<PessoaTelefone>()
                .HasOne(t => t.Pessoa)
                .WithMany(p => p.Telefones)
                .HasForeignKey(t => t.PessoaId);

            modelBuilder.Entity<PessoaEndereco>()
                .HasOne(e => e.Pessoa)
                .WithMany(p => p.Enderecos)
                .HasForeignKey(e => e.PessoaId);

            modelBuilder.Entity<PessoaPapel>()
                .HasOne(e => e.Pessoa)
                .WithMany(p => p.Papeis)
                .HasForeignKey(e => e.PessoaId);

            modelBuilder.Entity<Motorista>()
                .HasOne(x => x.Pessoa)
                .WithMany()
                .HasForeignKey(x => x.PessoaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Motorista>()
                .HasOne(x => x.Pessoa)
                .WithMany()
                .HasForeignKey(x => x.PessoaId);

            modelBuilder.Entity<MotoristaVeiculo>()
                .HasOne(x => x.Motorista)
                .WithMany()
                .HasForeignKey(x => x.MotoristaId);

            modelBuilder.Entity<MotoristaVeiculo>()
               .HasOne(x => x.Veiculo)
               .WithMany()
               .HasForeignKey(x => x.VeiculoId);

        }

        public DbSet<Motorista> Motorista { get; set; }
        public DbSet<MotoristaVeiculo> MotoristaVeiculo { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<PessoaEndereco> PessoaEndereco { get; set; }
        public DbSet<PessoaPapel> PessoalPapel { get; set; }
        public DbSet<PessoaTelefone> PessoaTelefone { get; set; }
        //public DbSet<Vaga> Vaga { get; set; }
        //public DbSet<VagaVeiculo> VagaVeiculo { get; set; }
        //public DbSet<Veiculo> Veiculo { get; set; }
        //public DbSet<VeiculoDetalhe> VeiculoDetalhe { get; set; }
        //public DbSet<VeiculoMarca> VeiculoMarca { get; set; }
        //public DbSet<VeiculoModelo> VeiculoModelo { get; set; }
        //public DbSet<VeiculoPlaca> VeiculoPlaca { get; set; }
    }
}
