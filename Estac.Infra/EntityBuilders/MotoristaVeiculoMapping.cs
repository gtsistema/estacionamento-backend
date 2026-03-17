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
    public class MotoristaVeiculoMapping : IEntityTypeConfiguration<MotoristaVeiculo>
    {
        public void Configure(EntityTypeBuilder<MotoristaVeiculo> builder)
        {
            builder.ToTable("MotoristaVeiculo", "gts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.MotoristaId)
                   .IsRequired();

            builder.Property(x => x.VeiculoId)
                   .IsRequired();

            // Relacionamento com Motorista
            builder.HasOne(x => x.Motorista)
                   .WithMany() // ou .WithMany(m => m.MotoristaVeiculos)
                   .HasForeignKey(x => x.MotoristaId)
                   .HasConstraintName("FK_MotoristaVeiculo_Motorista")
                   .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com Veiculo
            builder.HasOne(x => x.Veiculo)
                   .WithMany() // ou .WithMany(v => v.MotoristaVeiculos)
                   .HasForeignKey(x => x.VeiculoId)
                   .HasConstraintName("FK_MotoristaVeiculo_Veiculo")
                   .OnDelete(DeleteBehavior.Restrict);

            // Evita duplicidade Motorista + Veiculo
            builder.HasIndex(x => new { x.MotoristaId, x.VeiculoId })
                   .IsUnique();
        }
    }
}
