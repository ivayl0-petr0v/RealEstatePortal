namespace RealEstatePortal.Web.ViewModels.RealEstate;

public class RealEstateDeleteViewModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Address { get; set; } = null!;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = null!;
}
