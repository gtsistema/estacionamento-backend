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
    public class SubModuleMapping : IEntityTypeConfiguration<SubModule>
    {
        public void Configure(EntityTypeBuilder<SubModule> builder)
        {
            builder.ToTable("SubModule", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.ModuleId)
                .IsRequired();
        }
    }
}
