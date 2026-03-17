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
    public class PessoaContatoMapping : IEntityTypeConfiguration<PessoaContato>
    {
        public void Configure(EntityTypeBuilder<PessoaContato> builder)
        {
            builder.ToTable("PessoaContato", "gts");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.PessoaId)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Principal)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(p => p.TipoContato)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Numero)
                .HasColumnType("varchar(20)")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.Observacao)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150);

            builder.HasOne(p => p.Pessoa)
                .WithMany(p => p.Contatos)
                .HasForeignKey(p => p.PessoaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.PessoaId);

            builder.HasIndex(p => new { p.PessoaId, p.Numero });
        }
    }
}
