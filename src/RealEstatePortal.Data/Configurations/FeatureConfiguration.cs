namespace RealEstatePortal.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    using static RealEstatePortal.Data.Common.EntityValidation.Feature;

    public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder
                .HasKey(f => f.Id);

            builder
                .Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder
                .HasData(SeedFeatures());
        }

        private IEnumerable<Feature> SeedFeatures()
        {
            return new List<Feature>
            {
                new Feature { Id = 1, Name = "Elevator" },
                new Feature { Id = 2, Name = "Garage / Parking" },
                new Feature { Id = 3, Name = "Central Heating" },
                new Feature { Id = 4, Name = "Air Conditioning" },
                new Feature { Id = 5, Name = "Security System" },
                new Feature { Id = 6, Name = "Access Control" },
                new Feature { Id = 7, Name = "Thermal Insulation" },
                new Feature { Id = 8, Name = "Gas Heating" },
                new Feature { Id = 9, Name = "Swimming Pool" },
                new Feature { Id = 10, Name = "Gym / Fitness Center" },
                new Feature { Id = 11, Name = "Sports Facilities" },
                new Feature { Id = 12, Name = "Smart Home" },
                new Feature { Id = 13, Name = "Video Surveillance (CCTV)" },
                new Feature { Id = 14, Name = "Storage / Basement" }
            };
        }
    }
}
