namespace RealEstatePortal.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using RealEstatePortal.Data.Models;
    using static RealEstatePortal.Data.Common.EntityValidation.Inquiry;

    public class InquiryConfiguration : IEntityTypeConfiguration<Inquiry>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Inquiry> builder)
        {
            builder
                .HasKey(i => i.Id);

            builder
                .HasOne(i => i.RealEstate)
                .WithMany(re => re.Inquiries)
                .HasForeignKey(i => i.RealEstateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(i => i.User)
                .WithMany(u => u.Inquiries)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(i => i.SenderName)
                .IsRequired()
                .HasMaxLength(SenderNameMaxLength);

            builder
                .Property(i => i.SenderEmail)
                .IsRequired()
                .HasMaxLength(SenderEmailMaxLength);

            builder
                .Property(i => i.SenderPhone)
                .IsRequired(false)
                .HasMaxLength(SenderPhoneMaxLength);

            builder
                .Property(i => i.Message)
                .IsRequired()
                .HasMaxLength(MessageMaxLength);
        }
    }
}
