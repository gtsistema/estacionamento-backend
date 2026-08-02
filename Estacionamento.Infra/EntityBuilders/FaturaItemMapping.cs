using Estac.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class FaturaItemMapping : IEntityTypeConfiguration<FaturaItem>
    {
        public void Configure(EntityTypeBuilder<FaturaItem> builder)
        {
            builder.ToTable("FaturaItem", "gts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FaturaId)
                   .IsRequired();

            builder.Property(x => x.EntradaSaidaId)
                   .IsRequired();

            builder.Property(x => x.Placa)
                   .HasColumnType("varchar(10)")
                   .HasMaxLength(10);

            builder.Property(x => x.DataHoraEntrada)
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(x => x.DataHoraSaida)
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(x => x.TempoPermanenciaMinutos)
                   .HasDefaultValue(0)
                   .IsRequired();

            builder.Property(x => x.ValorEstacionamento)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0m)
                   .IsRequired();

            builder.Property(x => x.ValorLavagem)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0m)
                   .IsRequired();

            builder.Property(x => x.ValorPernoite)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0m)
                   .IsRequired();

            builder.Property(x => x.ValorServicosExtras)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0m)
                   .IsRequired();

            builder.Property(x => x.ValorBeneficioAbastecimento)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0m)
                   .IsRequired();

            builder.Property(x => x.ValorTotal)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0m)
                   .IsRequired();

            builder.Property(x => x.Descricao)
                   .HasColumnType("varchar(200)")
                   .HasMaxLength(200);

            builder.Property(x => x.DataCriacao)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.HasOne(x => x.Fatura)
                   .WithMany(x => x.Itens)
                   .HasForeignKey(x => x.FaturaId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.EntradaSaida)
                   .WithMany()
                   .HasForeignKey(x => x.EntradaSaidaId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Atende o NOT EXISTS da consulta de movimentos elegíveis.
            builder.HasIndex(x => x.EntradaSaidaId)
                   .HasDatabaseName("IX_FaturaItem_EntradaSaidaId");

            builder.HasIndex(x => new { x.FaturaId, x.EntradaSaidaId })
                   .IsUnique()
                   .HasDatabaseName("IX_FaturaItem_FaturaId_EntradaSaidaId");
        }
    }
}
