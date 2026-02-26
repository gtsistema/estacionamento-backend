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
    public class VeiculoMapping : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.ToTable("VEICULO");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Descricao)
               .HasColumnType("varchar(150)")
               .HasMaxLength(150);

            builder.Property(v => v.Placa)
                .HasColumnType("varchar(8)")
                .HasMaxLength(8)
                .IsRequired();

            builder.Property(v => v.Ano)
                .HasColumnType("int")
                .IsRequired(false);

            builder.Property(v => v.Ativo)
                .HasColumnType("bit")
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(v => v.Cor)
                .HasColumnType("varchar(30)")
                .HasMaxLength(30)
                .IsRequired(false);

            builder.Property(v => v.VeiculoModeloId)
                .HasColumnType("int");

            builder.Property(v => v.VeiculoDetalheId)
                .HasColumnType("int");

            builder.HasOne(v => v.VeiculoModelo)
                .WithMany()
                .HasForeignKey(v => v.VeiculoModeloId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.VeiculoDetalhe)
                .WithMany()
                .HasForeignKey(v => v.VeiculoDetalheId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
