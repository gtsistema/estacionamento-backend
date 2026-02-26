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
    public class PessoaPapelMapping : IEntityTypeConfiguration<PessoaPapel>
    {
        public void Configure(EntityTypeBuilder<PessoaPapel> builder)
        {
            builder.ToTable("PessoaPapel");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.PessoaId)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.TipoPapel)
                .HasColumnType("int")
                .IsRequired();

            builder.HasOne(p => p.Pessoa)
                .WithMany(p => p.Papeis)
                .HasForeignKey(p => p.PessoaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.PessoaId);

            builder.HasIndex(p => new { p.PessoaId, p.TipoPapel })
                .IsUnique();
        }
    }
}
