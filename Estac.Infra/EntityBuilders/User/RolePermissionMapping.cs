using Estac.Domain.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders.User
{
    public class RolePermissionMapping : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermission", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SubModuleId);

            builder.Property(x => x.ModuleId);

            builder.HasOne(v => v.Role)
           .WithMany(r => r.RolePermissions) // <-- aqui está o pulo do gato
           .HasForeignKey(v => v.RoleId)
           .HasPrincipalKey(r => r.Id);

            builder.HasOne(v => v.Permission)
               .WithMany()
               .HasForeignKey(v => v.PermissionId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.SubModule)
               .WithMany()
               .HasForeignKey(v => v.SubModuleId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Module)
                .WithMany()
                .HasForeignKey(x => x.ModuleId)
                .HasConstraintName("FK_RolePermission_Module_ModuleId");
        }
    }
}
