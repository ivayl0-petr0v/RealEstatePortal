namespace RealEstatePortal.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Inquiry
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string SenderName { get; set; } = null!;

    [EmailAddress]
    public string SenderEmail { get; set; } = null!;

    public string? SenderPhone { get; set; }

    public string Message { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public Guid RealEstateId { get; set; }
    public virtual RealEstate RealEstate { get; set; } = null!;

    public string? UserId { get; set; }
    public virtual ApplicationUser? User { get; set; }
}