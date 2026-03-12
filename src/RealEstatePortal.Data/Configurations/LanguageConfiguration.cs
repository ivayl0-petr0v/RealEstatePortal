namespace RealEstatePortal.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealEstatePortal.Data.Models;
    using static RealEstatePortal.Data.Common.EntityValidation.Language;

    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder
                .HasKey(l => l.Id);

            builder
                .Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);
        }
    }
}
