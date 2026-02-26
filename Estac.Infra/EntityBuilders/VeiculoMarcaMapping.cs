using Estac.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estac.Infra.EntityBuilders
{
    public class VeiculoMarcaMapping : IEntityTypeConfiguration<VeiculoMarca>
    {
        public void Configure(EntityTypeBuilder<VeiculoMarca> builder)
        {
            builder.ToTable("VEICULOMARCA");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Descricao)
              .HasColumnName("Descricao")
              .HasColumnType("varchar(150)")
              .HasMaxLength(150);

            builder.HasIndex(v => v.Id);
        }
    }
}
