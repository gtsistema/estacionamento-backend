using Estac.Domain.Auth;
using Estac.Domain.Models;
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

            modelBuilder.Entity<Despesa>(new DespesaMapping().Configure);
            modelBuilder.Entity<DespesaLancamento>(new DespesaLancamentoMapping().Configure);
            modelBuilder.Entity<Receita>(new ReceitaMapping().Configure);
            modelBuilder.Entity<ReceitaLancamento>(new ReceitaLancamentoMapping().Configure);
            modelBuilder.Entity<Orcamento>(new OrcamentoMapping().Configure);
            modelBuilder.Entity<Livro>(new LivroMapping().Configure);
        }

        public DbSet<Despesa> Despesa { get; set; }
        public DbSet<DespesaLancamento> DespesaLancamento { get; set; }
        public DbSet<Receita> Receita { get; set; }
        public DbSet<ReceitaLancamento> ReceitaLancamento { get; set; }
        public DbSet<Orcamento> Orcamento { get; set; }
        public DbSet<Livro> Livro { get; set; }
    }
}
