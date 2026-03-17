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
    public class VeiculoModeloMapping : IEntityTypeConfiguration<VeiculoModelo>
    {
        public void Configure(EntityTypeBuilder<VeiculoModelo> builder)
        {
            builder.ToTable("VeiculoModelo", "gts");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Descricao)
              .HasColumnName("Descricao")
              .HasColumnType("varchar(150)")
              .HasMaxLength(150);

            builder.Property(v => v.VeiculoMarcaId)
                .HasColumnType("int")
                .IsRequired();

            builder.HasOne(v => v.VeiculoMarca)
                .WithMany()
                .HasForeignKey(v => v.VeiculoMarcaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(v => v.VeiculoMarcaId);
        }
    }
}
