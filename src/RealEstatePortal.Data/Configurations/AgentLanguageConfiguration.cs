namespace RealEstatePortal.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class AgentLanguageConfiguration : IEntityTypeConfiguration<AgentLanguage>
    {
        public void Configure(EntityTypeBuilder<AgentLanguage> builder)
        {
            builder
                .HasKey(al => new { al.AgentId, al.LanguageId });

            builder
                .HasOne(al => al.Agent)
                .WithMany(a => a.SpokenLanguages)
                .HasForeignKey(al => al.AgentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(al => al.Language)
                .WithMany(l => l.AgentLanguages)
                .HasForeignKey(al => al.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
