namespace RealEstatePortal.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealEstatePortal.Data.Models;
    using static Common.EntityValidation.RealEstateImage;

    public class RealEstateImageConfiguration : IEntityTypeConfiguration<RealEstateImage>
    {
        public void Configure(EntityTypeBuilder<RealEstateImage> builder)
        {
            builder
                .HasOne(i => i.RealEstate)
                .WithMany(re => re.RealEstateImages)
                .HasForeignKey(i => i.RealEstateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(i => i.ImageUrl)
                .IsRequired()
                .HasMaxLength(ImageUrlMaxLength);

            builder
                .HasQueryFilter(i => i.RealEstate.IsDeleted == false);
        }
    }
}
