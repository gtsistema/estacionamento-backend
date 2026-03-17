using Estac.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class VeiculoPlacaMapping : IEntityTypeConfiguration<VeiculoPlaca>
    {
        public void Configure(EntityTypeBuilder<VeiculoPlaca> builder)
        {
            builder.ToTable("VeiculoPlaca", "gts");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Descricao)
                .HasColumnType("varchar(8)")
                .HasMaxLength(8)
                .IsRequired();

            builder.Property(v => v.VeiculoId)
                .HasColumnType("int")
                .IsRequired();

            builder.HasOne(v => v.Veiculo)
                .WithMany()
                .HasForeignKey(v => v.VeiculoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(v => v.VeiculoId);
        }
    }
}
