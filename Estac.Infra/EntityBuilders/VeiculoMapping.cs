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
            builder.ToTable("Veiculo", "gts");

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

            builder.Property(v => v.TipoCarga)
                .HasConversion<byte?>()
                .HasColumnType("tinyint")
                .IsRequired(false);

            builder.Property(v => v.VeiculoModeloId)
                .HasColumnType("int");

            builder.Property(v => v.VeiculoDetalheId)
                .HasColumnType("int");

            builder.Property(v => v.TransportadoraId)
                .IsRequired(false);

            builder.Property(v => v.MotoristaId)
                .IsRequired(false);

            builder.HasOne(v => v.VeiculoModelo)
                .WithMany()
                .HasForeignKey(v => v.VeiculoModeloId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.VeiculoDetalhe)
                .WithMany()
                .HasForeignKey(v => v.VeiculoDetalheId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.Transportadora)
                .WithMany(t => t.Veiculos)
                .HasForeignKey(v => v.TransportadoraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.Motorista)
                .WithOne(m => m.Veiculo)
                .HasForeignKey<Veiculo>(v => v.MotoristaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(v => v.MotoristaId)
                .IsUnique();
        }
    }
}
