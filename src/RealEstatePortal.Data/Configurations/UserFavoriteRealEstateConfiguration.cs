namespace RealEstatePortal.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealEstatePortal.Data.Models;
    public class UserFavoriteRealEstateConfiguration : IEntityTypeConfiguration<UserFavoriteRealEstate>
    {
        public void Configure(EntityTypeBuilder<UserFavoriteRealEstate> builder)
        {
            builder
                .HasKey(uf => new { uf.UserId, uf.RealEstateId });

            builder
                .HasOne(uf => uf.User)
                .WithMany(u => u.FavoriteRealEstates)
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(uf => uf.RealEstate)
                .WithMany(re => re.FavoriteRealEstates)
                .HasForeignKey(uf => uf.RealEstateId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}