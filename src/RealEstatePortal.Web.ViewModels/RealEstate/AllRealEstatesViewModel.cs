namespace RealEstatePortal.Web.ViewModels.RealEstate;

public class AllRealEstatesViewModel
{
    public string Id { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public decimal Price { get; set; }
    public string Title { get; set; } = null!;
    public decimal Area { get; set; }
    public string Address { get; set; } = null!;
    public int RoomsCount { get; set; }
    public int BedroomsCount { get; set; }
    public int BathroomsCount { get; set; }
    public bool IsFavorite { get; set; }
}