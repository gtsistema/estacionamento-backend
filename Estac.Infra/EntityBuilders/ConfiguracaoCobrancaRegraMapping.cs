using Estac.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class ConfiguracaoCobrancaRegraMapping : IEntityTypeConfiguration<ConfiguracaoCobrancaRegra>
    {
        public void Configure(EntityTypeBuilder<ConfiguracaoCobrancaRegra> builder)
        {
            builder.ToTable("ConfiguracaoCobrancaRegra", "gts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ConfiguracaoCobrancaId)
                   .IsRequired();

            builder.Property(x => x.CobrarDiaria)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.CobrarSemanal)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.CobrarQuinzenal)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.CobrarMensal)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.CobrarDataPersonalizada)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.CobrarLavagem)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.CobrarPernoite)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.CobrarServicosExtras)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.ConsiderarBeneficioAbastecimento)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.Descricao)
                   .HasMaxLength(200);

            builder.Property(x => x.DataCriacao)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.HasIndex(x => x.ConfiguracaoCobrancaId)
                   .IsUnique()
                   .HasDatabaseName("IX_ConfiguracaoCobrancaRegra_ConfiguracaoCobrancaId");
        }
    }
}
