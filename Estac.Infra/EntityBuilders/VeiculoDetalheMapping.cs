using Estac.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    class VeiculoDetalheMapping : IEntityTypeConfiguration<VeiculoDetalhe>
    {
        public void Configure(EntityTypeBuilder<VeiculoDetalhe> builder)
        {
            builder.ToTable("VEICULODETALHE");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Descricao)
            .HasColumnName("Descricao")
            .HasColumnType("varchar(150)")
            .HasMaxLength(150);

            builder.Property(v => v.Uf)
            .HasColumnType("varchar(2)")
            .HasMaxLength(2);

            builder.Property(v => v.VeiculoId)
            .HasColumnType("int");

            builder.Property(v => v.NomeProprietario)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150);

            builder.Property(v => v.CpfCnpjProprietario)
                .HasColumnType("varchar(18)")
                .HasMaxLength(18);

            builder.Property(v => v.KmAtual)
                .HasColumnType("decimal(10,2)");

            builder.Property(v => v.KmRastreador)
                .HasColumnType("decimal(10,2)");

            builder.Property(v => v.CapacidadeCombustivel)
                .HasColumnType("decimal(7,2)");

            builder.Property(v => v.CapacidadeArla)
                .HasColumnType("decimal(7,2)");

            builder.Property(v => v.MediaMinima)
                .HasColumnName("MEDIAMINIMA")
                .HasColumnType("decimal(5,2)");

            builder.Property(v => v.MediaMaxima)
                .HasColumnType("decimal(5,2)");

            builder.Property(v => v.InscricaoEstadual)
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            builder.Property(v => v.VeiculoTerceiro)
                .HasColumnType("bit")
                .HasDefaultValue(false);

            builder.Property(v => v.Observacoes)
                .HasColumnType("varchar(500)")
                .HasMaxLength(500);

            builder.HasOne(v => v.Veiculo)
                .WithOne(v => v.VeiculoDetalhe)
                .HasForeignKey<VeiculoDetalhe>(v => v.VeiculoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(v => v.VeiculoId)
                .IsUnique();
        }
    }
}
