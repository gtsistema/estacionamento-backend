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
    public class PessoaEnderecoMapping : IEntityTypeConfiguration<PessoaEndereco>
    {
        public void Configure(EntityTypeBuilder<PessoaEndereco> builder)
        {
            builder.ToTable("PessoaEndereco", "gts");

            builder.HasKey(p => p.Id);

            builder.Property(v => v.Descricao)
               .HasColumnName("Descricao")
               .HasColumnType("varchar(150)")
               .HasMaxLength(150);

            builder.Property(p => p.PessoaId)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Principal)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(p => p.TipoEndereco)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Cep)
                .HasColumnType("varchar(9)")
                .HasMaxLength(9);

            builder.Property(p => p.Logradouro)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150);

            builder.Property(p => p.Numero)
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            builder.Property(p => p.Complemento)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(p => p.Bairro)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(p => p.Cidade)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(p => p.Estado)
                .HasColumnType("varchar(2)")
                .HasMaxLength(2);

            builder.HasOne(p => p.Pessoa)
                .WithMany(p => p.Enderecos)
                .HasForeignKey(p => p.PessoaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.PessoaId);
        }
    }
}
