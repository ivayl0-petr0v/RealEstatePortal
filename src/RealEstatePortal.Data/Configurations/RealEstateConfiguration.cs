namespace RealEstatePortal.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;
    using RealEstatePortal.Data.Models.Enums;
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

            builder
                .HasData(SeedRealEstates());
        }

        private IEnumerable<RealEstate> SeedRealEstates()
        {
            return new List<RealEstate>
    {
        new RealEstate
        {
            Id = Guid.Parse("3f9c5b2a-8d1e-4f7a-b2c6-9e4a1d5c8b7f"),
            AgentId = Guid.Parse("D7445E1E-E99E-4CAE-82F4-515775351513"),
            CategoryId = 1,
            CityId = 1,
            Address = "bul. Cherni Vrah 45",
            Price = 250000,
            Area = 120,
            RoomsCount = 4,
            BedroomsCount = 3,
            BathroomsCount = 2,
            TransactionType = TransactionType.Sale,
            ConstructionType = "Brick",
            ConstructionYear = 2022,
            CompletionStatus = "Fully finished",
            Furnishing = "Unfurnished",
            TotalFloors = 8,
            RealEstateFloor = 5,
            Exposure = "South",
            Description = "Spacious 3-bedroom apartment in a premium location with underground parking and 24/7 security.",
            Status = Status.Available,
            IsDeleted = false
        },
        new RealEstate
        {
            Id = Guid.Parse("a1b2c3d4-e5f6-47a8-9b0c-1d2e3f4a5b6c"),
            AgentId = Guid.Parse("D7445E1E-E99E-4CAE-82F4-515775351513"),
            CategoryId = 2,
            CityId = 2,
            Address = "ul. Ivan Vazov 12",
            Price = 145000,
            Area = 95,
            RoomsCount = 3,
            BedroomsCount = 2,
            BathroomsCount = 1,
            TransactionType = TransactionType.Sale,
            ConstructionType = "Brick",
            ConstructionYear = 2015,
            CompletionStatus = "Fully finished",
            Furnishing = "Fully Furnished",
            TotalFloors = 2,
            RealEstateFloor = 1,
            Exposure = "East-West",
            Description = "Cozy house in the central area. Recently renovated with a small private garden.",
            Status = Status.Available,
            IsDeleted = false
        },
        new RealEstate
        {
            Id = Guid.Parse("7e8f9a0b-1c2d-43e4-85f6-7a8b9c0d1e2f"),
            AgentId = Guid.Parse("D7445E1E-E99E-4CAE-82F4-515775351513"),
            CategoryId = 1,
            CityId = 3,
            Address = "Briz, ul. Sveti Nikola",
            Price = 650,
            Area = 70,
            RoomsCount = 2,
            BedroomsCount = 1,
            BathroomsCount = 1,
            TransactionType = TransactionType.Rent,
            ConstructionType = "Brick",
            ConstructionYear = 2019,
            CompletionStatus = "Ready for living",
            Furnishing = "Fully Furnished",
            TotalFloors = 5,
            RealEstateFloor = 3,
            Exposure = "South-East",
            Description = "Modern 1-bedroom apartment for rent with sea view. Fully furnished.",
            Status = Status.Available,
            IsDeleted = false
        },
        new RealEstate
        {
            Id = Guid.Parse("d4c3b2a1-6f5e-48a7-b0c9-d1e2f3a4b5c6"),
            AgentId = Guid.Parse("72D70512-EE7B-4F2E-BD62-BAF7773F85FD"),
            CategoryId = 3,
            CityId = 1,
            Address = "Mladost 4, Business Park",
            Price = 320000,
            Area = 155,
            RoomsCount = 5,
            BedroomsCount = 4,
            BathroomsCount = 3,
            TransactionType = TransactionType.Sale,
            ConstructionType = "Brick",
            ConstructionYear = 2021,
            CompletionStatus = "Fully finished",
            Furnishing = "Semi-furnished",
            TotalFloors = 12,
            RealEstateFloor = 11,
            Exposure = "South-West",
            Description = "Luxury penthouse right next to Business Park Sofia. Huge terrace and smart home system.",
            Status = Status.Available,
            IsDeleted = false
        },
        new RealEstate
        {
            Id = Guid.Parse("f1e2d3c4-b5a6-4978-8c0b-a19b2c3d4e5f"),
            AgentId = Guid.Parse("72D70512-EE7B-4F2E-BD62-BAF7773F85FD"),
            CategoryId = 1,
            CityId = 4,
            Address = "Lazur, bl. 77",
            Price = 115000,
            Area = 65,
            RoomsCount = 2,
            BedroomsCount = 1,
            BathroomsCount = 1,
            TransactionType = TransactionType.Sale,
            ConstructionType = "Brick",
            ConstructionYear = 1998,
            CompletionStatus = "Fully finished",
            Furnishing = "Unfurnished",
            TotalFloors = 6,
            RealEstateFloor = 4,
            Exposure = "North-East",
            Description = "Excellent investment property near the Sea Garden. Needs minor cosmetic repairs.",
            Status = Status.Available,
            IsDeleted = false
        },
        new RealEstate
        {
            Id = Guid.Parse("5c6b7a89-0d1e-4f2a-93b4-c5d6e7f8a9b0"),
            AgentId = Guid.Parse("72D70512-EE7B-4F2E-BD62-BAF7773F85FD"),
            CategoryId = 1,
            CityId = 2,
            Address = "Trakia, bl. 100",
            Price = 400,
            Area = 55,
            RoomsCount = 2,
            BedroomsCount = 1,
            BathroomsCount = 1,
            TransactionType = TransactionType.Rent,
            ConstructionType = "Panel",
            ConstructionYear = 1985,
            CompletionStatus = "Ready for living",
            Furnishing = "Basic Furniture",
            TotalFloors = 8,
            RealEstateFloor = 2,
            Exposure = "West",
            Description = "Affordable apartment for rent in a quiet neighborhood. Close to public transport and supermarkets.",
            Status = Status.Available,
            IsDeleted = false
        }
    };
        }
    }
}

