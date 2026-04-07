using RealEstatePortal.Web.ViewModels.RealEstate;

namespace RealEstatePortal.Web.ViewModels.Agent;

public class AgentDetailsViewModel
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string AvatarUrl { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public string Description { get; set; } = null!;

    public string? WorkingDays { get; set; }

    public string? RestDays { get; set; }

    public string? WorkingHours { get; set; }

    public IEnumerable<string> SpokenLanguages { get; set; }
        = new List<string>();

    public string? FacebookUrl { get; set; }

    public string? WebsiteUrl { get; set; }

    public string? InstagramUrl { get; set; }

    public IEnumerable<AllRealEstatesViewModel> AgentRealEstates { get; set; }
        = new List<AllRealEstatesViewModel>();
}