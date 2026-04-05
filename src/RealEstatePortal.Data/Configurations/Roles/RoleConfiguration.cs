using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstatePortal.Data.Configurations.Roles;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "11111111-1111-1111-1111-111111111111",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
                Id = "22222222-2222-2222-2222-222222222222",
                Name = "User",
                NormalizedName = "USER"
            }
        );
    }
}
