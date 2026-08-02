using Estac.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class VeiculoMotoristaMapping : IEntityTypeConfiguration<VeiculoMotorista>
    {
        public void Configure(EntityTypeBuilder<VeiculoMotorista> builder)
        {
            builder.ToTable("VeiculoMotoristas", "gts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.MotoristaId)
                .IsRequired();

            builder.Property(x => x.VeiculoId)
                .IsRequired();

            builder.Property(x => x.Principal)
                .HasColumnType("bit");

            builder.HasOne(x => x.Motorista)
                .WithMany(m => m.VeiculoMotoristas)
                .HasForeignKey(x => x.MotoristaId)
                .HasConstraintName("FK_VeiculoMotoristas_Motorista")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Veiculo)
                .WithMany(v => v.VeiculoMotoristas)
                .HasForeignKey(x => x.VeiculoId)
                .HasConstraintName("FK_VeiculoMotoristas_Veiculo")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.MotoristaId, x.VeiculoId })
                .IsUnique();
        }
    }
}
