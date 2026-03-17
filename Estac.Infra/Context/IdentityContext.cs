using Estac.Domain.Auth;
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
    public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //User
            modelBuilder.Entity<ApplicationUser>(new UserMapping().Configure);
            modelBuilder.Entity<ApplicationRole>(new RoleMapping().Configure);
            modelBuilder.Entity<IdentityUserRole<int>>(new UserRoleMapping().Configure);
            modelBuilder.Entity<IdentityUserToken<int>>(new UserTokenMapping().Configure);
            modelBuilder.Entity<IdentityRoleClaim<int>>(new RoleClaimMapping().Configure);
            modelBuilder.Entity<IdentityUserClaim<int>>(new UserClaimMapping().Configure);
            modelBuilder.Entity<IdentityUserLogin<int>>(new UserLoginMapping().Configure);

            //PERMISSOES
            modelBuilder.Entity<Module>(new ModuleMapping().Configure);
            modelBuilder.Entity<Permission>(new PermissionMapping().Configure);
            modelBuilder.Entity<SubModule>(new SubModuleMapping().Configure);
            modelBuilder.Entity<UserPermission>(new UserPermissionMapping().Configure);
        }
    }
}