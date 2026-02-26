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
    public class VagaVeiculoMapping : IEntityTypeConfiguration<VagaVeiculo>
    {
        public void Configure(EntityTypeBuilder<VagaVeiculo> builder)
        {
            builder.ToTable("VagaVeiculo");

            builder.Property(v => v.Descricao)
            .HasColumnName("Descricao")
            .HasColumnType("varchar(150)")
            .HasMaxLength(150);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.VagaId)
                   .IsRequired();

            builder.Property(x => x.VeiculoId)
                   .IsRequired();

            builder.Property(x => x.DataEntrada)
                   .IsRequired();

            builder.Property(x => x.DataSaida)
                   .IsRequired(false);

            builder.HasOne(x => x.Vaga)
                .WithMany(v => v.VagaVeiculos)
                .HasForeignKey(x => x.VagaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com Veiculo
            builder.HasOne(x => x.Veiculo)
                   .WithMany() // ou .WithMany(v => v.VagaVeiculos)
                   .HasForeignKey(x => x.VeiculoId)
                   .HasConstraintName("FK_VagaVeiculo_Veiculo")
                   .OnDelete(DeleteBehavior.Restrict);

            // Índices importantes para performance
            builder.HasIndex(x => x.VagaId);
            builder.HasIndex(x => x.VeiculoId);
            builder.HasIndex(x => x.DataEntrada);
        }
    }
}
