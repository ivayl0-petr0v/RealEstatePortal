namespace RealEstatePortal.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using static Common.EntityValidation.Agent;

public class AgentConfiguration : IEntityTypeConfiguration<Agent>
{
    public void Configure(EntityTypeBuilder<Agent> builder)
    {
        builder
            .HasKey(a => a.Id);

        builder
            .HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<Agent>(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasQueryFilter(a => a.IsDeleted == false);

        builder
            .Property(a => a.FullName)
            .IsRequired()
            .HasMaxLength(FullNameMaxLength);

        builder
            .Property(a => a.AvatarUrl)
            .IsRequired(false)
            .HasMaxLength(AvatarUrlMaxLength);

        builder
            .Property(a => a.Address)
            .IsRequired()
            .HasMaxLength(AddressMaxLength);

        builder
            .Property(a => a.PhoneNumber)
            .IsRequired()
            .HasMaxLength(PhoneNumberMaxLength);

        builder
            .Property(a => a.WorkingDays)
            .IsRequired(false)
            .HasMaxLength(WorkingDaysMaxLength);

        builder
            .Property(a => a.RestDays)
            .IsRequired(false)
            .HasMaxLength(RestDaysMaxLength);

        builder
            .Property(a => a.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength);

        builder
            .Property(a => a.FacebookUrl)
            .IsRequired(false)
            .HasMaxLength(FacebookUrlMaxLength);

        builder
            .Property(a => a.WebsiteUrl)
            .IsRequired(false)
            .HasMaxLength(WebsiteUrlMaxLength);

        builder
            .Property(a => a.InstagramUrl)
            .IsRequired(false)
            .HasMaxLength(InstagramUrlMaxLength);
    }
}
