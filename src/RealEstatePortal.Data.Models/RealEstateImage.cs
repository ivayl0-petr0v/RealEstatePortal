namespace RealEstatePortal.Data.Models;

public class RealEstateImage
{

    public int Id { get; set; }

    public string ImageUrl { get; set; } = null!;

    public Guid RealEstateId { get; set; }
    public virtual RealEstate RealEstate { get; set; } = null!;

    public bool IsMain { get; set; }
}
