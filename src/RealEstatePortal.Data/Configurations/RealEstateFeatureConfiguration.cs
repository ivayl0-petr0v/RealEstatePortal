namespace RealEstatePortal.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealEstatePortal.Data.Models;

    public class RealEstateFeatureConfiguration : IEntityTypeConfiguration<RealEstateFeature>
    {
        public void Configure(EntityTypeBuilder<RealEstateFeature> builder)
        {
            builder
                .HasKey(rf => new { rf.RealEstateId, rf.FeatureId });

            builder
                .HasOne(rf => rf.RealEstate)
                .WithMany(r => r.RealEstateFeatures)
                .HasForeignKey(rf => rf.RealEstateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(rf => rf.Feature)
                .WithMany(f => f.RealEstateFeatures)
                .HasForeignKey(rf => rf.FeatureId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasQueryFilter(rf => rf.RealEstate.IsDeleted == false);
        }
    }
}