using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstatePortal.Data.Models;

namespace RealEstatePortal.Data.Configurations.Roles;

public class AdminUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var adminUser = new ApplicationUser
        {
            Id = "33333333-3333-3333-3333-333333333333",
            UserName = "admin@test.com",
            NormalizedUserName = "ADMIN@TEST.COM",
            Email = "admin@test.com",
            NormalizedEmail = "ADMIN@TEST.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var hasher = new PasswordHasher<ApplicationUser>();
        adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin123!");

        builder.HasData(adminUser);
    }
}