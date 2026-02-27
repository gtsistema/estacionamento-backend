using System;
using Estac.Domain.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders.User
{
    public class UserMapping : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            
            builder.ToTable("User");

            builder.HasOne(m => m.Pessoa)
                   .WithMany() // ou .WithMany(p => p.Estacionamentos) se existir coleção
                   .HasForeignKey(m => m.PessoaId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(m => m.Transportadora)
                              .WithMany() // ou .WithMany(p => p.Motoristas) se existir coleção
                              .HasForeignKey(m => m.TransportadoraId)
                              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Estacionamento)
                             .WithMany() // ou .WithMany(p => p.Motoristas) se existir coleção
                             .HasForeignKey(m => m.EstacionadoId)
                             .OnDelete(DeleteBehavior.Restrict);

            // ===================SEED====
            builder.HasData(new List<ApplicationUser>
            {
                //new ApplicationUser
                //{
                //    Id = new Guid("9552c4b8-e60e-42a6-8f97-96b8f8edd555"),
                //    UserName = "Admin",
                //    NormalizedUserName = "jeancpcorrea@gmail.com",
                //    Email = "jeancpcorrea@gmail.com",
                //    NormalizedEmail = "JEANCPCORREA@GMAIL.COM",
                //    EmailConfirmed = true,
                //    PasswordHash = "@Admin123456",
                //    SecurityStamp = "LDPU32DFTNBNBJ5JQ2R7VXEHNCGE6UER",
                //    ConcurrencyStamp = "1a503903-b30e-4c13-9e8a-2ec6038c475c",
                //    PhoneNumberConfirmed = true,
                //    PhoneNumber = "65981324241",
                //}
            });
        }
    }
}