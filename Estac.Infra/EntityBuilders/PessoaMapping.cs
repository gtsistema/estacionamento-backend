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
    public class PessoaMapping : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoa");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.TipoPessoa)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.NomeRazaoSocial)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(p => p.NomeFantasia)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150);

            builder.Property(p => p.Documento)
                .HasColumnType("varchar(18)")
                .HasMaxLength(18)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150);

            builder.Property(p => p.Ativo)
                .HasColumnType("bit")
                .HasDefaultValue(true);

            builder.HasMany(p => p.Papeis)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Contatos)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Enderecos)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.Documento)
                .IsUnique();
        }
    }
}
