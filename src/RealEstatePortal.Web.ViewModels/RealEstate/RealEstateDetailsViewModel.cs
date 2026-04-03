namespace RealEstatePortal.Web.ViewModels.RealEstate;

public class RealEstateDetailsViewModel
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Address { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal Area { get; set; }

    public string TransactionType { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int RoomsCount { get; set; }

    public int BedroomsCount { get; set; }

    public int BathroomsCount { get; set; }

    public string ConstructionType { get; set; } = null!;

    public int ConstructionYear { get; set; }

    public string CompletionStatus { get; set; } = null!;

    public string? Furnishing { get; set; }

    public int TotalFloors { get; set; }

    public int RealEstateFloor { get; set; }

    public string? Exposure { get; set; }

    public IEnumerable<string> ImageUrls { get; set; } = new List<string>();

    public IEnumerable<string> Features { get; set; } = new List<string>();

    public string AgentId { get; set; } = null!;

    public string AgentName { get; set; } = null!;

    public string AgentPhoneNumber { get; set; } = null!;

    public string? AgentEmail { get; set; }

    public string? AgentAvatarUrl { get; set; }

    public bool IsOwner { get; set; }
}
