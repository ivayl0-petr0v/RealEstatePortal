namespace RealEstatePortal.Web.ViewModels.RealEstate;

public class AllRealEstatesQueryModel
{
    public string? SearchTerm { get; set; }

    public string? TransactionType { get; set; }

    public string? Sorting { get; set; }

    public IEnumerable<AllRealEstatesViewModel> RealEstates { get; set; }
        = new List<AllRealEstatesViewModel>();

    public int CurrentPage { get; set; } = 1;

    public int RealEstatesPerPage { get; set; } = 6;

    public int TotalRealEstatesCount { get; set; }
}
