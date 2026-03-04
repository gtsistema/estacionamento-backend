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
    public class ContaBancariaMapping : IEntityTypeConfiguration<ContaBancaria>
    {
        public void Configure(EntityTypeBuilder<ContaBancaria> builder)
        {
            builder.ToTable("ContaBancaria");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedNever();

            builder.Property(x => x.EstacionamentoId)
                   .IsRequired();

            builder.Property(x => x.Banco)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(x => x.Agencia)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.AgenciaDigito)
                   .HasMaxLength(5)
                   .IsRequired();

            builder.Property(x => x.Conta)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.ContaDigito)
                   .HasMaxLength(5);

            builder.Property(x => x.TipoConta)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(x => x.ChavePix)
                   .HasMaxLength(150);

            builder.Property(x => x.Titular)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(x => x.CpfCnpj)
                   .HasMaxLength(18)
                   .IsRequired();

            builder.Property(x => x.Ativa)
                   .HasDefaultValue(true)
                   .IsRequired();

            builder.Property(x => x.DataCriacao)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            // 🔗 Relacionamento 1:N
            builder.HasOne(x => x.Estacionamento)
                   .WithMany(x => x.ContasBancarias)
                   .HasForeignKey(x => x.EstacionamentoId)
                   .OnDelete(DeleteBehavior.Restrict);

            // 📌 Índices importantes
            builder.HasIndex(x => x.EstacionamentoId);

            builder.HasIndex(x => new { x.EstacionamentoId, x.Ativa });
        }
    }
}
