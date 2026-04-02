namespace RealEstatePortal.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    using static Common.EntityValidation.RealEstate;

    public class RealEstateConfiguration : IEntityTypeConfiguration<RealEstate>
    {
        public void Configure(EntityTypeBuilder<RealEstate> builder)
        {
            builder
               .HasKey(re => re.Id);

            builder
                .HasOne(re => re.Category)
                .WithMany(cat => cat.RealEstates)
                .HasForeignKey(re => re.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasQueryFilter(re => re.IsDeleted == false && re.Agent.IsDeleted == false);

            builder
                .HasOne(re => re.City)
                .WithMany(city => city.RealEstates)
                .HasForeignKey(re => re.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(re => re.Agent)
                .WithMany(a => a.RealEstates)
                .HasForeignKey(re => re.AgentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(re => re.Price)
                .IsRequired()
                .HasColumnType(PriceColumnType);

            builder
                .Property(re => re.Area)
                .IsRequired()
                .HasColumnType(AreaColumnType);

            builder
                .Property(re => re.Address)
                .IsRequired()
                .HasMaxLength(AddressMaxLength);

            builder
                .Property(re => re.ConstructionType)
                .IsRequired()
                .HasMaxLength(ConstructionTypeMaxLength);

            builder
                .Property(re => re.CompletionStatus)
                .IsRequired()
                .HasMaxLength(CompletionStatusMaxLength);

            builder
                .Property(re => re.Furnishing)
                .IsRequired()
                .HasMaxLength(FurnishingMaxLength);

            builder
                .Property(re => re.Exposure)
                .IsRequired(false)
                .HasMaxLength(ExposureMaxLength);

            builder
                .Property(re => re.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);
        }
    }
}
