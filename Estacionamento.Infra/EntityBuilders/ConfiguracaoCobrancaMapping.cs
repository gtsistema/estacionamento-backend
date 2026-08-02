using Estac.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class ConfiguracaoCobrancaMapping : IEntityTypeConfiguration<ConfiguracaoCobranca>
    {
        public void Configure(EntityTypeBuilder<ConfiguracaoCobranca> builder)
        {
            builder.ToTable("ConfiguracaoCobranca", "gts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TransportadoraId)
                   .IsRequired();

            builder.Property(x => x.EstacionamentoId)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .HasConversion<byte>()
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.Property(x => x.ModalidadeCobranca)
                   .HasConversion<byte>()
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.Property(x => x.DiaFechamento)
                   .HasColumnType("tinyint");

            builder.Property(x => x.RegraFechamento)
                   .HasConversion<byte>()
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.Property(x => x.PrazoVencimentoDias)
                   .IsRequired();

            builder.Property(x => x.EmailFinanceiro)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.EnvioAutomaticoEmail)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.GerarFaturaAutomaticamente)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.PermitirPagamentoParcial)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.AplicarMulta)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.MultaPercentual)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0m)
                   .IsRequired();

            builder.Property(x => x.AplicarJuros)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.JurosPercentual)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0m)
                   .IsRequired();

            builder.Property(x => x.AplicarDescontoFixo)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.ValorDescontoFixo)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0m)
                   .IsRequired();

            builder.Property(x => x.AplicarAcrescimoFixo)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.ValorAcrescimoFixo)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0m)
                   .IsRequired();

            builder.Property(x => x.ValorEstacionamento)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.DataCobranca)
                   .HasColumnType("date");

            builder.Property(x => x.CobrarLavagem)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.ValorLavagem)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.CobrarPernoite)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.ValorPernoite)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.CobrarServicosExtras)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.ValorServicosExtras)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.ConsiderarBeneficioAbastecimento)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.ValorBeneficioAbastecimento)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.AgruparPorPlaca)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.AgruparPorPeriodo)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.AgruparPorTransportadora)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.Descricao)
                   .HasMaxLength(200);

            builder.Property(x => x.DataCriacao)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.HasOne(x => x.Transportadora)
                   .WithMany()
                   .HasForeignKey(x => x.TransportadoraId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Estacionamento)
                   .WithMany()
                   .HasForeignKey(x => x.EstacionamentoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.TransportadoraId, x.EstacionamentoId })
                   .IsUnique()
                   .HasDatabaseName("IX_ConfiguracaoCobranca_Transportadora_Estacionamento");
        }
    }
}
