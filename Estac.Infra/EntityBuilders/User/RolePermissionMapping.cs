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

            builder.Property(x => x.RoleId)
                .IsRequired();

            builder.Property(x => x.PermissionId)
                .IsRequired();

            builder.HasIndex(x => new { x.RoleId, x.PermissionId })
                .IsUnique();

            builder.HasOne(v => v.Role)
               .WithMany()
               .HasForeignKey(v => v.RoleId);

            builder.HasOne(v => v.Permission)
               .WithMany()
               .HasForeignKey(v => v.PermissionId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.SubModule)
               .WithMany()
               .HasForeignKey(v => v.SubModuleId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
