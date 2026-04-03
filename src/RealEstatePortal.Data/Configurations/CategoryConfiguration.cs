namespace RealEstatePortal.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using static RealEstatePortal.Data.Common.EntityValidation.Category;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength);

        builder
            .HasData(SeedCategories());
    }
    private IEnumerable<Category> SeedCategories()
    {
        return new List<Category>
        {
            new Category{ Id = 1, Name = "Studio Apartment" },
            new Category { Id = 2, Name = "1-Bedroom Apartment" },
            new Category { Id = 3, Name = "2-Bedroom Apartment" },
            new Category { Id = 4, Name = "3+ Bedroom Apartment" },
            new Category { Id = 5, Name = "Maisonette / Duplex" },
            new Category { Id = 6, Name = "House" },
            new Category { Id = 7, Name = "House Floor" },
            new Category { Id = 8, Name = "Villa" },
            new Category { Id = 9, Name = "Land / Plot" },
            new Category { Id = 10, Name = "Office" },
            new Category { Id = 11, Name = "Commercial Space" },
            new Category { Id = 12, Name = "Garage / Parking" }
        };
    }

}
