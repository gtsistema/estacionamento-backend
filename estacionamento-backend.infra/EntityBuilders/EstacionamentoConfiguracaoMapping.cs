using Estac.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class EstacionamentoConfiguracaoMapping : IEntityTypeConfiguration<EstacionamentoConfiguracao>
    {
        public void Configure(EntityTypeBuilder<EstacionamentoConfiguracao> builder)
        {
            builder.ToTable("EstacionamentoConfiguracao", "gts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.EstacionamentoId)
                   .IsRequired();

            builder.Property(x => x.TimeZoneId)
                   .HasColumnType("varchar(64)")
                   .HasMaxLength(64)
                   .IsRequired();

            builder.Property(x => x.Cultura)
                   .HasColumnType("varchar(16)")
                   .HasMaxLength(16)
                   .HasDefaultValue("pt-BR")
                   .IsRequired();

            builder.Property(x => x.Ativo)
                   .HasDefaultValue(true)
                   .IsRequired();

            builder.Property(x => x.DataCriacao)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            builder.Property(x => x.DataAtualizacao)
                   .IsRequired(false);

            builder.HasOne(x => x.Estacionamento)
                   .WithOne(x => x.Configuracao)
                   .HasForeignKey<EstacionamentoConfiguracao>(x => x.EstacionamentoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.EstacionamentoId)
                   .IsUnique()
                   .HasDatabaseName("IX_EstacionamentoConfiguracao_EstacionamentoId");
        }
    }
}
