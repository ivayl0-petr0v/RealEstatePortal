namespace RealEstatePortal.Web.ViewModels.Admin;

public class AdminRealEstateViewModel
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }

    public string Address { get; set; } = null!;

    public string AgentName { get; set; } = null!;

    public string AgentEmail { get; set; } = null!;
}
