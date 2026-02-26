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
    public class VagaMapping : IEntityTypeConfiguration<Vaga>
    {
        public void Configure(EntityTypeBuilder<Vaga> builder)
        {
            builder.ToTable("Vaga");

            builder.HasKey(v => v.Id);
                builder.Property(v => v.Descricao)
                .HasColumnName("Descricao")
                .HasColumnType("varchar(150)")
                .HasMaxLength(150);

            builder.Property(v => v.Ativo)
                 .HasDefaultValue(true);

            builder.Property(v => v.Observacao)
                   .HasMaxLength(500) // ajuste se necessário
                   .IsRequired(false);

            // Índice opcional para melhorar buscas por status
            builder.HasIndex(v => v.Ativo);
        }
    }
}
