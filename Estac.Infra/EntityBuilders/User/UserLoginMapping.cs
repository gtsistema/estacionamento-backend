using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders.User
{
    public class UserLoginMapping : IEntityTypeConfiguration<IdentityUserLogin<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<int>> builder)
        {
            builder.HasKey(r => new {r.LoginProvider, r.ProviderKey});

            builder.ToTable("UserLogin", "dbo");
        }
    }
}