using Estac.Domain.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estac.Infra.EntityBuilders.User
{
    public class PermissionMapping : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Acao)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.SubModuleId)
                .IsRequired();

            builder.HasOne(x => x.SubModule)
                .WithMany(x => x.Permissions)
                .HasForeignKey(x => x.SubModuleId);
        }
    }
}
