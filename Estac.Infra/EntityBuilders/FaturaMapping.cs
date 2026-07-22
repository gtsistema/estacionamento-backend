using Estac.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class FaturaMapping : IEntityTypeConfiguration<Fatura>
    {
        public void Configure(EntityTypeBuilder<Fatura> builder)
        {
            builder.ToTable("Fatura", "gts");

            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.ValorEmAberto);

            builder.Property(x => x.TransportadoraId)
                   .IsRequired();

            builder.Property(x => x.EstacionamentoId)
                   .IsRequired();

            builder.Property(x => x.ConfiguracaoCobrancaId)
                   .IsRequired(false);

            builder.Property(x => x.Numero)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .HasConversion<byte>()
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.Property(x => x.ModalidadeRecebimento)
                   .HasConversion<byte?>()
                   .HasColumnType("tinyint");

            builder.Property(x => x.ValorTotal)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.ValorRecebido)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0m)
                   .IsRequired();

            builder.Property(x => x.ValorDesconto)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0m)
                   .IsRequired();

            builder.Property(x => x.ValorAcrescimo)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0m)
                   .IsRequired();

            builder.Property(x => x.ValorJuros)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0m)
                   .IsRequired();

            builder.Property(x => x.ValorMulta)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0m)
                   .IsRequired();

            builder.Property(x => x.DataEmissao)
                   .IsRequired();

            builder.Property(x => x.DataVencimento)
                   .IsRequired();

            builder.Property(x => x.PeriodoInicio)
                   .IsRequired();

            builder.Property(x => x.PeriodoFim)
                   .IsRequired();

            builder.Property(x => x.EmailEnvio)
                   .HasMaxLength(200);

            builder.Property(x => x.Observacao)
                   .HasMaxLength(500);

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

            builder.HasOne(x => x.ConfiguracaoCobranca)
                   .WithMany()
                   .HasForeignKey(x => x.ConfiguracaoCobrancaId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(x => x.Numero)
                   .IsUnique()
                   .HasDatabaseName("IX_Fatura_Numero");

            builder.HasIndex(x => x.TransportadoraId);
            builder.HasIndex(x => x.EstacionamentoId);
            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.DataVencimento);
            builder.HasIndex(x => x.DataEmissao);
        }
    }
}
