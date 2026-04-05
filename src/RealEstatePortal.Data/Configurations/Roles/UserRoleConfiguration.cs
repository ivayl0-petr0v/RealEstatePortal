using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstatePortal.Data.Configurations.Roles;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(new IdentityUserRole<string>
        {
            RoleId = "11111111-1111-1111-1111-111111111111",
            UserId = "33333333-3333-3333-3333-333333333333"
        });
    }
}
