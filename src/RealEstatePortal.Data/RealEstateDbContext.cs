namespace RealEstatePortal.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Configurations;
    using Models;

    public class RealEstateDbContext : IdentityDbContext
    {
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RealEstate> RealEstates { get; set; } = null!;
        public virtual DbSet<Agent> Agents { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Feature> Features { get; set; } = null!;
        public virtual DbSet<RealEstateFeature> RealEstateFeatures { get; set; } = null!;
        public virtual DbSet<RealEstateImage> RealEstateImages { get; set; } = null!;
        public virtual DbSet<Inquiry> Inquiries { get; set; } = null!;
        public virtual DbSet<Language> Languages { get; set; } = null!;
        public virtual DbSet<AgentLanguage> AgentLanguages { get; set; } = null!;
        public virtual DbSet<UserFavoriteRealEstate> UserFavoriteRealEstates { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AgentConfiguration());
            builder.ApplyConfiguration(new AgentLanguageConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new FeatureConfiguration());
            builder.ApplyConfiguration(new InquiryConfiguration());
            builder.ApplyConfiguration(new LanguageConfiguration());
            builder.ApplyConfiguration(new RealEstateConfiguration());
            builder.ApplyConfiguration(new RealEstateFeatureConfiguration());
            builder.ApplyConfiguration(new RealEstateImageConfiguration());
            builder.ApplyConfiguration(new UserFavoriteRealEstateConfiguration());
        }
    }
}
