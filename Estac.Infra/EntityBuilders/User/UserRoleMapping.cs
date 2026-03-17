using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders.User
{
    public class UserRoleMapping : IEntityTypeConfiguration<IdentityUserRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            builder.HasKey(r => new {r.RoleId, r.UserId});

            builder.ToTable("UserRole", "dbo");

            // ============SEED===========            
            //builder.HasData(new List<IdentityUserRole<int>>
            //{
            //    //new IdentityUserRole<Guid>
            //    //{
            //    //    RoleId = new Guid("078A8C6E-A76D-46E8-BA75-3E9476C0A35C"),
            //    //    UserId = new Guid("9552c4b8-e60e-42a6-8f97-96b8f8edd555")
            //    //},
            //    //new IdentityUserRole<Guid>
            //    //{
            //    //    RoleId = new Guid("078a8c6e-a76d-46e8-ba75-3e9476c0a35c"),
            //    //    UserId = new Guid("beea0eee-0e6e-4272-969f-fbf27c766f00")
            //    //},
            //});
        }
    }
}